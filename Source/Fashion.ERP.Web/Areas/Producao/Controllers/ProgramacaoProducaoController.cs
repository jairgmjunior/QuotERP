using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.Producao;
using Fashion.ERP.Web.Areas.Producao.Models;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using NHibernate.Linq;
using Ninject.Extensions.Logging;

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
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public ProgramacaoProducaoController(ILogger logger, IRepository<ProgramacaoProducao> programacaoProducaoRepository,
            IRepository<Colecao> colecaoRepository, IRepository<FichaTecnica> fichaTecnicaRepository,
            IRepository<Pessoa> pessoaRepository, IRepository<Tamanho> tamanhoRepository)
        {
            _programacaoProducaoRepository = programacaoProducaoRepository;
            _colecaoRepository = colecaoRepository;
            _fichaTecnicaRepository = fichaTecnicaRepository;
            _pessoaRepository = pessoaRepository;
            _tamanhoRepository = tamanhoRepository;
            _logger = logger;
        }
        #endregion

        #region Colunas Ordenação
        private static readonly Dictionary<string, string> ColunasOrdenacaoGrid = new Dictionary<string, string>
        {
            {"Numero", "Numero"},
            {"Colecao", "Colecao.Descricao"},
            {"DataProgramada", "DataProgramada"},
            {"Tag", "FichaTecnica.Tag"},
            {"Referencia", "FichaTecnica.Referencia"},
            {"Descricao", "FichaTecnica.Descricao"},
            {"QtdeProgramada", "Quantidade"}
        };
        #endregion

        #region Novo
        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo()
        {
            return View(new ProgramacaoProducaoModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
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
                    domain.FichaTecnica = _fichaTecnicaRepository.Load(model.FichaTecnica);
                    domain.Funcionario = _pessoaRepository.Load(model.Responsavel);
                    domain.Numero = ProximoNumero();

                    var programacaoProducaoMatrizCorte = new ProgramacaoProducaoMatrizCorte()
                    {
                        TipoEnfestoTecido = model.TipoEnfestoTecido
                    };

                    model.GridItens.ForEach(modelItem =>
                    {
                        if (!modelItem.Quantidade.HasValue)
                            return;

                        var programacaoProducaoMatrizCorteItem = new ProgramacaoProducaoMatrizCorteItem()
                        {
                            Quantidade = modelItem.Quantidade.GetValueOrDefault(),
                            QuantidadeVezes = modelItem.QuantidadeVezes.GetValueOrDefault(),
                            Tamanho = _tamanhoRepository.Load(modelItem.Tamanho)
                        };
                        programacaoProducaoMatrizCorte.ProgramacaoProducaoMatrizCorteItens.Add(programacaoProducaoMatrizCorteItem);
                    });

                    domain.ProgramacaoProducaoMatrizCorte = programacaoProducaoMatrizCorte;

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

        public virtual ActionResult ObtenhaListaGridMatrizCorteModel([DataSourceRequest] DataSourceRequest request, long? id, long? fichaTecnicaId)
        {
            try
            {
                var retorno = new List<ProgramacaoProducaoMatrizCorteItemModel>();

                if (fichaTecnicaId != null)
                {
                    var fichaTecnica = _fichaTecnicaRepository.Get(fichaTecnicaId);
                    if (id != null)
                    {
                        var programacaoProducao = _programacaoProducaoRepository.Get(id);

                        if (programacaoProducao.ProgramacaoProducaoMatrizCorte != null)
                        {
                            fichaTecnica.FichaTecnicaMatriz.Grade.Tamanhos.Keys.ForEach(tamanho =>
                            {
                                var programacaoProducaoMatrizCorteItem = programacaoProducao.ProgramacaoProducaoMatrizCorte
                                    .ProgramacaoProducaoMatrizCorteItens.FirstOrDefault(x => x.Tamanho.Id == tamanho.Id);

                                var modelItem = new ProgramacaoProducaoMatrizCorteItemModel
                                {
                                    DescricaoTamanho = tamanho.Descricao,
                                    Tamanho = tamanho.Id,
                                    Quantidade =
                                        programacaoProducaoMatrizCorteItem != null
                                            ? programacaoProducaoMatrizCorteItem.Quantidade
                                            : (long?)null,
                                    QuantidadeVezes =
                                        programacaoProducaoMatrizCorteItem != null
                                            ? programacaoProducaoMatrizCorteItem.QuantidadeVezes
                                            : (long?)null,
                                };

                                retorno.Add(modelItem);
                            });
                            
                            return Json(retorno.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                        }
                    }

                    fichaTecnica.FichaTecnicaMatriz.Grade.Tamanhos.Keys.ForEach(tamanho =>
                    {
                        var modelItem = new ProgramacaoProducaoMatrizCorteItemModel
                        {
                            DescricaoTamanho = tamanho.Descricao,
                            Tamanho = tamanho.Id
                        };
                        retorno.Add(modelItem);
                    });

                }

                return Json(retorno.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var message = ex.GetMessage();
                _logger.Info(message);

                return Json(new DataSourceResult { Errors = message });
            }
        }

        #endregion
        
        #region Editar

        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(long id)
        {
            var domain = _programacaoProducaoRepository.Get(id);
            var model = Mapper.Flat<ProgramacaoProducaoModel>(domain);
            model.Responsavel = domain.Funcionario.Id;
            model.TipoEnfestoTecido = domain.ProgramacaoProducaoMatrizCorte.TipoEnfestoTecido;
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Editar(ProgramacaoProducaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _programacaoProducaoRepository.Get(model.Id));
                    domain.Colecao = _colecaoRepository.Load(model.Colecao);
                    domain.FichaTecnica = _fichaTecnicaRepository.Load(model.FichaTecnica);
                    domain.Funcionario = _pessoaRepository.Load(model.Responsavel);

                    var programacaoProducaoMatrizCorte = new ProgramacaoProducaoMatrizCorte()
                    {
                        TipoEnfestoTecido = model.TipoEnfestoTecido
                    };

                    model.GridItens.ForEach(modelItem =>
                    {
                        if (!modelItem.Quantidade.HasValue)
                            return;

                        var programacaoProducaoMatrizCorteItem = new ProgramacaoProducaoMatrizCorteItem()
                        {
                            Quantidade = modelItem.Quantidade.GetValueOrDefault(),
                            QuantidadeVezes = modelItem.QuantidadeVezes.GetValueOrDefault(),
                            Tamanho = _tamanhoRepository.Load(modelItem.Tamanho)
                        };
                        programacaoProducaoMatrizCorte.ProgramacaoProducaoMatrizCorteItens.Add(programacaoProducaoMatrizCorteItem);
                    });

                    domain.ProgramacaoProducaoMatrizCorte = programacaoProducaoMatrizCorte;
                    
                    _programacaoProducaoRepository.SaveOrUpdate(domain);

                    this.AddSuccessMessage("Programação da produção atualizada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a programação da produção. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
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

        private IQueryable<ProgramacaoProducao> ObtenhaQueryFiltrada(PesquisaProgramacaoProducaoModel model, StringBuilder filtros)
        {
            var programacoesProducao = _programacaoProducaoRepository.Find();
            if (!string.IsNullOrWhiteSpace(model.Referencia))
            {
                programacoesProducao = programacoesProducao.Where(p => p.FichaTecnica.Referencia == model.Referencia);
                filtros.AppendFormat("Referência: {0}, ", model.Referencia);
            }

            if (!string.IsNullOrWhiteSpace(model.Tag))
            {
                programacoesProducao = programacoesProducao.Where(p => p.FichaTecnica.Tag == model.Tag);
                filtros.AppendFormat("Tag: {0}, ", model.Tag);
            }

            if (model.Ano.HasValue)
            {
                programacoesProducao = programacoesProducao.Where(p => p.FichaTecnica.Ano == model.Ano);
                filtros.AppendFormat("Ano: {0}, ", model.Ano);
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

                var requisicaoMaterials = ObtenhaQueryFiltrada(model, filtros);

                if (!request.Sorts.IsNullOrEmpty())
                {
                    foreach (SortDescriptor sortDescriptor in request.Sorts)
                    {
                        requisicaoMaterials = sortDescriptor.SortDirection.ToString() == "Descending"
                            ? requisicaoMaterials.OrderByDescending(ColunasOrdenacaoGrid[sortDescriptor.Member])
                            : requisicaoMaterials.OrderBy(ColunasOrdenacaoGrid[sortDescriptor.Member]);
                    }
                }

                requisicaoMaterials = requisicaoMaterials.OrderByDescending(o => o.DataAlteracao);

                var total = requisicaoMaterials.Count();

                if (request.Page > 0)
                {
                    requisicaoMaterials = requisicaoMaterials.Skip((request.Page - 1) * request.PageSize);
                }

                var resultado = requisicaoMaterials.Take(request.PageSize).ToList();

                var list = resultado.Select(p => new GridProgramacaoProducaoModel
                {
                    Id = p.Id.GetValueOrDefault(),
                    Numero = p.Numero,
                    Colecao = p.Colecao.Descricao,
                    DataProgramada = p.DataProgramada.Date,
                    Tag = p.FichaTecnica.Tag + "/" + p.FichaTecnica.Ano,
                    Referencia = p.FichaTecnica.Referencia,
                    Descricao = p.FichaTecnica.Descricao,
                    QtdeProgramada = p.Quantidade
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
        
        #region PopulateViewDataPesquisa

        protected void PopulateViewDataPesquisa(PesquisaProgramacaoProducaoModel model)
        {
            var colecaos = _colecaoRepository.Find(p => p.Ativo).ToList();
            ViewBag.Colecao = colecaos.ToSelectList("Descricao", model.Colecao);
        }
        #endregion

        #region PopulateViewData

        protected void PopulateViewData(ProgramacaoProducaoModel model)
        {
            var colecaos = _colecaoRepository.Find(p => p.Ativo).ToList();
            ViewBag.Colecao = colecaos.ToSelectList("Descricao", model.Colecao);
        }
        #endregion

        private long ProximoNumero()
        {
            try
            {
                return _programacaoProducaoRepository.Find().Max(p => p.Numero) + 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }
    }
}