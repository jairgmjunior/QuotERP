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
    public partial class ProducaoController : BaseController
    {
        #region Variaveis
        private readonly IRepository<Domain.Producao.Producao> _producaoRepository;
        private readonly IRepository<RemessaProducao> _remessaProducaoRepository;
        private readonly IRepository<FichaTecnica> _fichaTecnicaRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<Tamanho> _tamanhoRepository;
        private readonly IRepository<UltimoNumero> _ultimoNumeroRepository;
        private readonly IRepository<DepartamentoProducao> _departamentoProducaoRepository;
        private readonly IRepository<Usuario> _usuarioRepository;

        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public ProducaoController(ILogger logger, IRepository<Domain.Producao.Producao> producaoRepository,
            IRepository<FichaTecnica> fichaTecnicaRepository, IRepository<Tamanho> tamanhoRepository, 
            IRepository<UltimoNumero> ultimoNumeroRepository, IRepository<RemessaProducao> remessaProducaoRepository,
            IRepository<Pessoa> pessoaRepository, IRepository<DepartamentoProducao> departamentoProducaoRepository,
            IRepository<Usuario> usuarioRepository)
        {
            _producaoRepository = producaoRepository;
            _fichaTecnicaRepository = fichaTecnicaRepository;
            _tamanhoRepository = tamanhoRepository;
            _ultimoNumeroRepository = ultimoNumeroRepository;
            _remessaProducaoRepository = remessaProducaoRepository;
            _pessoaRepository = pessoaRepository;
            _departamentoProducaoRepository = departamentoProducaoRepository;
            _usuarioRepository = usuarioRepository;
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
            return View(new ProducaoModel());
        }

        [HttpPost, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(ProducaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<Domain.Producao.Producao>(model);
                    domain.RemessaProducao = _remessaProducaoRepository.Load(model.RemessaProducao);
                    domain.Data = DateTime.Now.Date;
                    domain.DataAlteracao = DateTime.Now.Date;
                    domain.SituacaoProducao = SituacaoProducao.Iniciada;
                    
                    if (model.Lote.HasValue)
                    {
                        domain.Lote = model.Lote.GetValueOrDefault();
                        domain.Ano = model.Ano.GetValueOrDefault();
                    } else
                    {
                        domain.Lote = ObtenhaProximoNumero();
                        domain.Ano = DateTime.Now.Year;   
                    }

                    model.GridProducaoItens.ForEach(modelItem =>
                    {
                        var producaoItem = new ProducaoItem()
                        {
                            FichaTecnica = _fichaTecnicaRepository.Get(y => y.Referencia == modelItem.Referencia)
                        };

                        domain.ProducaoItens.Add(producaoItem);
                    });


                    _producaoRepository.SaveOrUpdate(domain);

                    this.AddSuccessMessage("Produção cadastrada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar a produção. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }
        
        #endregion
        
        #region Editar

        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(long id)
        {
            var domain = _producaoRepository.Get(id);
            var model = Mapper.Flat<ProducaoModel>(domain);
            model.GridProducaoItens = new List<ProducaoItemModel>();
            
            domain.ProducaoItens.ForEach(producaoItem =>
            {
                var modelItem = new ProducaoItemModel
                {
                    Descricao = producaoItem.FichaTecnica.Descricao,
                    Estilista = producaoItem.FichaTecnica.Estilista.Nome,
                    Foto = ObtenhaUrlFotoFichaTecnica(producaoItem.FichaTecnica),
                    Id = producaoItem.Id,
                    Referencia = producaoItem.FichaTecnica.Referencia,
                    TagAno = producaoItem.FichaTecnica.Tag + '/' + producaoItem.FichaTecnica.Ano,
                };
                model.GridProducaoItens.Add(modelItem);
            });

            return View(model);
        }

        [HttpPost, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(ProducaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = _producaoRepository.Get(model.Id);
                    
                    var lote = domain.Lote;
                    var ano = domain.Ano;

                    domain = Mapper.Unflat(model, domain);
                    domain.Lote = lote;
                    domain.Ano = ano;
                    domain.RemessaProducao = _remessaProducaoRepository.Load(model.RemessaProducao);

                    model.GridProducaoItens.ForEach(modelItem => EditarProducaoItem(domain, modelItem));

                    VerifiqueExcluirProducaoItem(domain, model);
                    
                    _producaoRepository.SaveOrUpdate(domain);

                    this.AddSuccessMessage("Produção atualizada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty,
                        "Ocorreu um erro ao salvar a produção. Confira se os dados foram informados corretamente: " +
                        exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }
            return View(model);
        }
        
        private void EditarProducaoItem(Domain.Producao.Producao domain, ProducaoItemModel modelItem)
        {
            if (!modelItem.Id.HasValue || modelItem.Id == 0)
            {
                var programacaoProducaoItem = new ProducaoItem()
                {
                    FichaTecnica = _fichaTecnicaRepository.Get(y => y.Referencia == modelItem.Referencia),
                };

                domain.ProducaoItens.Add(programacaoProducaoItem);
            }
        }

        private void VerifiqueExcluirProducaoItem(Domain.Producao.Producao producao, ProducaoModel model)
        {
            var listaExcluir = new List<ProducaoItem>();

            producao.ProducaoItens.ForEach(producaoItem =>
            {
                if (model.GridProducaoItens == null ||
                    model.GridProducaoItens.All(
                        x => x.Id != producaoItem.Id && producaoItem.Id != null))
                {
                    listaExcluir.Add(producaoItem);
                }
            });

            foreach (var producaoItem in listaExcluir)
            {
                producao.ProducaoItens.Remove(producaoItem);
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
                    var domain = _producaoRepository.Get(id);
                    _producaoRepository.Delete(domain);
                    
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
            var model = new PesquisaProducaoModel { ModoConsulta = "Listar" };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewDataPesquisa")]
        public virtual ActionResult Index(PesquisaProducaoModel model)
        {
            try
            {
                var filtros = new StringBuilder();

                var producoes = ObtenhaQueryFiltrada(model, filtros);

                // Se não é uma listagem, gerar o relatório
                var result = producoes.ToList();

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

        private IQueryable<Domain.Producao.Producao> ObtenhaQueryFiltrada(PesquisaProducaoModel model, StringBuilder filtros)
        {
            var producoes = _producaoRepository.Find();
            if (!string.IsNullOrWhiteSpace(model.Referencia))
            {
                producoes = producoes.Where(p => p.ProducaoItens.Any(item => item.FichaTecnica.Referencia == model.Referencia));
                filtros.AppendFormat("Referência: {0}, ", model.Referencia);
            }

            if (!string.IsNullOrWhiteSpace(model.Tag))
            {
                producoes = producoes.Where(p => p.ProducaoItens.Any(item => item.FichaTecnica.Tag == model.Tag));
                filtros.AppendFormat("Tag: {0}, ", model.Tag);
            }

            if (model.Ano.HasValue)
            {
                producoes = producoes.Where(p => p.ProducaoItens.Any(item => item.FichaTecnica.Ano == model.Ano));
                filtros.AppendFormat("Ano: {0}, ", model.Ano);
            }

            if (model.Lote.HasValue)
            {
                producoes = producoes.Where(p => p.Lote == model.Lote.GetValueOrDefault());
                filtros.AppendFormat("Lote: {0}, ", model.Lote);
            }

            if (model.AnoLote.HasValue)
            {
                producoes = producoes.Where(p => p.Ano == model.AnoLote.GetValueOrDefault());
                filtros.AppendFormat("Ano do Lote: {0}, ", model.AnoLote);
            }

            if (model.RemessaProducao.HasValue)
            {
                producoes = producoes.Where(p => p.RemessaProducao.Id == model.RemessaProducao);
                filtros.AppendFormat("Remessa: {0}, ", _remessaProducaoRepository.Get(model.RemessaProducao.Value).Descricao);
            }

            if (model.DataCadastro.HasValue && !model.DataCadastroAte.HasValue)
            {
                producoes = producoes.Where(p => p.Data.Date >= model.DataCadastro.Value);

                filtros.AppendFormat("Data de cadastro de '{0}', ", model.DataCadastro.Value.ToString("dd/MM/yyyy"));
            }

            if (!model.DataCadastro.HasValue && model.DataCadastroAte.HasValue)
            {
                producoes = producoes.Where(p => p.Data.Date <= model.DataCadastroAte.Value);

                filtros.AppendFormat("Data de cadastro até '{0}', ", model.DataCadastroAte.Value.ToString("dd/MM/yyyy"));
            }

            if (model.DataCadastro.HasValue && model.DataCadastroAte.HasValue)
            {
                producoes = producoes.Where(p => p.Data.Date >= model.DataCadastro.Value
                                                         && p.Data.Date <= model.DataCadastroAte.Value);
                filtros.AppendFormat("Data de cadastro de '{0}' até '{1}', ",
                                     model.DataCadastro.Value.ToString("dd/MM/yyyy"),
                                     model.DataCadastroAte.Value.ToString("dd/MM/yyyy"));
            }

            if (model.SituacaoProducao.HasValue)
            {
                producoes = producoes.Where(p => p.SituacaoProducao == model.SituacaoProducao);
                
                filtros.AppendFormat("Situação: {0}", model.SituacaoProducao.GetValueOrDefault().EnumToString());
            }

            return producoes;
        }

        public virtual ActionResult ObtenhaListaGridModel([DataSourceRequest] DataSourceRequest request, PesquisaProducaoModel model)
        {
            try
            {
                var filtros = new StringBuilder();

                var producoes = ObtenhaQueryFiltrada(model, filtros);

                if (!request.Sorts.IsNullOrEmpty())
                {
                    foreach (SortDescriptor sortDescriptor in request.Sorts)
                    {
                        producoes = sortDescriptor.SortDirection.ToString() == "Descending"
                            ? producoes.OrderByDescending(ColunasOrdenacaoGrid[sortDescriptor.Member])
                            : producoes.OrderBy(ColunasOrdenacaoGrid[sortDescriptor.Member]);
                    }
                }

                producoes = producoes.OrderByDescending(o => o.DataAlteracao);

                var total = producoes.Count();

                if (request.Page > 0)
                {
                    producoes = producoes.Skip((request.Page - 1) * request.PageSize);
                }

                var resultado = producoes.Take(request.PageSize).ToList();

                var list = resultado.Select(p => new GridProducaoModel
                {
                    Id = p.Id.GetValueOrDefault(),
                    LoteAno = p.Lote.ToString() + '/' + p.Ano,
                    RemessaProducao = p.RemessaProducao.Descricao,
                    Data = p.Data.Date,
                    QtdeFichasTecnicas = p.ProducaoItens.Count,
                    QtdeProgramada = p.ProducaoProgramacao != null ? p.ProducaoProgramacao.Quantidade : 0,
                    SituacaoProducao = p.SituacaoProducao,
                    TipoProducao = p.TipoProducao
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

        #region Programação
        
        public virtual ActionResult Programacao(long id)
        {
            var domain = _producaoRepository.Get(id);
            var model = Mapper.Flat<ProducaoProgramacaoModel>(domain);
            
            model.SituacaoProducao = domain.SituacaoProducao.EnumToString();
            model.RemessaProducao = domain.RemessaProducao.Descricao;
            model.IdRemessaProducao = domain.RemessaProducao.Id.GetValueOrDefault();
            model.Observacao = domain.Observacao;
            model.ResponsavelProducao = domain.Funcionario.Nome;
            model.Descricao = domain.Descricao;
            model.TipoProducao = domain.TipoProducao.EnumToString();
            
            if (domain.ProducaoProgramacao != null)
            {
                model.Funcionario =  domain.ProducaoProgramacao.Funcionario.Id;
                model.ObservacaoProgramacao = domain.ProducaoProgramacao.Observacao;
                model.DataProgramacao = domain.ProducaoProgramacao.DataProgramada;
            }

            model.GridProducaoItens = new List<ProducaoProgramacaoItemModel>();

            domain.ProducaoItens.ForEach(producaoItem =>
            {
                var modelItem = new ProducaoProgramacaoItemModel
                {
                    Descricao = producaoItem.FichaTecnica.Descricao,
                    Estilista = producaoItem.FichaTecnica.Estilista.Nome,
                    Quantidade = producaoItem.QuantidadeProgramada,
                    Foto = ObtenhaUrlFotoFichaTecnica(producaoItem.FichaTecnica),
                    Id = producaoItem.Id,
                    Referencia = producaoItem.FichaTecnica.Referencia,
                    TagAno = producaoItem.FichaTecnica.Tag + '/' + producaoItem.FichaTecnica.Ano,
                    MatrizCorteJson = ObtenhaMatrizCorteJson(producaoItem)
                };
                model.GridProducaoItens.Add(modelItem);
            });

            return View(model);
        }

        [HttpPost]
        public virtual ActionResult Programacao(ProducaoProgramacaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = _producaoRepository.Get(model.Id);
                    
                    domain.ProducaoProgramacao = domain.ProducaoProgramacao ?? new ProducaoProgramacao()
                    {
                        Data = DateTime.Now
                    };
                    
                    domain.ProducaoProgramacao.DataProgramada = model.DataProgramacao.GetValueOrDefault();
                    domain.ProducaoProgramacao.Observacao = model.ObservacaoProgramacao;
                    domain.ProducaoProgramacao.Funcionario = _pessoaRepository.Load(model.Funcionario);
                    domain.ProducaoProgramacao.Quantidade = model.Quantidade;
                    
                    model.GridProducaoItens.ForEach(modelItem =>
                    {
                        var javaScriptSerializer = new JavaScriptSerializer();
                        var producaoProgramacaoMatrizCorteModel = javaScriptSerializer.Deserialize<ProducaoProgramacaoMatrizCorteModel>(modelItem.MatrizCorteJson);

                        var producaoItem = domain.ProducaoItens.First(x => x.Id == modelItem.Id);

                        var producaoMatrizCorte = producaoItem.ProducaoMatrizCorte ?? new ProducaoMatrizCorte
                        {
                            TipoEnfestoTecido = (TipoEnfestoTecido)producaoProgramacaoMatrizCorteModel.TipoEnfestoTecido
                        };

                        producaoProgramacaoMatrizCorteModel.GridMatrizCorteItens.ForEach(modelMatrizCorteItem =>
                        {
                            var producaoMatrizCorteItem = producaoMatrizCorte.ProducaoMatrizCorteItens.FirstOrDefault(x => 
                                x.Tamanho.Descricao == modelMatrizCorteItem.DescricaoTamanho) ?? new ProducaoMatrizCorteItem();

                            producaoMatrizCorteItem.QuantidadeProgramada = modelMatrizCorteItem.Quantidade.GetValueOrDefault();
                            producaoMatrizCorteItem.QuantidadeVezes = modelMatrizCorteItem.QuantidadeVezes.GetValueOrDefault();
                            producaoMatrizCorteItem.Tamanho = _tamanhoRepository.Get(x => x.Descricao == modelMatrizCorteItem.DescricaoTamanho);
                            
                            producaoMatrizCorte.ProducaoMatrizCorteItens.Add(producaoMatrizCorteItem );
                        });


                        //producaoItem.QuantidadeProgramada = 0;
                        //producaoItem.ProducaoMatrizCorte = null;

                        producaoItem.QuantidadeProgramada = modelItem.Quantidade.GetValueOrDefault();
                        producaoItem.ProducaoMatrizCorte = producaoMatrizCorte;
                    });


                    _producaoRepository.SaveOrUpdate(domain);

                    this.AddSuccessMessage("Programação da produção cadastrada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar a programação da produção. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }

        public virtual String ObtenhaMatrizCorteJson(ProducaoItem producaoItem)
        {
            if (producaoItem.ProducaoMatrizCorte == null)
            {
                return null;
            }

            var matrizCorteModel = new ProducaoProgramacaoMatrizCorteModel
            {
                QuantidadeItem = producaoItem.QuantidadeProgramada,
                TipoEnfestoTecido = (int)producaoItem.ProducaoMatrizCorte.TipoEnfestoTecido,
                GridMatrizCorteItens = new List<ProducaoProgramacaoMatrizCorteItemModel>()
            };

            var matrizCorte = producaoItem.ProducaoMatrizCorte;

            matrizCorte = ObtenhaProducaoMatrizCorteCompleto(matrizCorte, producaoItem.FichaTecnica);

            matrizCorte.ProducaoMatrizCorteItens.ForEach(
                programacaoProducaoMatrizCorteItem =>
                {
                    var matrizCorteItemModel = new ProducaoProgramacaoMatrizCorteItemModel()
                    {
                        Quantidade = programacaoProducaoMatrizCorteItem.QuantidadeProgramada,
                        QuantidadeVezes = programacaoProducaoMatrizCorteItem.QuantidadeVezes,
                        DescricaoTamanho = programacaoProducaoMatrizCorteItem.Tamanho.Descricao
                    };

                    matrizCorteModel.GridMatrizCorteItens.Add(matrizCorteItemModel);
                });

            var javaScriptSerializer = new JavaScriptSerializer();
            return javaScriptSerializer.Serialize(matrizCorteModel);
        }

        public virtual ProducaoMatrizCorte ObtenhaProducaoMatrizCorteCompleto(ProducaoMatrizCorte domain, FichaTecnica fichaTecnica)
        {
            var matrizCorteDomain = domain;

            if (domain.ProducaoMatrizCorteItens.IsEmpty())
            {
                fichaTecnica.FichaTecnicaMatriz.Grade.Tamanhos.Keys.ForEach(tamanho =>
                {
                    var programacaoProducaoMatrizCorteItem = new ProducaoMatrizCorteItem
                    {
                        Tamanho = tamanho,
                        QuantidadeProgramada = 0,
                        QuantidadeVezes = 0
                    };
                    matrizCorteDomain.ProducaoMatrizCorteItens.Add(programacaoProducaoMatrizCorteItem);
                });
            }

            return domain;
        }
        
        [ChildActionOnly]
        public virtual ActionResult MatrizCorte()
        {
            return PartialView(new ProducaoProgramacaoMatrizCorteModel());
        }

        public virtual JsonResult ObtenhaListaProgramacaoProducaoMatrizCorteItemModel(string referenciaFichaTecnica)
        {
            var retorno = new List<ProducaoProgramacaoMatrizCorteItemModel>();

            var fichaTecnica = _fichaTecnicaRepository.Get(x => x.Referencia == referenciaFichaTecnica);

            fichaTecnica.FichaTecnicaMatriz.Grade.Tamanhos.Keys.ForEach(tamanho =>
            {
                var modelItem = new ProducaoProgramacaoMatrizCorteItemModel
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
        
        #region Materiais

        [PopulateViewData("PopulateViewDataMateriais")]
        public virtual ActionResult Materiais(long id)
        {
            var domain = _producaoRepository.Get(id);

            var userId = FashionSecurity.GetLoggedUserId();
            var usuario = _usuarioRepository.Get(userId);
            var funcionarioId = usuario.Funcionario != null ? usuario.Funcionario.Id : null;

            if (funcionarioId == null)
            {
                this.AddErrorMessage("O usuário logado não possui funcionário associado a ele.");
                return RedirectToAction("Index");
            }

            var pessoaLogada = _pessoaRepository.Get(funcionarioId);

            var model = new ProducaoMateriaisModel
            {
                //DataProgramada = domain.ProducaoProgramacao != null ? domain.ProducaoProgramacao.DataProgramada : default(DateTime),
                LoteAno = domain.Lote + "/" + domain.Ano,
                RemessaProducao = domain.RemessaProducao.Descricao,
                SituacaoProducao = domain.SituacaoProducao.EnumToString(),
                GridItens = new List<GridProducaoMaterialModel>(),
                Fotos = domain.ProducaoItens.SelectMany(x => x.FichaTecnica.FichaTecnicaFotos.Select(p => new FotoTituloModel
                {
                    Foto = p.Arquivo.Nome.GetFileUrl(),
                    Titulo = x.FichaTecnica.Referencia
                }))
            };

            var producaoItemMateriaisAgrupado = domain.ProducaoItens.SelectMany(x => x.ProducaoItemMateriais)
                .GroupBy(x => new {x.Material, x.Responsavel, x.DepartamentoProducao})
                .Select(x => new
                {
                    x.Key.Material,
                    x.Key.Responsavel,
                    x.Key.DepartamentoProducao,
                    Quantidade = x.Sum(y => y.Quantidade),
                    QuantidadeNecessidade = x.Sum(y => y.QuantidadeNecessidade)
                });
            
            producaoItemMateriaisAgrupado.ForEach(x =>
            {
                var editavel = x.Responsavel == null || x.Responsavel.Id == pessoaLogada.Id;
                
                var modelMaterial = new GridProducaoMaterialModel
                {
                    DepartamentoProducao = x.DepartamentoProducao.Id.ToString(),
                    Descricao = x.Material.Descricao,
                    Referencia = x.Material.Referencia,
                    UnidadeMedida = x.Material.UnidadeMedida.Sigla,
                    Foto = x.Material.Foto != null ? x.Material.Foto.Nome.GetFileUrl() : string.Empty,
                    GeneroCategoria = x.Material.Subcategoria.Categoria.GeneroCategoria,
                    Necessitado = x.QuantidadeNecessidade != 0,
                    Quantidade = x.Quantidade,
                    Editavel = editavel
                };

                model.GridItens.Add(modelMaterial);
            });

            if (model.GridItens.Count == 0)
            {
                domain.ProducaoItens.ForEach(x =>
                {
                    x.FichaTecnica.MateriaisConsumo.ForEach(y =>
                    {
                        //y.Quantidade = y.Quantidade * x.QuantidadeProgramada;
                        y.Quantidade = y.Quantidade * 100;
                    });
                });

                var materiaisFichaTecnicaDomain = domain.ProducaoItens.SelectMany(x => x.FichaTecnica.MateriaisConsumo);

                var materiaisDomainAgrupado = materiaisFichaTecnicaDomain.GroupBy(x => new {x.Material, x.DepartamentoProducao})
                    .Select(x => new
                    {
                        x.Key.Material,
                        x.Key.DepartamentoProducao,
                        Quantidade = x.Sum(y => y.Quantidade)
                    });

                materiaisDomainAgrupado.ForEach(x =>
                {
                    var modelMaterial = new GridProducaoMaterialModel
                    {
                        DepartamentoProducao = x.DepartamentoProducao.Id.ToString(),
                        Descricao = x.Material.Descricao,
                        Referencia = x.Material.Referencia,
                        UnidadeMedida = x.Material.UnidadeMedida.Sigla,
                        Foto = x.Material.Foto != null ? x.Material.Foto.Nome.GetFileUrl() : string.Empty,
                        GeneroCategoria = x.Material.Subcategoria.Categoria.GeneroCategoria,
                        Necessitado = false,
                        Quantidade = x.Quantidade,
                        Editavel = true
                    };

                    model.GridItens.Add(modelMaterial);
                });
            }

            model.GridItens = model.GridItens.OrderBy(x => x.Descricao).ToList();

            return View(model);
        }

        #endregion
        
        #region PopulateViewData

        protected void PopulateViewDataPesquisa(PesquisaProducaoModel model)
        {
            var remessas = _remessaProducaoRepository.Find().ToList();
            ViewBag.RemessaProducao = remessas.ToSelectList("Descricao", model.RemessaProducao);
            ViewBag.OrdenarPor = new SelectList(ColunasOrdenacaoRelatorio, "value", "key");
        }

        protected void PopulateViewData(ProducaoModel model)
        {
            var remessas = _remessaProducaoRepository.Find().ToList();
            ViewBag.RemessaProducao = remessas.ToSelectList("Descricao", model.RemessaProducao);
        }

        protected void PopulateViewDataMateriais(ProducaoMateriaisModel model)
        {
            var generoCategorias = (from Enum e in Enum.GetValues(typeof(GeneroCategoria))
                                    let d = e.GetDisplay()
                                    select new { Id = Convert.ToInt32(e), Name = d != null ? d.Name : e.ToString() }).ToList();

            ViewBag.GeneroCategoriaDicionarioJson = generoCategorias.ToDictionary(k => k.Id.ToString(), e => e.Name).FromDictionaryToJson();

            var departamentoProducaos = _departamentoProducaoRepository.Find(p => p.Ativo).ToList();
            ViewBag.DepartamentoProducaos = departamentoProducaos.Select(s => new { Id = s.Id.ToString(), s.Nome }).OrderBy(x => x.Nome);
            ViewBag.DepartamentoProducaoDicionarioJson = departamentoProducaos.ToDictionary(k => k.Id.ToString(), e => e.Nome).FromDictionaryToJson();
        }

        #endregion
        
        private long ObtenhaProximoNumero()
        {
            var ultimoNumero = _ultimoNumeroRepository.Get(x => x.NomeTabela == "producao");
            long numero = 0;

            if (ultimoNumero != null)
            {
                ultimoNumero = ObtenhaProximoNumeroDisponivel(ultimoNumero);
                numero = ultimoNumero.Numero;
                _ultimoNumeroRepository.SaveOrUpdate(ultimoNumero);
            }
            else
            {
                ultimoNumero = new UltimoNumero {NomeTabela = "producao", Numero = 1};
                ObtenhaProximoNumeroDisponivel(ultimoNumero);
                _ultimoNumeroRepository.SaveOrUpdate(ultimoNumero);
                numero = ultimoNumero.Numero;
            }

            return numero;
        }

        private UltimoNumero ObtenhaProximoNumeroDisponivel(UltimoNumero ultimoNumero)
        {
            ultimoNumero.Numero++;
            var producao = _producaoRepository.Get(x => x.Lote == ultimoNumero.Numero);
            return producao != null ? ObtenhaProximoNumeroDisponivel(ultimoNumero) : ultimoNumero;
        }

        public string ObtenhaUrlFotoFichaTecnica(FichaTecnica fichaTecnica)
        {
            if (fichaTecnica.FichaTecnicaFotos.Any(x => x.Padrao))
            {
                return fichaTecnica.FichaTecnicaFotos.Where(x => x.Padrao).ElementAt(0).Arquivo.Nome.GetFileUrl();
            }

            return fichaTecnica.FichaTecnicaFotos.Any() ? fichaTecnica.FichaTecnicaFotos.ElementAt(0).Arquivo.Nome.GetFileUrl() : null;
        }
    }
}