using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.Producao;
using Fashion.ERP.Reporting.Compras;
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Reporting.Producao;
using Fashion.ERP.Web.Areas.Almoxarifado.Models;
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
    public partial class ColecaoProgramadaController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IRepository<ProgramacaoProducao> _programacaoProducaoRepository;
        private readonly IRepository<Colecao> _colecaoRepository;
        
        #region Construtores
        public ColecaoProgramadaController(ILogger logger, 
            IRepository<Colecao> colecaoRepository, IRepository<ProgramacaoProducao> programacaoProducaoRepository)
        {
            _logger = logger;
            _colecaoRepository = colecaoRepository;
            _programacaoProducaoRepository = programacaoProducaoRepository;
        }
        #endregion

        #region Colunas Ordenação
        private static readonly Dictionary<string, string> ColunasOrdenacaoGrid = new Dictionary<string, string>
        {
            {"Colecao", "Descricao"}
        };
        #endregion

        #region Index
        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Index()
        {
            var model = new PesquisaColecaoProgramadaModel ();

            return View(model);
        }

        private IQueryable<Colecao> ObtenhaQueryFiltrada(PesquisaColecaoProgramadaModel model)
        {
            var programacoes = _programacaoProducaoRepository.Find();
            
            if (model.DataInicial.HasValue && model.DataFinal.HasValue)
            {
                programacoes = programacoes.Where(p => p.DataProgramada.Date >= model.DataInicial.Value && p.DataProgramada.Date <= model.DataFinal.Value);
            }

            var colecoesProgramadas = programacoes.Select(x => x.Colecao).ToList().Distinct().AsQueryable();
         
            if (model.ColecoesProgramadas.IsNullOrEmpty() == false)
            {
                var colecoes = model.ColecoesProgramadas.ConvertAll(long.Parse);
                colecoesProgramadas = colecoesProgramadas.Where(p => colecoes.Contains(p.Id ?? 0));
            }

            return colecoesProgramadas;
        }

        public virtual ActionResult ObtenhaListaGridModel([DataSourceRequest] DataSourceRequest request, PesquisaColecaoProgramadaModel model)
        {
            try
            {
                var colecoesProgramadas = ObtenhaQueryFiltrada(model);

                if (!request.Sorts.IsNullOrEmpty())
                {
                    foreach (SortDescriptor sortDescriptor in request.Sorts)
                    {
                        colecoesProgramadas = sortDescriptor.SortDirection.ToString() == "Descending"
                            ? colecoesProgramadas.OrderByDescending(ColunasOrdenacaoGrid[sortDescriptor.Member])
                            : colecoesProgramadas.OrderBy(ColunasOrdenacaoGrid[sortDescriptor.Member]);
                    }
                }

                colecoesProgramadas = colecoesProgramadas.OrderByDescending(o => o.Descricao);

                var total = colecoesProgramadas.Count();

                if (request.Page > 0)
                {
                    colecoesProgramadas = colecoesProgramadas.Skip((request.Page - 1) * request.PageSize);
                }

                var resultado = colecoesProgramadas.Take(request.PageSize).ToList();

                var list = resultado.Select(p => new GridColecaoProgramadaModel()
                {
                    Id = p.Id.Value,
                    Colecao = p.Descricao,
                    QtdeFichasTecnicasProgramadas = _programacaoProducaoRepository.Find(x => x.Colecao.Id == p.Id).Count(),
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
            var programacoesProducao = _programacaoProducaoRepository.Find(x => x.Colecao.Id == id).ToList();

            var result = programacoesProducao.Select(q => new
            {
                q.Id,
                PrimeiraFoto = ObtenhaUrlPrimeiraFoto(q.FichaTecnica),
                SegundaFoto = ObtenhaUrlSegundaFoto(q.FichaTecnica),
                q.FichaTecnica.Tag,
                q.FichaTecnica.Ano,
                Catalogo = q.FichaTecnica.Catalogo.GetValueOrDefault() ? "Sim" : "Não",
                q.FichaTecnica.Referencia,
                q.DataProgramada,
                Estilista = q.FichaTecnica.Estilista.Nome,
                QuantidadeProgramada = q.Quantidade
            });

            var report = new BookColecaoProgramadaReport { DataSource = result };

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
            var colecoes = _colecaoRepository.Find(p => p.Ativo).ToList();
            ViewBag.ColecoesProgramadas = colecoes.ToSelectList("Descricao");

            if (model.ColecoesProgramadas == null)
                model.ColecoesProgramadas = new List<string>();
        }
    }
}