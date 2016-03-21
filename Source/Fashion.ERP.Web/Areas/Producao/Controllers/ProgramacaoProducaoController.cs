using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.Producao;
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Reporting.Producao;
using Fashion.ERP.Web.Areas.Producao.Models;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Common.Utils;
using Fashion.Framework.Mvc.Security;
using Fashion.Framework.Repository;
using FluentNHibernate.Conventions;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using NHibernate.Linq;
using Ninject.Extensions.Logging;
using Telerik.Reporting;

namespace Fashion.ERP.Web.Areas.Producao.Controllers
{
    public partial class ProgramacaoProducaoController : BaseController
    {
        #region Variaveis
        private readonly IRepository<ProgramacaoProducao> _programacaoProducaoRepository;
        private readonly IRepository<Colecao> _colecaoRepository;
        private readonly IRepository<FichaTecnica> _fichaTecnicaRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<Tamanho> _tamanhoRepository;
        private readonly IRepository<Material> _materialRepository;
        private readonly IRepository<ReservaMaterial> _reservaMaterialRepository;
        private readonly IRepository<DepartamentoProducao> _departamentoProducaoRepository;
        private readonly IRepository<ReservaEstoqueMaterial> _reservaEstoqueMaterialRepository;
        private readonly IRepository<ProgramacaoProducaoMatrizCorte> _programacaoProducaoMatrizCorteRepository;
        private readonly IRepository<UltimoNumero> _ultimoNumeroRepository;
        private readonly IRepository<Usuario> _usuarioRepository;
        private readonly IRepository<CentroCusto> _centroCustoRepository;
        private readonly IRepository<RequisicaoMaterial> _requisicaoMaterialRepository;
        private readonly IRepository<TipoItem> _tipoItemRepository;

        private readonly ILogger _logger;
        private long? _proximoNumeroReservaMaterial;
        private long? _proximoNumeroRequisicaoMaterial;
        #endregion

        #region Construtores
        public ProgramacaoProducaoController(ILogger logger, IRepository<ProgramacaoProducao> programacaoProducaoRepository,
            IRepository<Colecao> colecaoRepository, IRepository<FichaTecnica> fichaTecnicaRepository,
            IRepository<Pessoa> pessoaRepository, IRepository<Tamanho> tamanhoRepository,
            IRepository<Material> materialRepository, IRepository<ReservaMaterial> reservaMaterialRepository,
            IRepository<DepartamentoProducao> departamentoProducaoRepository, IRepository<ReservaEstoqueMaterial> reservaEstoqueMaterialRepository,
            IRepository<ProgramacaoProducaoMatrizCorte> programacaoProducaoMatrizCorteRepository, IRepository<UltimoNumero> ultimoNumeroRepository,
            IRepository<Usuario> usuarioRepository, IRepository<CentroCusto> centroCustoRepository,
            IRepository<RequisicaoMaterial> requisicaoMaterialRepository, IRepository<TipoItem> tipoItemRepository)
        {
            _programacaoProducaoRepository = programacaoProducaoRepository;
            _colecaoRepository = colecaoRepository;
            _fichaTecnicaRepository = fichaTecnicaRepository;
            _pessoaRepository = pessoaRepository;
            _tamanhoRepository = tamanhoRepository;
            _materialRepository = materialRepository;
            _reservaMaterialRepository = reservaMaterialRepository;
            _departamentoProducaoRepository = departamentoProducaoRepository;
            _reservaEstoqueMaterialRepository = reservaEstoqueMaterialRepository;
            _programacaoProducaoMatrizCorteRepository = programacaoProducaoMatrizCorteRepository;
            _ultimoNumeroRepository = ultimoNumeroRepository;
            _usuarioRepository = usuarioRepository;
            _centroCustoRepository = centroCustoRepository;
            _requisicaoMaterialRepository = requisicaoMaterialRepository;
            _tipoItemRepository = tipoItemRepository;
            _logger = logger;
        }
        #endregion

        #region Colunas Ordenação
        private static readonly Dictionary<string, string> ColunasOrdenacaoGrid = new Dictionary<string, string>
        {
            {"LoteAno", "Lote"},
            {"Colecao", "Colecao.Descricao"},
            {"DataProgramada", "DataProgramada"},
            {"Tag", "FichaTecnica.Tag"},
            {"Referencia", "FichaTecnica.Referencia"},
            {"Descricao", "FichaTecnica.Descricao"},
            {"QtdeProgramada", "Quantidade"}
        };
        #endregion

        #region Colunas Ordenação de Relatório
        private static readonly Dictionary<string, string> ColunasOrdenacaoRelatorio = new Dictionary<string, string>
        {
            {"Lote", "Lote"},
            {"Coleção", "Colecao.Descricao"},
            {"Data Programada", "DataProgramada"},
            {"Responsável", "Funcionario.Nome"}
        };
        #endregion

        #region Novo
        [PopulateViewData("PopulateViewData"), HttpGet]
        public virtual ActionResult Novo()
        {
            return View(new ProgramacaoProducaoModel());
        }

        [HttpPost, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(ProgramacaoProducaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<ProgramacaoProducao>(model);
                    domain.Colecao = _colecaoRepository.Load(model.Colecao);
                    domain.Data = DateTime.Now.Date;
                    domain.DataAlteracao = DateTime.Now.Date;
                    domain.Funcionario = _pessoaRepository.Load(model.Responsavel);
                    domain.Quantidade = model.Quantidade;
                    domain.SituacaoProgramacaoProducao = SituacaoProgramacaoProducao.Iniciada;

                    if (model.Lote.HasValue)
                    {
                        domain.Lote = model.Lote.GetValueOrDefault();
                        domain.Ano = model.Ano.GetValueOrDefault();
                    } else
                    {
                        domain.Lote = ObtenhaProximoNumero();
                        domain.Ano = DateTime.Now.Year;   
                    }

                    var programacaoProducaoMateriais = new List<ProgramacaoProducaoMaterial>();

                    model.GridProgramacaoProducaoItens.ForEach(modelItem =>
                    {
                        var javaScriptSerializer = new JavaScriptSerializer();
                        var programacaoProducaoMatrizCorteModel =
                            javaScriptSerializer.Deserialize<ProgramacaoProducaoMatrizCorteModel>(
                                modelItem.MatrizCorteJson);
                        var programacaoProducaoMatrizCorte = new ProgramacaoProducaoMatrizCorte()
                        {
                            TipoEnfestoTecido =
                                (TipoEnfestoTecido)programacaoProducaoMatrizCorteModel.TipoEnfestoTecido
                        };

                        programacaoProducaoMatrizCorteModel.GridMatrizCorteItens.ForEach(modelMatrizCorteItem =>
                        {
                            var programacaoProducaoMatrizCorteItem = new ProgramacaoProducaoMatrizCorteItem()
                            {
                                Quantidade = modelMatrizCorteItem.Quantidade.GetValueOrDefault(),
                                QuantidadeVezes = modelMatrizCorteItem.QuantidadeVezes.GetValueOrDefault(),
                                Tamanho =
                                    _tamanhoRepository.Get(x => x.Descricao == modelMatrizCorteItem.DescricaoTamanho)
                            };

                            programacaoProducaoMatrizCorte.ProgramacaoProducaoMatrizCorteItens.Add(
                                programacaoProducaoMatrizCorteItem);
                        });

                        var programacaoProducaoItem = new ProgramacaoProducaoItem()
                        {
                            FichaTecnica = _fichaTecnicaRepository.Get(y => y.Referencia == modelItem.Referencia),
                            Quantidade = modelItem.Quantidade.GetValueOrDefault(),
                            ProgramacaoProducaoMatrizCorte = programacaoProducaoMatrizCorte
                        };

                        domain.ProgramacaoProducaoItems.Add(programacaoProducaoItem);

                        programacaoProducaoItem.FichaTecnica.MateriaisConsumo.ForEach(materialConsumoFichaTecnica =>
                        {
                            var programacaoProducaoMaterial = new ProgramacaoProducaoMaterial()
                            {
                                DepartamentoProducao = materialConsumoFichaTecnica.DepartamentoProducao,
                                Material = materialConsumoFichaTecnica.Material,
                                Quantidade = materialConsumoFichaTecnica.Quantidade * programacaoProducaoItem.Quantidade
                            };
                            programacaoProducaoMateriais.Add(programacaoProducaoMaterial);
                        });

                        programacaoProducaoItem.FichaTecnica.MateriaisConsumoVariacao.ForEach(materialConsumoVariacaoFichaTecnica =>
                        {
                            var programacaoProducaoMaterial = new ProgramacaoProducaoMaterial()
                            {
                                DepartamentoProducao = materialConsumoVariacaoFichaTecnica.DepartamentoProducao,
                                Material = materialConsumoVariacaoFichaTecnica.Material,
                                Quantidade = materialConsumoVariacaoFichaTecnica.Quantidade * programacaoProducaoItem.Quantidade
                            };
                            programacaoProducaoMateriais.Add(programacaoProducaoMaterial);
                        });
                    });

                    programacaoProducaoMateriais = programacaoProducaoMateriais.GroupBy(x => new { x.Material, x.DepartamentoProducao }, (chave, grupo) =>
                        new ProgramacaoProducaoMaterial
                        {
                            DepartamentoProducao = chave.DepartamentoProducao,
                            Material = chave.Material,
                            Quantidade = grupo.Sum(x => x.Quantidade),
                        }).ToList();

                    domain.ProgramacaoProducaoMateriais.AddRange(programacaoProducaoMateriais);

                    _programacaoProducaoRepository.SaveOrUpdate(domain);

                    this.AddSuccessMessage("Programação de produção cadastrada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar a programação de produção. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }
        
        public virtual JsonResult ObtenhaListaProgramacaoProducaoMatrizCorteItemModel(string referenciaFichaTecnica)
        {
            var retorno = new List<ProgramacaoProducaoMatrizCorteItemModel>();

            var fichaTecnica = _fichaTecnicaRepository.Get(x => x.Referencia == referenciaFichaTecnica);

            fichaTecnica.FichaTecnicaMatriz.Grade.Tamanhos.Keys.ForEach(tamanho =>
            {
                var modelItem = new ProgramacaoProducaoMatrizCorteItemModel
                {
                    DescricaoTamanho = tamanho.Descricao,
                    Tamanho = tamanho.Id,
                    Quantidade = 0,
                    QuantidadeVezes = 0
                };
                retorno.Add(modelItem);
            });

             return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion
        
        #region Editar

        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(long id)
        {
            var domain = _programacaoProducaoRepository.Get(id);
            var model = Mapper.Flat<ProgramacaoProducaoModel>(domain);
            model.Responsavel = domain.Funcionario.Id;
            model.GridProgramacaoProducaoItens = new List<ProgramacaoProducaoItemModel>();

            domain.ProgramacaoProducaoItems.ForEach(programacaoProducaoItem =>
            {
                var modelItem = new ProgramacaoProducaoItemModel
                {
                    Descricao = programacaoProducaoItem.FichaTecnica.Descricao,
                    Estilista = programacaoProducaoItem.FichaTecnica.Estilista.Nome,
                    Quantidade = programacaoProducaoItem.Quantidade,
                    Foto = ObtenhaUrlFotoFichaTecnica(programacaoProducaoItem.FichaTecnica),
                    Id = programacaoProducaoItem.Id,
                    Referencia = programacaoProducaoItem.FichaTecnica.Referencia,
                    TagAno = programacaoProducaoItem.FichaTecnica.Tag + '/' + programacaoProducaoItem.FichaTecnica.Ano,
                    MatrizCorteJson = ObtenhaMatrizCorteJson(programacaoProducaoItem)
                };
                model.GridProgramacaoProducaoItens.Add(modelItem);
            });

            return View(model);
        }

        public virtual String ObtenhaMatrizCorteJson(ProgramacaoProducaoItem programacaoProducaoItem)
        {
            var matrizCorteModel = new ProgramacaoProducaoMatrizCorteModel
            {
                QuantidadeItem = programacaoProducaoItem.Quantidade,
                TipoEnfestoTecido = (int)programacaoProducaoItem.ProgramacaoProducaoMatrizCorte.TipoEnfestoTecido,
                GridMatrizCorteItens = new List<ProgramacaoProducaoMatrizCorteItemModel>()
            };

            var matrizCorte = programacaoProducaoItem.ProgramacaoProducaoMatrizCorte;

            matrizCorte = ObtenhaProgramacaoProducaoMatrizCorteCompleto(matrizCorte, programacaoProducaoItem.FichaTecnica);
             
            matrizCorte.ProgramacaoProducaoMatrizCorteItens.ForEach(
                programacaoProducaoMatrizCorteItem =>
                {
                    var matrizCorteItemModel = new ProgramacaoProducaoMatrizCorteItemModel()
                    {
                        Quantidade = programacaoProducaoMatrizCorteItem.Quantidade,
                        QuantidadeVezes = programacaoProducaoMatrizCorteItem.QuantidadeVezes,
                        DescricaoTamanho = programacaoProducaoMatrizCorteItem.Tamanho.Descricao
                    };

                    matrizCorteModel.GridMatrizCorteItens.Add(matrizCorteItemModel);
                });

            var javaScriptSerializer = new JavaScriptSerializer();
            return javaScriptSerializer.Serialize(matrizCorteModel);
        }

        public virtual ProgramacaoProducaoMatrizCorte ObtenhaProgramacaoProducaoMatrizCorteCompleto(ProgramacaoProducaoMatrizCorte domain, FichaTecnica fichaTecnica)
        {
            var matrizCorteDomain = domain;

            if (domain.ProgramacaoProducaoMatrizCorteItens.IsEmpty())
            {
                fichaTecnica.FichaTecnicaMatriz.Grade.Tamanhos.Keys.ForEach(tamanho =>
                {
                    var programacaoProducaoMatrizCorteItem = new ProgramacaoProducaoMatrizCorteItem
                    {
                        Tamanho = tamanho,
                        Quantidade = 0,
                        QuantidadeVezes = 0
                    };
                    matrizCorteDomain.ProgramacaoProducaoMatrizCorteItens.Add(programacaoProducaoMatrizCorteItem);
                });
            }

            return domain;
        }

        [HttpPost, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(ProgramacaoProducaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = _programacaoProducaoRepository.Get(model.Id);

                    if (model.Quantidade != domain.Quantidade)
                    {
                        this.AddInfoMessage(
                            "Ao alterar a quantidade de peças programadas é necessário recalcular manualmente a quantidade de todos os materiais.");
                    }

                    if (domain.SituacaoProgramacaoProducao != SituacaoProgramacaoProducao.Iniciada)
                    {
                        this.AddInfoMessage("Nâo é possível alterar uma programação produção com alguma reserva ou requisição de material");
                        
                        return RedirectToAction("Editar", new { domain.Id }); 
                    }

                    domain = Mapper.Unflat(model, domain);
                    domain.Colecao = _colecaoRepository.Load(model.Colecao);
                    domain.Funcionario = _pessoaRepository.Load(model.Responsavel);
                    domain.Quantidade = model.Quantidade;

                    model.GridProgramacaoProducaoItens.ForEach(modelItem => EditarProgramacaoProducaoItem(domain, modelItem));

                    VerifiqueExcluirProgramacaoProducaoItem(domain, model);

                    _programacaoProducaoRepository.SaveOrUpdate(domain);

                    this.AddSuccessMessage("Programação da produção atualizada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty,
                        "Ocorreu um erro ao salvar a programação da produção. Confira se os dados foram informados corretamente: " +
                        exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }
            return View(model);
        }

        private void EditarProgramacaoProducaoItem(ProgramacaoProducao domain, ProgramacaoProducaoItemModel modelItem)
        {
            var javaScriptSerializer = new JavaScriptSerializer();
            var programacaoProducaoMatrizCorteModel =
                javaScriptSerializer.Deserialize<ProgramacaoProducaoMatrizCorteModel>(
                    modelItem.MatrizCorteJson);
            var programacaoProducaoMatrizCorte = new ProgramacaoProducaoMatrizCorte()
            {
                TipoEnfestoTecido =
                    (TipoEnfestoTecido)programacaoProducaoMatrizCorteModel.TipoEnfestoTecido
            };

            programacaoProducaoMatrizCorteModel.GridMatrizCorteItens.ForEach(modelMatrizCorteItem =>
            {
                var programacaoProducaoMatrizCorteItem = new ProgramacaoProducaoMatrizCorteItem()
                {
                    Quantidade = modelMatrizCorteItem.Quantidade.GetValueOrDefault(),
                    QuantidadeVezes = modelMatrizCorteItem.QuantidadeVezes.GetValueOrDefault(),
                    Tamanho =
                        _tamanhoRepository.Get(x => x.Descricao == modelMatrizCorteItem.DescricaoTamanho)
                };

                programacaoProducaoMatrizCorte.ProgramacaoProducaoMatrizCorteItens.Add(
                    programacaoProducaoMatrizCorteItem);
            });

            if (modelItem.Id.HasValue && modelItem.Id != 0)
            {
                var programacaoProducaoItem =
                    domain.ProgramacaoProducaoItems.FirstOrDefault(x => x.Id == modelItem.Id);
                programacaoProducaoItem.Quantidade = modelItem.Quantidade.GetValueOrDefault();

                _programacaoProducaoMatrizCorteRepository.Delete(
                    programacaoProducaoItem.ProgramacaoProducaoMatrizCorte);

                programacaoProducaoItem.ProgramacaoProducaoMatrizCorte = programacaoProducaoMatrizCorte;
            }
            else
            {
                var programacaoProducaoItem = new ProgramacaoProducaoItem()
                {
                    FichaTecnica = _fichaTecnicaRepository.Get(y => y.Referencia == modelItem.Referencia),
                    Quantidade = modelItem.Quantidade.GetValueOrDefault(),
                    ProgramacaoProducaoMatrizCorte = programacaoProducaoMatrizCorte
                };

                domain.ProgramacaoProducaoItems.Add(programacaoProducaoItem);

                programacaoProducaoItem.FichaTecnica.MateriaisConsumo.ForEach(materialConsumoFichaTecnica =>
                {
                    var programacaoProducaoMaterial = domain.ProgramacaoProducaoMateriais.FirstOrDefault(
                        x => x.Material.Id == materialConsumoFichaTecnica.Material.Id);

                    if (programacaoProducaoMaterial == null)
                    {
                        programacaoProducaoMaterial = new ProgramacaoProducaoMaterial
                        {
                            DepartamentoProducao = materialConsumoFichaTecnica.DepartamentoProducao,
                            Material = materialConsumoFichaTecnica.Material,
                            Quantidade = materialConsumoFichaTecnica.Quantidade * programacaoProducaoItem.Quantidade
                        };

                        domain.ProgramacaoProducaoMateriais.Add(programacaoProducaoMaterial);
                    }
                    else
                    {
                        programacaoProducaoMaterial.Quantidade += (materialConsumoFichaTecnica.Quantidade * programacaoProducaoItem.Quantidade);
                    }
                });

                programacaoProducaoItem.FichaTecnica.MateriaisConsumoVariacao.ForEach(materialConsumoVariacaoFichaTecnica =>
                {
                    var programacaoProducaoMaterial = domain.ProgramacaoProducaoMateriais.FirstOrDefault(
                        x => x.Material.Id == materialConsumoVariacaoFichaTecnica.Material.Id);

                    if (programacaoProducaoMaterial == null)
                    {
                        programacaoProducaoMaterial = new ProgramacaoProducaoMaterial
                        {
                            DepartamentoProducao = materialConsumoVariacaoFichaTecnica.DepartamentoProducao,
                            Material = materialConsumoVariacaoFichaTecnica.Material,
                            Quantidade = materialConsumoVariacaoFichaTecnica.Quantidade * programacaoProducaoItem.Quantidade
                        };

                        domain.ProgramacaoProducaoMateriais.Add(programacaoProducaoMaterial);
                    }
                    else
                    {
                        programacaoProducaoMaterial.Quantidade += (materialConsumoVariacaoFichaTecnica.Quantidade * programacaoProducaoItem.Quantidade);
                    }
                });
            }
        }

        private void VerifiqueExcluirProgramacaoProducaoItem(ProgramacaoProducao programacaoProducao, ProgramacaoProducaoModel model)
        {
            var listaExcluir = new List<ProgramacaoProducaoItem>();

            programacaoProducao.ProgramacaoProducaoItems.ForEach(programacaoProducaoItem =>
            {
                if (model.GridProgramacaoProducaoItens == null ||
                    model.GridProgramacaoProducaoItens.All(
                        x => x.Id != programacaoProducaoItem.Id && programacaoProducaoItem.Id != null))
                {
                    listaExcluir.Add(programacaoProducaoItem);
                }
            });

            foreach (var programacaoProducaoItem in listaExcluir)
            {
                programacaoProducao.ProgramacaoProducaoItems.Remove(programacaoProducaoItem);
            }
        }

        #endregion

        #region Excluir

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Excluir(long? id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = _programacaoProducaoRepository.Get(id);
                    _programacaoProducaoRepository.Delete(domain);
                    
                    this.AddSuccessMessage("Programação de produção excluída com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao excluir a programação de produção: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return RedirectToAction("Editar", new { id });
        }

        #endregion

        #region Index
        [PopulateViewData("PopulateViewDataPesquisa")]
        public virtual ActionResult Index()
        {
            var model = new PesquisaProgramacaoProducaoModel { ModoConsulta = "Listar" };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewDataPesquisa")]
        public virtual ActionResult Index(PesquisaProgramacaoProducaoModel model)
        {
            try
            {
                var filtros = new StringBuilder();

                var programacoesProducao = ObtenhaQueryFiltrada(model, filtros);

                // Se não é uma listagem, gerar o relatório
                var result = programacoesProducao.ToList();

                if (!result.Any())
                    return Json(new { Error = "Nenhum item encontrado." });

                #region Montar Relatório
                var report = new ListaProgramacaoProducaoReport { DataSource = result };

                if (filtros.Length > 2)
                    report.ReportParameters["Filtros"].Value = filtros.ToString().Substring(0, filtros.Length - 2);

                if (model.OrdenarPor != null)
                    report.Sortings.Add("=Fields." + model.OrdenarPor,
                                        model.OrdenarEm == "asc" ? SortDirection.Asc : SortDirection.Desc);

                #endregion

                var filename = report.ToByteStream().SaveFile(".pdf");

                return Json(new { Url = filename });
            }
            catch (Exception exception)
            {
                var message = exception.GetMessage();
                _logger.Info(message);

                if (HttpContext.Request.IsAjaxRequest())
                    return Json(new { Error = message });

                ModelState.AddModelError(string.Empty, message);
                return View(model);
            }
        }

        private IQueryable<ProgramacaoProducao> ObtenhaQueryFiltrada(PesquisaProgramacaoProducaoModel model, StringBuilder filtros)
        {
            var programacoesProducao = _programacaoProducaoRepository.Find();
            if (!string.IsNullOrWhiteSpace(model.Referencia))
            {
                programacoesProducao = programacoesProducao.Where(p => p.ProgramacaoProducaoItems.Any(item => item.FichaTecnica.Referencia == model.Referencia));
                filtros.AppendFormat("Referência: {0}, ", model.Referencia);
            }

            if (!string.IsNullOrWhiteSpace(model.Tag))
            {
                programacoesProducao = programacoesProducao.Where(p => p.ProgramacaoProducaoItems.Any(item => item.FichaTecnica.Tag == model.Tag));
                filtros.AppendFormat("Tag: {0}, ", model.Tag);
            }

            if (model.Ano.HasValue)
            {
                programacoesProducao = programacoesProducao.Where(p => p.ProgramacaoProducaoItems.Any(item => item.FichaTecnica.Ano == model.Ano));
                filtros.AppendFormat("Ano: {0}, ", model.Ano);
            }

            if (model.Lote.HasValue)
            {
                programacoesProducao = programacoesProducao.Where(p => p.Lote == model.Lote.GetValueOrDefault());
                filtros.AppendFormat("Lote: {0}, ", model.Lote);
            }

            if (model.AnoLote.HasValue)
            {
                programacoesProducao = programacoesProducao.Where(p => p.Ano == model.AnoLote.GetValueOrDefault());
                filtros.AppendFormat("Ano do Lote: {0}, ", model.AnoLote);
            }

            if (model.Colecao.HasValue)
            {
                programacoesProducao = programacoesProducao.Where(p => p.Colecao.Id == model.Colecao);
                filtros.AppendFormat("Coleção: {0}, ", _colecaoRepository.Get(model.Colecao.Value).Descricao);
            }

            if (model.DataCadastro.HasValue && model.DataCadastroAte.HasValue)
            {
                programacoesProducao = programacoesProducao.Where(p => p.Data.Date >= model.DataCadastro.Value
                                                         && p.Data.Date <= model.DataCadastroAte.Value);
                filtros.AppendFormat("Data de cadastro de '{0}' até '{1}', ",
                                     model.DataCadastro.Value.ToString("dd/MM/yyyy"),
                                     model.DataCadastroAte.Value.ToString("dd/MM/yyyy"));
            }

            if (model.SituacaoProgramacaoProducao.HasValue)
            {
                programacoesProducao = programacoesProducao.Where(p => p.SituacaoProgramacaoProducao == model.SituacaoProgramacaoProducao);
                
                filtros.AppendFormat("Situação: {0}", model.SituacaoProgramacaoProducao.GetValueOrDefault().EnumToString());
            }

            if (model.DataProgramada.HasValue && model.DataProgramadaAte.HasValue)
            {
                programacoesProducao = programacoesProducao.Where(p => p.DataProgramada.Date >= model.DataProgramada.Value
                                                         && p.DataProgramada.Date <= model.DataProgramadaAte.Value);
                filtros.AppendFormat("Data programada de '{0}' até '{1}', ",
                                     model.DataProgramada.Value.ToString("dd/MM/yyyy"),
                                     model.DataProgramadaAte.Value.ToString("dd/MM/yyyy"));
            }

            return programacoesProducao;
        }

        public virtual ActionResult ObtenhaListaGridModel([DataSourceRequest] DataSourceRequest request, PesquisaProgramacaoProducaoModel model)
        {
            try
            {
                var filtros = new StringBuilder();

                var programacoesProducao = ObtenhaQueryFiltrada(model, filtros);

                if (!request.Sorts.IsNullOrEmpty())
                {
                    foreach (SortDescriptor sortDescriptor in request.Sorts)
                    {
                        programacoesProducao = sortDescriptor.SortDirection.ToString() == "Descending"
                            ? programacoesProducao.OrderByDescending(ColunasOrdenacaoGrid[sortDescriptor.Member])
                            : programacoesProducao.OrderBy(ColunasOrdenacaoGrid[sortDescriptor.Member]);
                    }
                }

                programacoesProducao = programacoesProducao.OrderByDescending(o => o.DataAlteracao);

                var total = programacoesProducao.Count();

                if (request.Page > 0)
                {
                    programacoesProducao = programacoesProducao.Skip((request.Page - 1) * request.PageSize);
                }

                var resultado = programacoesProducao.Take(request.PageSize).ToList();

                var list = resultado.Select(p => new GridProgramacaoProducaoModel
                {
                    Id = p.Id.GetValueOrDefault(),
                    LoteAno = p.Lote.ToString() + '/' + p.Ano,
                    Colecao = p.Colecao.Descricao,
                    DataProgramada = p.DataProgramada.Date,
                    Responsavel = p.Funcionario.Nome,
                    QtdeFichasTecnicas = p.ProgramacaoProducaoItems.Count(),
                    QtdeProgramada = p.Quantidade,
                    SituacaoProgramacaoProducao = p.SituacaoProgramacaoProducao
                }).ToList();

                var valorPage = request.Page;
                request.Page = 1;
                var data = list.ToDataSourceResult(request);
                request.Page = valorPage;

                var result = new DataSourceResult()
                {
                    AggregateResults = data.AggregateResults,
                    Data = data.Data,
                    Total = total
                };

                return Json(result);
            }
            catch (Exception ex)
            {
                var message = ex.GetMessage();
                _logger.Info(message);

                return Json(new DataSourceResult { Errors = message });
            }
        }
        
        #endregion

        #region Requisição

        [PopulateViewData("PopulateViewDataRequisicao")]
        public virtual ActionResult ProgramacaoProducaoRequisicao(long id)
        {
            var domain = _programacaoProducaoRepository.Get(id);
            
            var model = new ProgramacaoProducaoRequisicaoModel
            {
                DataProgramada = domain.DataProgramada,
                Quantidade = domain.Quantidade,
                ColecaoProgramada = domain.Colecao.Descricao,
                LoteAno = domain.Lote + "/" + domain.Ano,
                SituacaoProgramacaoProducao = domain.SituacaoProgramacaoProducao.EnumToString(),
                GridItens = new List<GridProgramacaoProducaoRequisicaoModel>(),
                Fotos = domain.ProgramacaoProducaoItems.SelectMany(x => x.FichaTecnica.FichaTecnicaFotos.Select(p => new FotoTituloModel
                {
                    Foto = p.Arquivo.Nome.GetFileUrl(),
                    Titulo = x.FichaTecnica.Referencia
                }))
            };

            domain.ProgramacaoProducaoMateriais.ForEach(programacaoProducaoMaterial =>
            {
                if (!programacaoProducaoMaterial.Reservado)
                {
                    return;
                }

                var unidade = programacaoProducaoMaterial.ReservaMaterial != null ? programacaoProducaoMaterial.ReservaMaterial.Unidade.Id : 0;

                var modelMaterial = new GridProgramacaoProducaoRequisicaoModel()
                {
                    Id = programacaoProducaoMaterial.Id.GetValueOrDefault(),
                    DepartamentoProducao = programacaoProducaoMaterial.DepartamentoProducao.Id.ToString(),
                    Descricao = programacaoProducaoMaterial.Material.Descricao,
                    Referencia = programacaoProducaoMaterial.Material.Referencia,
                    UnidadeMedida = programacaoProducaoMaterial.Material.UnidadeMedida.Sigla,
                    Foto = (programacaoProducaoMaterial.Material.Foto != null ? programacaoProducaoMaterial.Material.Foto.Nome.GetFileUrl() : string.Empty),
                    GeneroCategoria = programacaoProducaoMaterial.Material.Subcategoria.Categoria.GeneroCategoria,
                    Requisitado = programacaoProducaoMaterial.Requisitado,
                    Editavel = !programacaoProducaoMaterial.Requisitado,
                    Quantidade = programacaoProducaoMaterial.Quantidade,
                    Unidade = unidade
                };

                model.GridItens.Add(modelMaterial);
            });

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult ProgramacaoProducaoRequisicao(ProgramacaoProducaoRequisicaoModel model)
        {
            foreach (var modelValue in ModelState.Values)
            {
                modelValue.Errors.Clear();
            }

            try
            {
                var domain = _programacaoProducaoRepository.Get(model.Id);

                var requisicoesMaterial = new Dictionary<long, RequisicaoMaterial>();

                model.GridItens.ForEach(modelItem =>
                {
                    if (modelItem.Editavel && modelItem.Requisitado)
                    {
                        var programacaoProducaoMaterial = domain.ProgramacaoProducaoMateriais.FirstOrDefault(x => x.Id == modelItem.Id);
                        var unidade = programacaoProducaoMaterial.ReservaMaterial.Unidade;

                        if (requisicoesMaterial.ContainsKey(unidade.Id.GetValueOrDefault()))
                        {
                            var requisicaoMaterial = requisicoesMaterial[unidade.Id.GetValueOrDefault()];
                            var requisicaoMaterialItem = ObtenhaRequisicaoMaterialItem(modelItem, programacaoProducaoMaterial);
                            requisicaoMaterial.RequisicaoMaterialItems.Add(requisicaoMaterialItem);
                            requisicaoMaterial.ReservaMateriais.Add(programacaoProducaoMaterial.ReservaMaterial);
                        }
                        else
                        {
                            var requisicaoMaterial = ObtenhaRequisicaoMaterial(model, modelItem, programacaoProducaoMaterial, domain);
                            requisicoesMaterial[unidade.Id.GetValueOrDefault()] = requisicaoMaterial;
                        }

                        programacaoProducaoMaterial.Requisitado = true;
                        programacaoProducaoMaterial.ReservaMaterial = null;
                    }
                });
               
                _programacaoProducaoRepository.SaveOrUpdate(domain);

                AtualizeSituacaoProgramacaoProducao(domain);

                requisicoesMaterial.Values.ForEach(requisicaoMaterial => _requisicaoMaterialRepository.Save(requisicaoMaterial));

                this.AddSuccessMessage("Requisições realizadas com sucesso.\n");
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty,
                    "Ocorreu um erro ao cadastrar a requisição. Confira se os dados foram informados corretamente: " +
                    exception.Message);
                _logger.Info(exception.GetMessage());
            }

            return View(model);
        }

        private void AtualizeSituacaoProgramacaoProducao(ProgramacaoProducao programacaoProducao)
        {
            if (programacaoProducao.ProgramacaoProducaoMateriais.Any(x => x.Requisitado))
            {
                programacaoProducao.SituacaoProgramacaoProducao = SituacaoProgramacaoProducao.EmRequisicao;
            }
            else if (programacaoProducao.ProgramacaoProducaoMateriais.All(x => !x.Reservado && !x.Requisitado))
            {
                programacaoProducao.SituacaoProgramacaoProducao = SituacaoProgramacaoProducao.Iniciada;
            }
            else if (programacaoProducao.ProgramacaoProducaoMateriais.All(x => !x.Requisitado) && programacaoProducao.ProgramacaoProducaoMateriais.Any(x => x.Reservado))
            {
                programacaoProducao.SituacaoProgramacaoProducao = SituacaoProgramacaoProducao.EmReserva;
            }
        }

        private RequisicaoMaterial ObtenhaRequisicaoMaterial(ProgramacaoProducaoRequisicaoModel model, GridProgramacaoProducaoRequisicaoModel modelMaterial, ProgramacaoProducaoMaterial programacaoProducaoMaterial,
            ProgramacaoProducao programacaoProducao)
        {
            var unidade = programacaoProducaoMaterial.ReservaMaterial.Unidade;

            var requisicaoMaterial = new RequisicaoMaterial
            {
                CentroCusto = _centroCustoRepository.Get(model.CentroCusto),
                Data = DateTime.Now,
                Numero = ProximoNumeroRequisicaoMaterial(),
                Origem = programacaoProducao.Lote + "/" + programacaoProducao.Ano,
                Requerente = _pessoaRepository.Get(model.Requerente),
                TipoItem = _tipoItemRepository.Find().FirstOrDefault(x => x.Descricao == "MATÉRIA-PRIMA"),
                UnidadeRequisitada = unidade,
                SituacaoRequisicaoMaterial = SituacaoRequisicaoMaterial.NaoAtendido,
                UnidadeRequerente = _pessoaRepository.Get(model.UnidadeRequerente)
            };

            requisicaoMaterial.ReservaMateriais.Add(programacaoProducaoMaterial.ReservaMaterial);
            var requisicaoMaterialItem = ObtenhaRequisicaoMaterialItem(modelMaterial, programacaoProducaoMaterial);
            requisicaoMaterial.RequisicaoMaterialItems.Add(requisicaoMaterialItem);

            return requisicaoMaterial;
        }

        private RequisicaoMaterialItem ObtenhaRequisicaoMaterialItem(GridProgramacaoProducaoRequisicaoModel model, ProgramacaoProducaoMaterial programacaoProducaoMaterial)
        {
            return new RequisicaoMaterialItem
            {
                Material = programacaoProducaoMaterial.Material,
                QuantidadeSolicitada = model.Quantidade,
                SituacaoRequisicaoMaterial = SituacaoRequisicaoMaterial.NaoAtendido
            };
        }

        #endregion

        #region Materiais

        [PopulateViewData("PopulateViewDataMateriais")]
        public virtual ActionResult MateriaisProgramacaoProducao(long id)
        {
            var domain = _programacaoProducaoRepository.Get(id);

            var userId = FashionSecurity.GetLoggedUserId();
            var usuario = _usuarioRepository.Get(userId);
            var funcionarioId = usuario.Funcionario != null ? usuario.Funcionario.Id : null;

            if (funcionarioId == null)
            {
                this.AddErrorMessage("O usuário logado não possui funcionário associado a ele.");
                return RedirectToAction("Index");
            }

            var pessoaLogada = _pessoaRepository.Get(funcionarioId);
            
            var model = new ProgramacaoProducaoMateriaisModel
            {
                DataProgramada = domain.DataProgramada,
                Quantidade = domain.Quantidade,
                LoteAno = domain.Lote + "/" + domain.Ano,
                Colecao = domain.Colecao.Descricao,
                SituacaoProgramacaoProducao = domain.SituacaoProgramacaoProducao.EnumToString(),
                GridItens = new List<GridProgramacaoProducaoMaterialModel>(),
                Fotos = domain.ProgramacaoProducaoItems.SelectMany(x => x.FichaTecnica.FichaTecnicaFotos.Select(p => new FotoTituloModel
                {
                    Foto = p.Arquivo.Nome.GetFileUrl(), 
                    Titulo = x.FichaTecnica.Referencia
                }))
            };

            domain.ProgramacaoProducaoMateriais.ForEach(programacaoProducaoMaterial =>
            {
                var unidade = programacaoProducaoMaterial.ReservaMaterial != null ? programacaoProducaoMaterial.ReservaMaterial.Unidade.Id : 0;

                var editavel = (programacaoProducaoMaterial.Responsavel == null ||
                                programacaoProducaoMaterial.Responsavel.Id == pessoaLogada.Id)
                               && !programacaoProducaoMaterial.Requisitado;

                var modelMaterial = new GridProgramacaoProducaoMaterialModel()
                {
                    Id = programacaoProducaoMaterial.Id.GetValueOrDefault(),
                    DepartamentoProducao = programacaoProducaoMaterial.DepartamentoProducao.Id.ToString(),
                    Descricao = programacaoProducaoMaterial.Material.Descricao,
                    Referencia = programacaoProducaoMaterial.Material.Referencia,
                    UnidadeMedida = programacaoProducaoMaterial.Material.UnidadeMedida.Sigla,
                    Foto = (programacaoProducaoMaterial.Material.Foto != null ? programacaoProducaoMaterial.Material.Foto.Nome.GetFileUrl() : string.Empty),
                    GeneroCategoria = programacaoProducaoMaterial.Material.Subcategoria.Categoria.GeneroCategoria,
                    Reservado = programacaoProducaoMaterial.Reservado,
                    Quantidade = programacaoProducaoMaterial.Quantidade,
                    Unidade = unidade,
                    Editavel = editavel
                };

                model.GridItens.Add(modelMaterial);
            });

            return View(model);
        }
        
        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult MateriaisProgramacaoProducao(ProgramacaoProducaoMateriaisModel model)
        {
            foreach (var modelValue in ModelState.Values)
            {
                modelValue.Errors.Clear();
            }

            try
            {
                var domain = _programacaoProducaoRepository.Get(model.Id);

                model.GridItens.ForEach(modelItem =>
                {
                    if (modelItem.Id == 0)
                    {
                        CrieNovoProgramacaoProducaoMaterial(modelItem, domain);
                    }
                    else
                    {
                        if (modelItem.Editavel)
                        {
                            EditarProgramacaoProducaoMaterial(modelItem, domain);    
                        }
                    }
                });

                var listaExcluir = new List<ProgramacaoProducaoMaterial>();

                domain.ProgramacaoProducaoMateriais.ForEach(programacaoProducaoMaterial =>
                {
                    if (model.GridItens == null ||
                        model.GridItens.All(
                            x => x.Id != programacaoProducaoMaterial.Id && programacaoProducaoMaterial.Id != null))
                    {
                        listaExcluir.Add(programacaoProducaoMaterial);
                    }
                });
                    
                var mensagemNaoPodeExcluir = String.Empty;
                foreach (var programacaoProducaoMaterial in listaExcluir)
                {
                    if (programacaoProducaoMaterial.Reservado && programacaoProducaoMaterial.ReservaMaterial.SituacaoReservaMaterial ==
                        SituacaoReservaMaterial.NaoAtendida)
                    {
                        _reservaMaterialRepository.Delete(programacaoProducaoMaterial.ReservaMaterial);
                        var reservaMaterialItem = programacaoProducaoMaterial.ReservaMaterial.ReservaMaterialItems.First();
                        ReservaEstoqueMaterial.AtualizeReservaEstoqueMaterial(reservaMaterialItem.QuantidadeReserva * -1, reservaMaterialItem.Material, programacaoProducaoMaterial.ReservaMaterial.Unidade, _reservaEstoqueMaterialRepository);

                        domain.ProgramacaoProducaoMateriais.Remove(programacaoProducaoMaterial);
                    } else if (!programacaoProducaoMaterial.Reservado && !programacaoProducaoMaterial.Requisitado)
                    {
                        domain.ProgramacaoProducaoMateriais.Remove(programacaoProducaoMaterial);
                    }
                    else
                    {
                        mensagemNaoPodeExcluir += "Não foi possível excluir o material de referência: " 
                            + programacaoProducaoMaterial.Material.Referencia + " já atendido por uma requisição.\n";
                    }
                }

                AtualizeSituacaoProgramacaoProducao(domain);

                _programacaoProducaoRepository.SaveOrUpdate(domain);
                Framework.UnitOfWork.Session.Current.Flush();

                this.AddSuccessMessage("Materiais da programação da produção atualizados com sucesso.\n" + mensagemNaoPodeExcluir);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty,
                    "Ocorreu um erro ao atualizar os materiais da programação de produção. Confira se os dados foram informados corretamente: " +
                    exception.Message);
                _logger.Info(exception.GetMessage());
            }
            
            return View(model);
        }

        private void CrieNovoProgramacaoProducaoMaterial(GridProgramacaoProducaoMaterialModel modelItem, ProgramacaoProducao domain)
        {
            var programacaoProducaoMaterial = new ProgramacaoProducaoMaterial()
            {
                Material = _materialRepository.Get(x => x.Referencia == modelItem.Referencia),
                Quantidade = modelItem.Quantidade,
                ReservaMaterial = modelItem.Reservado ? ObtenhaNovaReservaMaterial(modelItem, domain) : null,
                Reservado = modelItem.Reservado,
                DepartamentoProducao = _departamentoProducaoRepository.Load(Convert.ToInt64(modelItem.DepartamentoProducao))
            };

            if (programacaoProducaoMaterial.ReservaMaterial != null)
            {
                var userId = FashionSecurity.GetLoggedUserId();
                var usuario = _usuarioRepository.Get(userId);
                var funcionarioId = usuario.Funcionario != null ? usuario.Funcionario.Id : null;
                programacaoProducaoMaterial.Responsavel = _pessoaRepository.Get(funcionarioId);
            }

            domain.ProgramacaoProducaoMateriais.Add(programacaoProducaoMaterial);
        }

        private void EditarProgramacaoProducaoMaterial(GridProgramacaoProducaoMaterialModel modelItem, ProgramacaoProducao domain)
        {
            var programacaoProducaoMaterial = domain.ProgramacaoProducaoMateriais.First(x => x.Id == modelItem.Id);
            programacaoProducaoMaterial.Quantidade = modelItem.Quantidade;
            programacaoProducaoMaterial.DepartamentoProducao = _departamentoProducaoRepository.Load(Convert.ToInt64(modelItem.DepartamentoProducao));
            
            if (modelItem.Reservado)
            {
                if (programacaoProducaoMaterial.ReservaMaterial != null)
                {
                    EditarReservaMaterial(programacaoProducaoMaterial, modelItem, domain);
                }
                else
                {
                    programacaoProducaoMaterial.ReservaMaterial = ObtenhaNovaReservaMaterial(modelItem, domain);
                }

                programacaoProducaoMaterial.Reservado = true;
            }
            else
            {
                if (programacaoProducaoMaterial.ReservaMaterial != null)
                {
                    _reservaMaterialRepository.Delete(programacaoProducaoMaterial.ReservaMaterial);

                    var reservaMaterialItem = programacaoProducaoMaterial.ReservaMaterial.ReservaMaterialItems.First(x => x.Material.Referencia == modelItem.Referencia);
                    ReservaEstoqueMaterial.AtualizeReservaEstoqueMaterial(reservaMaterialItem.QuantidadeReserva * -1, reservaMaterialItem.Material, programacaoProducaoMaterial.ReservaMaterial.Unidade, _reservaEstoqueMaterialRepository);

                    programacaoProducaoMaterial.ReservaMaterial = null;
                    programacaoProducaoMaterial.Reservado = false;
                    programacaoProducaoMaterial.Responsavel = null;
                }
            }

            if (programacaoProducaoMaterial.ReservaMaterial != null && programacaoProducaoMaterial.Responsavel == null)
            {
                var userId = FashionSecurity.GetLoggedUserId();
                var usuario = _usuarioRepository.Get(userId);
                var funcionarioId = usuario.Funcionario != null ? usuario.Funcionario.Id : null;
                programacaoProducaoMaterial.Responsavel = _pessoaRepository.Get(funcionarioId);
            }
        }

        private ReservaMaterial ObtenhaNovaReservaMaterial(GridProgramacaoProducaoMaterialModel modelItem, ProgramacaoProducao domain)
        {
            var material = _materialRepository.Get(x => x.Referencia == modelItem.Referencia);
            var unidade = _pessoaRepository.Load(Convert.ToInt64(modelItem.Unidade));

            var reservaMaterial = new ReservaMaterial()
            {
                Colecao = domain.Colecao,
                Numero = ProximoNumeroReservaMaterial(),
                Data = DateTime.Now,
                DataProgramacao = domain.DataProgramada,
                Requerente = domain.Funcionario,
                Unidade = unidade,
                ReferenciaOrigem = domain.Lote + "/"+ domain.Ano,
                SituacaoReservaMaterial = SituacaoReservaMaterial.NaoAtendida
            };

            var reservaMaterialItem = new ReservaMaterialItem()
            {
                Material = material,
                QuantidadeReserva = modelItem.Quantidade,
                SituacaoReservaMaterial = SituacaoReservaMaterial.NaoAtendida
            };

            reservaMaterial.ReservaMaterialItems.Add(reservaMaterialItem);
            ReservaEstoqueMaterial.AtualizeReservaEstoqueMaterial(modelItem.Quantidade, material, unidade, _reservaEstoqueMaterialRepository);

            return reservaMaterial;
        }

        private void EditarReservaMaterial(ProgramacaoProducaoMaterial programacaoProducaoMaterial, GridProgramacaoProducaoMaterialModel modelItem, ProgramacaoProducao domain)
        {
            var reservaMaterial = programacaoProducaoMaterial.ReservaMaterial;
            reservaMaterial.Unidade = _pessoaRepository.Load(Convert.ToInt64(modelItem.Unidade));
            var reservaMaterialItem = reservaMaterial.ReservaMaterialItems.First(x => x.Material.Referencia == modelItem.Referencia);

            var valorAtual = reservaMaterialItem.QuantidadeReserva;
            var valorNovo = modelItem.Quantidade;

            reservaMaterialItem.QuantidadeReserva = modelItem.Quantidade;
            
            ReservaEstoqueMaterial.AtualizeReservaEstoqueMaterial(valorNovo - valorAtual, reservaMaterialItem.Material, reservaMaterial.Unidade, _reservaEstoqueMaterialRepository);
        }

        #endregion

        #region PopulateViewDataPesquisa

        protected void PopulateViewDataPesquisa(PesquisaProgramacaoProducaoModel model)
        {
            var colecaos = _colecaoRepository.Find(p => p.Ativo).ToList();
            ViewBag.Colecao = colecaos.ToSelectList("Descricao", model.Colecao);
            ViewBag.OrdenarPor = new SelectList(ColunasOrdenacaoRelatorio, "value", "key");
        }
        #endregion

        #region PopulateViewData

        protected void PopulateViewData(ProgramacaoProducaoModel model)
        {
            var colecaos = _colecaoRepository.Find(p => p.Ativo).ToList();
            ViewBag.Colecao = colecaos.ToSelectList("Descricao", model.Colecao);
        }
        #endregion

        protected void PopulateViewDataMateriais(ProgramacaoProducaoMateriaisModel model)
        {
            var generoCategorias = (from Enum e in Enum.GetValues(typeof(GeneroCategoria))
                                    let d = e.GetDisplay()
                                    select new { Id = Convert.ToInt32(e), Name = d != null ? d.Name : e.ToString() }).ToList();

            ViewBag.GeneroCategoriaDicionarioJson = generoCategorias.ToDictionary(k => k.Id.ToString(), e => e.Name).FromDictionaryToJson();

            var departamentoProducaos = _departamentoProducaoRepository.Find(p => p.Ativo).ToList();
            ViewBag.DepartamentoProducaos = departamentoProducaos.Select(s => new { Id = s.Id.ToString(), s.Nome }).OrderBy(x => x.Nome);
            ViewBag.DepartamentoProducaoDicionarioJson = departamentoProducaos.ToDictionary(k => k.Id.ToString(), e => e.Nome).FromDictionaryToJson();

            var unidades = _pessoaRepository.Find(p => p.Unidade != null || p.Unidade.Ativo).ToList();
            ViewBag.Unidades = unidades.Select(s => new { Id = s.Id.ToString(), s.NomeFantasia }).OrderBy(x => x.NomeFantasia);
            ViewBag.UnidadeGeral = unidades.ToSelectList("NomeFantasia");
            ViewBag.UnidadeDicionarioJson = unidades.ToDictionary(k => k.Id.ToString(), e => e.NomeFantasia).FromDictionaryToJson();
        }

        protected void PopulateViewDataRequisicao(ProgramacaoProducaoRequisicaoModel model)
        {
            var generoCategorias = (from Enum e in Enum.GetValues(typeof(GeneroCategoria))
                                    let d = e.GetDisplay()
                                    select new { Id = Convert.ToInt32(e), Name = d != null ? d.Name : e.ToString() }).ToList();

            ViewBag.GeneroCategoriaDicionarioJson = generoCategorias.ToDictionary(k => k.Id.ToString(), e => e.Name).FromDictionaryToJson();

            var departamentoProducaos = _departamentoProducaoRepository.Find(p => p.Ativo).ToList();
            ViewBag.DepartamentoProducaos = departamentoProducaos.Select(s => new { Id = s.Id.ToString(), s.Nome }).OrderBy(x => x.Nome);
            ViewBag.DepartamentoProducaoDicionarioJson = departamentoProducaos.ToDictionary(k => k.Id.ToString(), e => e.Nome).FromDictionaryToJson();

            var unidades = _pessoaRepository.Find(p => p.Unidade != null || p.Unidade.Ativo).OrderBy(o => o.NomeFantasia).ToList();
            ViewBag.Unidades = unidades.Select(s => new { Id = s.Id.ToString(), s.NomeFantasia }).OrderBy(x => x.NomeFantasia);
            ViewBag.UnidadeDicionarioJson = unidades.ToDictionary(k => k.Id.ToString(), e => e.NomeFantasia).FromDictionaryToJson();

            ViewBag.UnidadeRequerente = unidades.ToSelectList("NomeFantasia", model.UnidadeRequerente);

            var centroCustos = _centroCustoRepository.Find(x => x.Ativo).OrderBy(o => o.Nome).ToList();
            ViewBag.CentroCusto = centroCustos.ToSelectList("Nome", model.CentroCusto);
        }
        
        private long ObtenhaProximoNumero()
        {
            var ultimoNumero = _ultimoNumeroRepository.Get(x => x.NomeTabela == "programacaoproducao");
            long numero = 0;

            if (ultimoNumero != null)
            {
                ultimoNumero = ObtenhaProximoNumeroDisponivel(ultimoNumero);
                numero = ultimoNumero.Numero;
                _ultimoNumeroRepository.SaveOrUpdate(ultimoNumero);
            }
            else
            {
                ultimoNumero = new UltimoNumero {NomeTabela = "programacaoproducao", Numero = 1};
                ObtenhaProximoNumeroDisponivel(ultimoNumero);
                _ultimoNumeroRepository.SaveOrUpdate(ultimoNumero);
                numero = ultimoNumero.Numero;
            }

            return numero;
        }

        private UltimoNumero ObtenhaProximoNumeroDisponivel(UltimoNumero ultimoNumero)
        {
            ultimoNumero.Numero++;
            var programacaoProducao = _programacaoProducaoRepository.Get(x => x.Lote == ultimoNumero.Numero);
            return programacaoProducao != null ? ObtenhaProximoNumeroDisponivel(ultimoNumero) : ultimoNumero;
        }

        private long ProximoNumeroReservaMaterial()
        {
            try
            {
                if (_proximoNumeroReservaMaterial != null)
                {
                    _proximoNumeroReservaMaterial++;
                    return _proximoNumeroReservaMaterial.GetValueOrDefault();
                }
                
                _proximoNumeroReservaMaterial = _reservaMaterialRepository.Find().Max(p => p.Numero) + 1;
                return _proximoNumeroReservaMaterial.GetValueOrDefault();
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public string ObtenhaUrlFotoFichaTecnica(FichaTecnica fichaTecnica)
        {
            if (fichaTecnica.FichaTecnicaFotos.Any(x => x.Padrao))
            {
                return fichaTecnica.FichaTecnicaFotos.Where(x => x.Padrao).ElementAt(0).Arquivo.Nome.GetFileUrl();
            }

            return fichaTecnica.FichaTecnicaFotos.Any() ? fichaTecnica.FichaTecnicaFotos.ElementAt(0).Arquivo.Nome.GetFileUrl() : null;
        }

        #region PesquisarVarios
        [ChildActionOnly]//OutputCache(Duration = 3600)
        public virtual ActionResult MatrizCorte()
        {
            return PartialView(new ProgramacaoProducaoMatrizCorteModel());
        }
        #endregion


        private long ProximoNumeroRequisicaoMaterial()
        {
            try
            {
                if (_proximoNumeroRequisicaoMaterial.HasValue)
                {
                    _proximoNumeroRequisicaoMaterial += 1;
                }
                else
                {
                    _proximoNumeroRequisicaoMaterial = _requisicaoMaterialRepository.Find().Max(p => p.Numero) + 1;
                }

                return _proximoNumeroRequisicaoMaterial.Value;
            }
            catch (Exception)
            {
                return 1;
            }
        }
    }
}