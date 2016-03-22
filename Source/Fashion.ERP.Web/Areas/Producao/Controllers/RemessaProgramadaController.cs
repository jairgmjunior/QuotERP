using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.Producao;
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Reporting.Producao;
using Fashion.ERP.Web.Areas.Producao.Models;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Ninject.Extensions.Logging;
using Telerik.Reporting;

namespace Fashion.ERP.Web.Areas.Producao.Controllers
{
    public partial class RemessaProgramadaController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IRepository<ProgramacaoProducao> _programacaoProducaoRepository;
        private readonly IRepository<Colecao> _colecaoRepository;
        private readonly IRepository<RemessaProducao> _remessaProducaoRepository;
        
        #region Construtores
        public RemessaProgramadaController(ILogger logger, 
            IRepository<Colecao> colecaoRepository, IRepository<ProgramacaoProducao> programacaoProducaoRepository,
            IRepository<RemessaProducao> remessaProducaoRepository)
        {
            _logger = logger;
            _colecaoRepository = colecaoRepository;
            _programacaoProducaoRepository = programacaoProducaoRepository;
            _remessaProducaoRepository = remessaProducaoRepository;
        }
        #endregion

        #region Colunas Ordenação
        private static readonly Dictionary<string, string> ColunasOrdenacaoGrid = new Dictionary<string, string>
        {
            {"RemessaProducao", "Descricao"}
        };
        #endregion

        #region Index
        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Index()
        {
            var model = new PesquisaColecaoProgramadaModel ();

            return View(model);
        }

        private IQueryable<RemessaProducao> ObtenhaQueryFiltrada(PesquisaColecaoProgramadaModel model)
        {
            var programacoesProducao = _programacaoProducaoRepository.Find();
            
            if (model.DataInicial.HasValue && model.DataFinal.HasValue)
            {
                programacoesProducao = programacoesProducao.Where(p => p.DataProgramada.Date >= model.DataInicial.Value && p.DataProgramada.Date <= model.DataFinal.Value);
            }

            var remessasProducoes = programacoesProducao.Select(x => x.RemessaProducao).ToList().Distinct().AsQueryable();
         
            if (model.RemessasProducao.IsNullOrEmpty() == false)
            {
                var remessas = model.RemessasProducao.ConvertAll(long.Parse);
                remessasProducoes = remessasProducoes.Where(p => remessas.Contains(p.Id ?? 0));
            }

            return remessasProducoes;
        }

        public virtual ActionResult ObtenhaListaGridModel([DataSourceRequest] DataSourceRequest request, PesquisaColecaoProgramadaModel model)
        {
            try
            {
                var remessasProducao = ObtenhaQueryFiltrada(model);

                if (!request.Sorts.IsNullOrEmpty())
                {
                    foreach (SortDescriptor sortDescriptor in request.Sorts)
                    {
                        remessasProducao = sortDescriptor.SortDirection.ToString() == "Descending"
                            ? remessasProducao.OrderByDescending(ColunasOrdenacaoGrid[sortDescriptor.Member])
                            : remessasProducao.OrderBy(ColunasOrdenacaoGrid[sortDescriptor.Member]);
                    }
                }

                remessasProducao = remessasProducao.OrderByDescending(o => o.Descricao);

                var total = remessasProducao.Count();

                if (request.Page > 0)
                {
                    remessasProducao = remessasProducao.Skip((request.Page - 1) * request.PageSize);
                }

                var resultado = remessasProducao.Take(request.PageSize).ToList();

                var list = resultado.Select(p => new GridColecaoProgramadaModel()
                {
                    Id = p.Id.Value,
                    RemessaProgramada = p.Descricao,
                    QtdeFichasTecnicasProgramadas = _programacaoProducaoRepository.Find(x => x.RemessaProducao.Id == p.Id).Count(),
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

        #region Imprimir
        public virtual ActionResult Book(long id)
        {
            var programacoesProducao = _programacaoProducaoRepository.Find(x => x.RemessaProducao.Id == id).ToList();

            var result = programacoesProducao.SelectMany(x => x.ProgramacaoProducaoItems).GroupBy(x => 
                new { x.FichaTecnica }, (chave, grupo) => new
            {
                PrimeiraFoto = ObtenhaUrlPrimeiraFoto(chave.FichaTecnica),
                SegundaFoto = ObtenhaUrlSegundaFoto(chave.FichaTecnica),
                chave.FichaTecnica.Tag,
                chave.FichaTecnica.Ano,
                chave.FichaTecnica.Descricao,
                Catalogo = chave.FichaTecnica.Catalogo.GetValueOrDefault() ? "Sim" : "Não",
                chave.FichaTecnica.Referencia,
                Estilista = chave.FichaTecnica.Estilista.Nome,
                QuantidadeProgramada = grupo.Sum(y => y.Quantidade)
            });

            var report = new BookRemesssaProgramadaReport { DataSource = result };

            var reportHeaderSection = report.Items.First(item => item.Name == "reportHeader") as ReportHeaderSection;
            var colecaoAprovadaTextBox = reportHeaderSection.Items.First(item => item.Name == "Colecao") as TextBox;
            colecaoAprovadaTextBox.Value = _colecaoRepository.Get(id).Descricao;

            report.Sortings.Add("=Fields.Tag", SortDirection.Asc);

            var filename = report.ToByteStream().SaveFile(".pdf");
            
            return File(filename);
        }
        #endregion

        public string ObtenhaUrlPrimeiraFoto(FichaTecnica fichaTecnica)
        {
            if (fichaTecnica.FichaTecnicaFotos.Any(x => x.Impressao))
            {
                return fichaTecnica.FichaTecnicaFotos.Where(x => x.Impressao).ElementAt(0).Arquivo.Nome.GetFileUrl();
            }

            return null;
        }

        public string ObtenhaUrlSegundaFoto(FichaTecnica fichaTecnica)
        {
            if (fichaTecnica.FichaTecnicaFotos.Count(x => x.Impressao) > 1)
            {
                return fichaTecnica.FichaTecnicaFotos.Where(x => x.Impressao).ElementAt(1).Arquivo.Nome.GetFileUrl();
            }

            return null;
        }

        protected void PopulateViewData(PesquisaColecaoProgramadaModel model)
        {
            var remessasProducao = _remessaProducaoRepository.Find().ToList();
            ViewBag.RemessasProducao = remessasProducao.ToSelectList("Descricao");

            if (model.RemessasProducao == null)
                model.RemessasProducao = new List<string>();
        }
    }
}