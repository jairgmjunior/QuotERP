using System;
using System.Collections.Generic;
using System.Linq;
using Fashion.ERP.Web.Helpers;
using System.Text;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Reporting.Almoxarifado;
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.Almoxarifado.Models;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using NHibernate.Linq;
using Ninject.Extensions.Logging;
using Telerik.Reporting;
using Telerik.Reporting.Drawing;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Controllers
{
    public partial class ReservaMaterialController : BaseController
    {
        #region Variaveis
        private readonly IRepository<ReservaMaterial> _reservaMaterialRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<Colecao> _colecaoRepository;
        private readonly IRepository<Material> _materialRepository;
        private readonly ILogger _logger;
        private Dictionary<string, string> _colunasPesquisaReservaMaterial;
        #endregion

        #region Construtores
        public ReservaMaterialController(ILogger logger, IRepository<Colecao> colecaoRepository,
            IRepository<ReservaMaterial> reservaMaterialRepository,  IRepository<Pessoa> pessoaRepository,
            IRepository<Material> materialRepository)
        {
            _reservaMaterialRepository = reservaMaterialRepository;
            _pessoaRepository = pessoaRepository;
            _colecaoRepository = colecaoRepository;
            _materialRepository = materialRepository;

            _logger = logger;

            PreecheColunasPesquisa();
        }
        #endregion

        #region Views

        #region Index
        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Index()
        {
            var reservaMaterials = _reservaMaterialRepository.Find();

            var model = new PesquisaReservaMaterialModel { ModoConsulta = "Listar" };

            model.Grid = reservaMaterials.Select(p => new GridReservaMaterialModel
            {
                Id = (long)p.Id,
                Referencia = p.ReferenciaOrigem,
                Numero = p.Numero,
                Data = p.Data,
                DataProgramacao = p.DataProgramacao,
                Situacao = p.SituacaoReservaMaterial.EnumToString(),
                Colecao = p.Colecao.Descricao
            }).OrderByDescending(o => o.Id).ToList();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Index(PesquisaReservaMaterialModel model)
        {
            var reservaMaterials = _reservaMaterialRepository.Find();

            try
            {
                #region Filtros
                var filtros = new StringBuilder();

                if (!string.IsNullOrWhiteSpace(model.ReferenciaOrigem))
                {
                    reservaMaterials = reservaMaterials.Where(p => p.ReferenciaOrigem == model.ReferenciaOrigem);
                    filtros.AppendFormat("Referência de origem: {0}, ", model.ReferenciaOrigem);
                }
                
                if (model.Numero.HasValue)
                {
                    reservaMaterials = reservaMaterials.Where(p => p.Numero == model.Numero);
                    filtros.AppendFormat("Número: {0}, ", model.Numero.Value);
                }

                if (model.Unidade.HasValue)
                {
                    reservaMaterials = reservaMaterials.Where(p => p.Unidade.Id == model.Unidade);
                    filtros.AppendFormat("Unidade: {0}, ", _pessoaRepository.Get(model.Unidade.Value).NomeFantasia);
                }

                if (model.DataProgramacaoInicio.HasValue && model.DataProgramacaoFim.HasValue)
                {
                    reservaMaterials = reservaMaterials.Where(p => p.DataProgramacao.Date >= model.DataProgramacaoInicio.Value
                                                             && p.Data.Date <= model.DataProgramacaoFim.Value);
                    filtros.AppendFormat("Data de Programação de '{0}' até '{1}', ",
                                         model.DataProgramacaoInicio.Value.ToString("dd/MM/yyyy"),
                                         model.DataProgramacaoFim.Value.ToString("dd/MM/yyyy"));
                }

                if (model.SituacaoReservaMaterial.HasValue)
                {
                    reservaMaterials = reservaMaterials.Where(p => p.SituacaoReservaMaterial == model.SituacaoReservaMaterial);
                    filtros.AppendFormat("Situação: {0}, ", model.SituacaoReservaMaterial.Value.EnumToString());
                }

                #endregion

                // Verifica se é uma listagem
                if (model.ModoConsulta == "Listar")
                {
                    if (model.OrdenarPor != null)
                        reservaMaterials = model.OrdenarEm == "asc"
                            ? reservaMaterials.OrderBy(model.OrdenarPor)
                            : reservaMaterials.OrderByDescending(model.OrdenarPor);

                    model.Grid = reservaMaterials.Select(p => new GridReservaMaterialModel
                    {
                        Id = (long)p.Id,
                        Referencia = p.ReferenciaOrigem,
                        Numero = p.Numero,
                        Data = p.Data,
                        DataProgramacao = p.DataProgramacao,
                        Situacao = p.SituacaoReservaMaterial.EnumToString()
                    }).ToList();

                    return View(model);
                }

                // Se não é uma listagem, gerar o relatório
                var result = reservaMaterials.ToList();

                if (!result.Any())
                    return Json(new { Error = "Nenhum item encontrado." });

                #region Montar Relatório
                Report report = null;

                switch (model.TipoRelatorio)
                {
                    case "Detalhado":
                        var ficha = new FichaMaterialReport { DataSource = result };

                        if (filtros.Length > 2)
                            ficha.ReportParameters["Filtros"].Value = filtros.ToString().Substring(0, filtros.Length - 2);

                        report = ficha;
                        break;
                    case "Listagem":
                        var lista = new ListaMaterialReport { DataSource = result };

                        if (filtros.Length > 2)
                            lista.ReportParameters["Filtros"].Value = filtros.ToString().Substring(0, filtros.Length - 2);

                        var grupo = lista.Groups.First(p => p.Name.Equals("Grupo"));

                        if (model.AgruparPor != null)
                        {
                            grupo.Groupings.Add("=Fields." + model.AgruparPor);

                            var key = _colunasPesquisaReservaMaterial.First(p => p.Value == model.AgruparPor).Key;
                            var titulo = string.Format("= \"{0}: \" + Fields.{1}", key, model.AgruparPor);
                            grupo.GroupHeader.GetTextBox("Titulo").Value = titulo;
                        }
                        else
                        {
                            lista.Groups.Remove(grupo);
                        }

                        report = lista;
                        break;
                    case "Sintético":
                        var total = new TotalMaterialReport();

                        if (filtros.Length > 2)
                            total.ReportParameters["Filtros"].Value = filtros.ToString().Substring(0, filtros.Length - 2);

                        total.Grafico.DataSource = result;

                        // Altura do gráfico
                        total.Grafico.Height = Unit.Pixel(result.GroupBy(model.AgruparPor).Count() * 30 + 50);

                        // Agrupar
                        total.Grafico.Series[0].CategoryGroup.Groupings[0].Expression = "=Fields." + model.AgruparPor;

                        // Título
                        total.Grafico.Titles[0].Text = _colunasPesquisaReservaMaterial.FirstOrDefault(p => p.Value == model.AgruparPor).Key;

                        report = total;
                        break;
                }

                if (model.OrdenarPor != null)
                    report.Sortings.Add("=Fields." + model.OrdenarPor, model.OrdenarEm == "asc" ? SortDirection.Asc : SortDirection.Desc);
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

        #endregion

        #region Novo

        [PopulateViewData("PopulateViewDataNovoEditar")]
        public virtual ActionResult Novo()
        {
            return View(new ReservaMaterialModel());
        }

        [HttpPost, PopulateViewData("PopulateViewDataNovoEditar")]
        public virtual ActionResult Novo(ReservaMaterialModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<ReservaMaterial>(model);
                    domain.Data = DateTime.Now;
                    domain.Numero = ProximoNumero();

                    IncluaNovosReservaMaterialItens(model, domain);
                    
                    AtualizeSituacao(domain);
                    _reservaMaterialRepository.Save(domain);
                    
                    Framework.UnitOfWork.Session.Current.Flush();

                    this.AddSuccessMessage("Reserva material cadastrado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar a reserva de material. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }

        private long ProximoNumero()
        {
            try
            {
                return _reservaMaterialRepository.Find().Max(p => p.Numero) + 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }
        #endregion

        #region Editar

        [ImportModelStateFromTempData, PopulateViewData("PopulateViewDataNovoEditar")]
        public virtual ActionResult Editar(long? id)
        {
            var domain = _reservaMaterialRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<ReservaMaterialModel>(domain);

                model.GridItens = domain.ReservaMaterialItems.Select(x =>
                    new ReservaMaterialItemModel
                    {
                        Descricao = x.Material.Descricao,
                        Referencia = x.Material.Referencia,
                        SituacaoReservaMaterial = x.SituacaoReservaMaterial,
                        QuantidadeAtendida = x.QuantidadeAtendida,
                        QuantidadeCancelada = x.ReservaMaterialItemCancelado != null ? x.ReservaMaterialItemCancelado.QuantidadeCancelada : 0,
                        QuantidadeSolicitada = x.QuantidadeReserva,
                        UnidadeMedida = x.Material.UnidadeMedida.Sigla,
                        Id = x.Id
                    }).ToList();
                
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a reserva de material.");
            return RedirectToAction("Index");
        }

        [HttpPost, PopulateViewData("PopulateViewDataNovoEditar")]
        public virtual ActionResult Editar(ReservaMaterialModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = _reservaMaterialRepository.Get(model.Id);
                    
                    Framework.UnitOfWork.Session.Current.Evict(domain.Requerente);

                    domain = Mapper.Unflat(model, domain);

                    domain.Requerente = _pessoaRepository.Get(model.Requerente);

                    ExcluaReservaMaterialItens(model, domain);
                    AtualizeReservaMaterialItens(model, domain);
                    IncluaNovosReservaMaterialItens(model, domain);
                    AtualizeSituacao(domain);

                    _reservaMaterialRepository.SaveOrUpdate(domain);
                    Framework.UnitOfWork.Session.Current.Flush();
                    this.AddSuccessMessage("Reserva de material atualizado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a reserva de material. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }

        private void AtualizeSituacao(ReservaMaterial reservaMaterial)
        {
            reservaMaterial.ReservaMaterialItems.ForEach(x => x.AtualizeSituacao());
            reservaMaterial.AtualizeSituacao();
        }

        private void IncluaNovosReservaMaterialItens(ReservaMaterialModel reservaMaterialModel, ReservaMaterial reservaMaterial)
        {
            reservaMaterialModel.GridItens.ForEach(x =>
            {
                var reservaMaterialItem = reservaMaterial.ReservaMaterialItems.FirstOrDefault(y => y.Id == x.Id);
                if (reservaMaterialItem == null)
                {
                    reservaMaterialItem = new ReservaMaterialItem
                    {
                        Material = _materialRepository.Find(y => y.Referencia == x.Referencia).First(),
                        QuantidadeAtendida = x.QuantidadeAtendida,
                        QuantidadeReserva = x.QuantidadeSolicitada,
                        SituacaoReservaMaterial = SituacaoReservaMaterial.NaoAtendida
                    };
                    reservaMaterial.ReservaMaterialItems.Add(reservaMaterialItem);
                }
            });
        }

        private void AtualizeReservaMaterialItens(ReservaMaterialModel reservaMaterialModel, ReservaMaterial reservaMaterial)
        {
            reservaMaterialModel.GridItens.ForEach(x =>
            {
                var reservaMaterialItem = reservaMaterial.ReservaMaterialItems.FirstOrDefault(y => y.Id == x.Id);
                if (reservaMaterialItem != null)
                {
                    reservaMaterialItem.QuantidadeAtendida = x.QuantidadeAtendida;
                    reservaMaterialItem.QuantidadeReserva = x.QuantidadeSolicitada;
                }
            });
        }

        private void ExcluaReservaMaterialItens(ReservaMaterialModel reservaMaterialModel, ReservaMaterial reservaMaterial)
        {
            var reservaMaterialItensExcluir = new List<ReservaMaterialItem>();

            reservaMaterial.ReservaMaterialItems.ForEach(x =>
            {
                if (reservaMaterialModel.GridItens.All(y => y.Id != x.Id))
                {
                    reservaMaterialItensExcluir.Add(x);
                }
            });

            reservaMaterialItensExcluir.ForEach(x => reservaMaterial.ReservaMaterialItems.Remove(x));
        }

        #endregion

        #region Excluir

        [HttpPost, ValidateAntiForgeryToken, ExportModelStateToTempData]
        public virtual ActionResult Excluir(long? id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = _reservaMaterialRepository.Get(id);
                    _reservaMaterialRepository.Delete(domain);

                    this.AddSuccessMessage("Reserva de material excluído com sucesso");

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir a reserva de material: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return RedirectToAction("Editar", new { id });
        }
        #endregion

        #endregion

        #region Métodos

        #region PopulateViewData
        protected void PopulateViewData(PesquisaReservaMaterialModel model)
        {
            var unidades = _pessoaRepository.Find(p => p.Unidade != null).OrderBy(o => o.NomeFantasia).ToList();
            ViewData["Unidade"] = unidades.ToSelectList("NomeFantasia", model.Unidade);
            
            ViewBag.AgruparPor = new SelectList(_colunasPesquisaReservaMaterial, "value", "key");
            ViewBag.OrdenarPor = new SelectList(_colunasPesquisaReservaMaterial, "value", "key");
        }

        protected void PopulateViewDataNovoEditar(ReservaMaterialModel model)
        {
            var unidades = _pessoaRepository.Find(p => p.Unidade != null).OrderBy(o => o.NomeFantasia).ToList();
            ViewData["Unidade"] = unidades.ToSelectList("NomeFantasia", model.Unidade);

            var colecoes = _colecaoRepository.Find().OrderBy(o => o.Descricao).ToList();
            ViewData["Colecao"] = colecoes.ToSelectList("Descricao", model.Colecao);
        }
        #endregion

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            //var material = (MaterialModel)model;

            //// Verificar se existe catálogo com esta referência
            //if (_materialRepository.Find().Any(p => p.Referencia == material.Referencia && p.Id != material.Id))
            //    ModelState.AddModelError("Referencia", "Já existe catálogo de material cadastrado com esta referência.");

            //// Verificar se existe catálogo com este código de barras
            //if (string.IsNullOrWhiteSpace(material.CodigoBarra) == false &&
            //    _materialRepository.Find().Any(p => p.CodigoBarra == material.CodigoBarra && p.Id != material.Id))
            //    ModelState.AddModelError("CodigoBarra", "Já existe catálogo de material cadastrado com este código de barras.");
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
        }
        #endregion

        #region PreecheColunasPesquisa
        private void PreecheColunasPesquisa()
        {
            _colunasPesquisaReservaMaterial = new Dictionary<string, string>
                           {
                               {"Coleção", "Colecao.Descricao"},
                               {"Data de Programação", "DataProgramacao"},
                               {"Unidade", "Unidade.NomeFantasia"},
                               {"Situação", "SituacaoReservaMaterial"}
                           };
        }
        #endregion

        #endregion

        #region Actions Grid
        //Não são utilizadas pois as alterações são realizadas no submit e não durante a edição
        public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(request);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Create([DataSourceRequest] DataSourceRequest request, ReservaMaterialItemModel reservaMaterialItemModel)
        {
            return Json(new[] { reservaMaterialItemModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, ReservaMaterialItemModel reservaMaterialItemModel)
        {
            return Json(new[] { reservaMaterialItemModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Destroy([DataSourceRequest] DataSourceRequest request, ReservaMaterialItemModel reservaMaterialItemModel)
        {
            return Json(new[] { reservaMaterialItemModel }.ToDataSourceResult(request, ModelState));
        }
        #endregion
    }
}