using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
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
using Fashion.Framework.Repository;
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
        
        private readonly IRepository<Tamanho> _tamanhoRepository;
        private readonly IRepository<UltimoNumero> _ultimoNumeroRepository;

        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public ProducaoController(ILogger logger, IRepository<Domain.Producao.Producao> producaoRepository,
            IRepository<FichaTecnica> fichaTecnicaRepository, IRepository<Tamanho> tamanhoRepository, 
            IRepository<UltimoNumero> ultimoNumeroRepository, IRepository<RemessaProducao> remessaProducaoRepository )
        {
            _producaoRepository = producaoRepository;
            _fichaTecnicaRepository = fichaTecnicaRepository;
            _tamanhoRepository = tamanhoRepository;
            _ultimoNumeroRepository = ultimoNumeroRepository;
            _remessaProducaoRepository = remessaProducaoRepository;
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

        #region PopulateViewDataPesquisa

        protected void PopulateViewDataPesquisa(PesquisaProducaoModel model)
        {
            var remessas = _remessaProducaoRepository.Find().ToList();
            ViewBag.RemessaProducao = remessas.ToSelectList("Descricao", model.RemessaProducao);
            ViewBag.OrdenarPor = new SelectList(ColunasOrdenacaoRelatorio, "value", "key");
        }
        #endregion

        #region PopulateViewData

        protected void PopulateViewData(ProducaoModel model)
        {
            var remessas = _remessaProducaoRepository.Find().ToList();
            ViewBag.RemessaProducao = remessas.ToSelectList("Descricao", model.RemessaProducao);
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