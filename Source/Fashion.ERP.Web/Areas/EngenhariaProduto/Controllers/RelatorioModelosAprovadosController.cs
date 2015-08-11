using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.ERP.Reporting.EngenhariaProduto;
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Web.Areas.EngenhariaProduto.Models;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;
using Telerik.Reporting;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers
{
    public partial class RelatorioModelosAprovadosController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IRepository<Modelo> _modeloRepository;
        private readonly IRepository<Colecao> _colecaoRepository;
        private readonly IRepository<Material> _materialRepository;
        private readonly IRepository<ClassificacaoDificuldade> _classificacaoDificuldadeRepository;

        #region Colunas
        private static readonly Dictionary<string, string> ColunasAgrupamentoModelosAprovados = new Dictionary<string, string>
        {
            {"Dificuldade", "ModeloAvaliacao.ClassificacaoDificuldade.Descricao"},
            {"Catálogo", "ModeloAvaliacao.Catalogo"},
            {"Estilista", "Estilista.Nome"}
        };

        private static readonly Dictionary<string, string> ColunasOrdenacaoModelosAprovados = new Dictionary<string, string>
        {
            {"Total aprovado", "ModeloAvaliacao.QuantidadeTotaAprovacao"},
            {"Estilista", "Estilista.Nome"}
        };
        #endregion
        
        #region Construtores
        public RelatorioModelosAprovadosController(ILogger logger, IRepository<Modelo> modeloRepository, 
            IRepository<Colecao> colecaoRepository, IRepository<ClassificacaoDificuldade> classificacaoDificuldadeRepository,
            IRepository<Material> materialRepository)
        {
            _logger = logger;
            _modeloRepository = modeloRepository;
            _colecaoRepository = colecaoRepository;
            _classificacaoDificuldadeRepository = classificacaoDificuldadeRepository;
            _materialRepository = materialRepository;
        }

        #endregion

        #region ModelosAprovados
        [PopulateViewData("PopulateModelosAprovados")]
        public virtual ActionResult ModelosAprovados()
        {
            var model = new ModelosAprovadosModel();

            /*   var hoje = DateTime.Now.Date;
               var umMesAtras = hoje.AddMonths(-1);

               model.DataInicial = umMesAtras;
               model.DataFinal = hoje;*/

            return View(model);
        }

        [HttpPost, AjaxOnly, PopulateViewData("PopulateModelosAprovados")]
        public virtual JsonResult ModelosAprovados(ModelosAprovadosModel model)
        {
            var query = _modeloRepository.Find(x => x.Situacao == SituacaoModelo.Aprovado && x.ModeloAvaliacao != null);

            var filtros = new StringBuilder();

            if (model.Colecao.HasValue)
            {
                query = query.Where(p => p.ModeloAvaliacao.Colecao.Id == model.Colecao);
            }

            if (model.IntervaloInicial.HasValue)
            {
                query = query.Where(p => p.ModeloAvaliacao.Data >= model.IntervaloInicial.Value);
                filtros.AppendFormat("Período de aprovação entre: {0:dd/MM/yyyy}, ", model.IntervaloInicial.Value);
            }

            if (model.IntervaloFinal.HasValue)
            {
                query = query.Where(p => p.ModeloAvaliacao.Data <= model.IntervaloFinal.Value);
                filtros.AppendFormat("à: {0:dd/MM/yyyy}, ", model.IntervaloFinal.Value);
            }

            if (model.ClassificacaoDificuldade.HasValue)
            {
                query =
                    query.Where(p => p.ModeloAvaliacao.ClassificacaoDificuldade.Id == model.ClassificacaoDificuldade);
                filtros.AppendFormat("Dificuldade: {0}, ",
                    _classificacaoDificuldadeRepository.Get(model.ClassificacaoDificuldade.Value).Descricao);
            }

            if (model.Catalogo.HasValue)
            {
                query = query.Where(p => p.ModeloAvaliacao.Catalogo == model.Catalogo);
                filtros.AppendFormat("Catálogo: {0}, ", model.Catalogo.Value ? "Sim" : "Não");
            }

            if (!string.IsNullOrWhiteSpace(model.Tag))
            {
                query = query.Where(p => p.ModeloAvaliacao.Tag.Contains(model.Tag));
                filtros.AppendFormat("Tag: {0}, ", model.Tag);
            }
            
            if (!string.IsNullOrWhiteSpace(model.Ano))
            {
                query = query.Where(p => p.ModeloAvaliacao.Ano == int.Parse(model.Ano));
                filtros.AppendFormat("Ano {0}, ", model.Ano);
            }

            if (model.Material.HasValue)
            {
                query = query.Where(p => p.MateriaisConsumo.Any(material =>
                            material.Material.Id == model.Material));

                var materialDomain = _materialRepository.Load(model.Material);
                filtros.AppendFormat("Referência do Material: {0}, ", materialDomain.Referencia);
            }

            var result = query.ToList();

            if (!result.Any())
                return Json(new {Error = "Nenhum modelo aprovado foi encontrado."});

            var report = new ModelosAprovadosReport() {DataSource = result};

            if (filtros.Length > 2)
                report.ReportParameters["Filtros"].Value = filtros.ToString().Substring(0, filtros.Length - 2);
            
            var reportHeaderSection = report.Items.First(item => item.Name == "reportHeader") as ReportHeaderSection;
            var colecaoAprovadaTextBox = reportHeaderSection.Items.First(item => item.Name == "ColecaoAprovada") as TextBox;
            colecaoAprovadaTextBox.Value = _colecaoRepository.Get(model.Colecao).Descricao;

            var grupo = report.Groups.First(p => p.Name.Equals("Grupo"));

            if (model.AgruparPor != null)
            {
                grupo.Groupings.Add("=Fields." + model.AgruparPor);

                var key = ColunasAgrupamentoModelosAprovados.First(p => p.Value == model.AgruparPor).Key;
                var titulo = string.Format("=\"{0}: \" + Fields.{1}", key, model.AgruparPor);
                grupo.GroupHeader.GetTextBox("Titulo").Value = titulo;
                grupo.GroupFooter.GetTextBox("TotalAgrupamento").Value = "Total de Modelos por " + key;
            }
            else
            {
                report.Groups.Remove(grupo);
            }
            
            if (model.OrdenarPor != null)
                report.Sortings.Add("=Fields." + model.OrdenarPor,
                    model.OrdenarEm == "asc" ? SortDirection.Asc : SortDirection.Desc);

            report.Sortings.Add("=Fields.ModeloAvaliacao.Tag", SortDirection.Asc);

            var filename = report.ToByteStream().SaveFile(".pdf");

            return Json(new {Url = filename});
        }
        #endregion

        #region PopulateModelosAprovados
        protected void PopulateModelosAprovados(ModelosAprovadosModel model)
        {
            var colecoes = _colecaoRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["Colecao"] = colecoes.ToSelectList("Descricao", model.Colecao);

            var classificacaoDificuldade = _classificacaoDificuldadeRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewData["classificacaoDificuldade"] = classificacaoDificuldade.ToSelectList("Descricao", model.ClassificacaoDificuldade);

            ViewBag.OrdenarPor = new SelectList(ColunasOrdenacaoModelosAprovados, "value", "key");
            ViewBag.AgruparPor = new SelectList(ColunasAgrupamentoModelosAprovados, "value", "key");
        }
        #endregion
    }
}