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

namespace Fashion.ERP.Web.Areas.Almoxarifado.Controllers
{
    public partial class ReservaMaterialController : BaseController
    {
        #region Variaveis
        private readonly IRepository<ReservaMaterial> _reservaMaterialRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<Colecao> _colecaoRepository;
        private readonly IRepository<Material> _materialRepository;
        private readonly IRepository<ReservaEstoqueMaterial> _reservaEstoqueMaterialRepository;
        private readonly IRepository<RequisicaoMaterial> _requisicaoMaterialRepository;
        private readonly ILogger _logger;
        private Dictionary<string, string> _colunasPesquisaReservaMaterial;
        #endregion

        #region Construtores
        public ReservaMaterialController(ILogger logger, IRepository<Colecao> colecaoRepository,
            IRepository<ReservaMaterial> reservaMaterialRepository,  IRepository<Pessoa> pessoaRepository,
            IRepository<Material> materialRepository, IRepository<ReservaEstoqueMaterial> reservaEstoqueMaterialRepository,
            IRepository<RequisicaoMaterial> requisicaoMaterialRepository)
        {
            _reservaMaterialRepository = reservaMaterialRepository;
            _pessoaRepository = pessoaRepository;
            _colecaoRepository = colecaoRepository;
            _materialRepository = materialRepository;
            _reservaEstoqueMaterialRepository = reservaEstoqueMaterialRepository;
            _requisicaoMaterialRepository = requisicaoMaterialRepository;

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
                Id = p.Id.Value,
                Referencia = p.ReferenciaOrigem,
                Numero = p.Numero,
                Data = p.Data,
                DataProgramacao = p.DataProgramacao.HasValue ? p.DataProgramacao.Value : default(DateTime?),
                Situacao = p.SituacaoReservaMaterial.EnumToString(),
                Colecao = p.Colecao.Descricao,
                Unidade = p.Unidade.NomeFantasia,
            }).OrderBy(o => o.Numero).ToList();

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
                    reservaMaterials = reservaMaterials.Where(p => p.DataProgramacao.Value.Date >= model.DataProgramacaoInicio.Value
                                                             && p.DataProgramacao.Value.Date <= model.DataProgramacaoFim.Value);
                    filtros.AppendFormat("Data de Programação de '{0}' até '{1}', ",
                                         model.DataProgramacaoInicio.Value.ToString("dd/MM/yyyy"),
                                         model.DataProgramacaoFim.Value.ToString("dd/MM/yyyy"));
                }

                if (model.SituacaoReservaMaterial.HasValue)
                {
                    reservaMaterials = reservaMaterials.Where(p => p.SituacaoReservaMaterial == model.SituacaoReservaMaterial);
                    filtros.AppendFormat("Situação: {0}, ", model.SituacaoReservaMaterial.Value.EnumToString());
                }

                if (model.Material.HasValue)
                {
                    reservaMaterials = reservaMaterials.Where(p => p.ReservaMaterialItems.Any(i => i.Material.Id == model.Material));
                    filtros.AppendFormat("Material: {0}, ", _materialRepository.Get(model.Material.Value).Descricao);
                }

                if (!string.IsNullOrWhiteSpace(model.ReferenciaExterna))
                {
                    reservaMaterials =
                        reservaMaterials.Where(
                            p =>
                                p.ReservaMaterialItems.SelectMany(x => x.Material.ReferenciaExternas)
                                    .Any(y => y.Referencia == model.ReferenciaExterna));
                    filtros.AppendFormat("Referência externa: {0}, ", model.ReferenciaExterna);
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
                        Id = p.Id.Value,
                        Referencia = p.ReferenciaOrigem,
                        Numero = p.Numero,
                        Data = p.Data,
                        DataProgramacao = p.DataProgramacao,
                        Situacao = p.SituacaoReservaMaterial.EnumToString(),
                        Colecao = p.Colecao.Descricao,
                        Unidade = p.Unidade.NomeFantasia
                    }).OrderBy(o => o.Numero).ToList();

                    return View(model);
                }

                // Se não é uma listagem, gerar o relatório
                var result = reservaMaterials.ToList();

                if (!result.Any())
                    return Json(new { Error = "Nenhum item encontrado." });

                #region Montar Relatório
                var report = new ListaReservaMaterialReport() { DataSource = result };

                if (filtros.Length > 2)
                    report.ReportParameters["Filtros"].Value = filtros.ToString().Substring(0, filtros.Length - 2);

                var grupo = report.Groups.First(p => p.Name.Equals("Grupo"));

                if (model.AgruparPor != null)
                {
                    grupo.Groupings.Add("= AjusteValores(Fields." + model.AgruparPor + ")");

                    var key = _colunasPesquisaReservaMaterial.First(p => p.Value == model.AgruparPor).Key;
                    var titulo = string.Format("= \"{0}: \" + AjusteValores(Fields.{1})", key, model.AgruparPor);
                    grupo.GroupHeader.GetTextBox("Titulo").Value = titulo;
                }
                else
                {
                    report.Groups.Remove(grupo);
                }

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

                    domain.AtualizeSituacao();
                    _reservaMaterialRepository.Save(domain);
                    
                    this.AddSuccessMessage("Reserva de material cadastrada com sucesso.");
                }
                catch (Exception exception)
                {
                    var errorMsg = "Não é possível salvar a reserva de material. Confira se os dados foram informados corretamente: " +
                        exception.Message;
                    this.AddErrorMessage(errorMsg);
                    _logger.Info(exception.GetMessage());

                    return new JsonResult { Data = "error" };
                }
            }

            return new JsonResult { Data = "sucesso" };
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
                model.PermiteAlterar = PermiteAlterar(id);

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

        private bool PermiteAlterar(long? id)
        {
            var requisicaoMaterial =
            _requisicaoMaterialRepository.Get(y => y.ReservaMaterial.Id == id);
            if (requisicaoMaterial != null)
            {
                return false;
            }

            return true;
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
                    domain.AtualizeSituacao();

                    _reservaMaterialRepository.SaveOrUpdate(domain);

                    this.AddSuccessMessage("Reserva de material atualizado com sucesso.");
                }
                catch (Exception exception)
                {
                    var errorMsg = "Não é possível salvar a reserva de material. Confira se os dados foram informados corretamente: " +
                        exception.Message;
                    this.AddErrorMessage(errorMsg);
                    _logger.Info(exception.GetMessage());

                    return new JsonResult { Data = "error" };
                }
            }

            return new JsonResult { Data = "sucesso" };
        }


        private void IncluaNovosReservaMaterialItens(ReservaMaterialModel reservaMaterialModel, ReservaMaterial reservaMaterial)
        {
            reservaMaterialModel.GridItens.ForEach(x =>
            {
                var reservaMaterialItem = reservaMaterial.ReservaMaterialItems.FirstOrDefault(y => y.Id == x.Id);
                var material = _materialRepository.Find(y => y.Referencia == x.Referencia).First();
                if (reservaMaterialItem == null)
                {
                    reservaMaterialItem = new ReservaMaterialItem
                    {
                        Material = material,
                        QuantidadeAtendida = x.QuantidadeAtendida,
                        QuantidadeReserva = x.QuantidadeSolicitada,
                        SituacaoReservaMaterial = SituacaoReservaMaterial.NaoAtendida
                    };
                    reservaMaterial.ReservaMaterialItems.Add(reservaMaterialItem);

                    reservaMaterial.AtualizeReservaEstoqueMaterial(reservaMaterialItem.QuantidadeReserva, material, reservaMaterial.Unidade, _reservaEstoqueMaterialRepository);
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
                    if (reservaMaterialItem.QuantidadeReserva != x.QuantidadeSolicitada)
                    {
                        var diferenca = x.QuantidadeSolicitada - reservaMaterialItem.QuantidadeReserva;
                        reservaMaterialItem.QuantidadeReserva = x.QuantidadeSolicitada;

                        reservaMaterial.AtualizeReservaEstoqueMaterial(diferenca, reservaMaterialItem.Material, reservaMaterial.Unidade, _reservaEstoqueMaterialRepository);
                    }
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

            reservaMaterialItensExcluir.ForEach(x =>
            {
                reservaMaterial.ReservaMaterialItems.Remove(x);
                reservaMaterial.AtualizeReservaEstoqueMaterial(x.QuantidadeReserva * -1, x.Material, reservaMaterial.Unidade, _reservaEstoqueMaterialRepository);
            });
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

                    domain.AtualizeReservaEstoqueMaterialAoExcluir(_reservaEstoqueMaterialRepository);

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
        public virtual ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(request);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult EditingInline_Create([DataSourceRequest] DataSourceRequest request, ReservaMaterialItemModel reservaMaterialItemModel)
        {
            return Json(new[] { reservaMaterialItemModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, ReservaMaterialItemModel reservaMaterialItemModel)
        {
            return Json(new[] { reservaMaterialItemModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult EditingInline_Destroy([DataSourceRequest] DataSourceRequest request, ReservaMaterialItemModel reservaMaterialItemModel)
        {
            return Json(new[] { reservaMaterialItemModel }.ToDataSourceResult(request, ModelState));
        }
        #endregion
    }
}