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
using Fashion.Framework.Mvc.Security;
using Fashion.Framework.Repository;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using NHibernate.Linq;
using Ninject.Extensions.Logging;
using Telerik.Reporting;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Controllers
{
    public partial class RequisicaoMaterialController : BaseController
    {
        #region Variaveis

        private readonly IRepository<RequisicaoMaterial> _requisicaoMaterialRepository;
        private readonly IRepository<ReservaMaterial> _reservaMaterialRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<Usuario> _usuarioRepository;
        private readonly IRepository<TipoItem> _tipoItemRepository;
        private readonly IRepository<CentroCusto> _centroCustoRepository;
        private readonly IRepository<Material> _materialRepository;
        private readonly IRepository<DepositoMaterial> _depositoMaterialRepository;
        private readonly IRepository<ReservaEstoqueMaterial> _reservaEstoqueMaterialRepository;
        private readonly ILogger _logger;
        private Dictionary<string, string> _colunasPesquisaReservaMaterial;
        #endregion

        #region Construtores
        public RequisicaoMaterialController(ILogger logger, IRepository<TipoItem> tipoItemRepository,
            IRepository<ReservaMaterial> reservaMaterialRepository, IRepository<Pessoa> pessoaRepository,
            IRepository<Material> materialRepository, IRepository<ReservaEstoqueMaterial> reservaEstoqueMaterialRepository,
            IRepository<RequisicaoMaterial> requisicaoMaterialRepository, IRepository<CentroCusto> centroCustoRepository,
            IRepository<DepositoMaterial> depositoMaterialRepository, IRepository<Usuario> usuarioRepository )
        {
            _reservaMaterialRepository = reservaMaterialRepository;
            _pessoaRepository = pessoaRepository;
            _tipoItemRepository = tipoItemRepository;
            _materialRepository = materialRepository;
            _reservaEstoqueMaterialRepository = reservaEstoqueMaterialRepository;
            _requisicaoMaterialRepository = requisicaoMaterialRepository;
            _centroCustoRepository = centroCustoRepository;
            _depositoMaterialRepository = depositoMaterialRepository;
            _usuarioRepository = usuarioRepository;

            _logger = logger;

            PreecheColunasPesquisa();
        }
        #endregion

        #region Views

        #region Index
        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Index()
        {
            var requisicaoMaterials = _requisicaoMaterialRepository.Find();

            var model = new PesquisaRequisicaoMaterialModel { ModoConsulta = "Listar" };

            model.Grid = requisicaoMaterials.Select(p => new GridRequisicaoMaterialModel
            {
                Id = p.Id.Value,
                Origem = p.Origem,
                Numero = p.Numero,
                Data = p.Data,
                TipoMaterial = p.TipoItem.Descricao,
                Requerente = p.Requerente.Nome,
                Situacao = p.SituacaoRequisicaoMaterial.EnumToString(),
                UnidadeRequerente = p.UnidadeRequerente.NomeFantasia
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
            var usuarioId = FashionSecurity.GetLoggedUserId();
            var usuario = _usuarioRepository.Get(usuarioId);

            long funcionarioId = 0L;
            if (usuario.Funcionario != null)
            {
                funcionarioId = usuario.Funcionario.Id ?? usuario.Funcionario.Id.Value;
            }

            return View(new RequisicaoMaterialModel
            {
                Requerente = funcionarioId
            });
        }

        [HttpPost, PopulateViewData("PopulateViewDataNovoEditar")]
        public virtual ActionResult Novo(RequisicaoMaterialModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<RequisicaoMaterial>(model);
                    domain.Data = DateTime.Now;
                    domain.Numero = ProximoNumero();
                    domain.UnidadeRequerente = _pessoaRepository.Get(model.UnidadeRequerente);
                    domain.UnidadeRequisitada = _pessoaRepository.Get(model.UnidadeRequisitada);
                    domain.Requerente = _pessoaRepository.Get(model.Requerente);
                    domain.CentroCusto = _centroCustoRepository.Get(model.CentroCusto);
                    domain.TipoItem = _tipoItemRepository.Get(model.TipoItem);

                    IncluaNovosRequisicaoMaterialItens(model, domain);

                    domain.AtualizeSituacao();
                    domain.CrieReservaMaterial(ProximoNumeroReservaMaterial(), _reservaEstoqueMaterialRepository);

                    _requisicaoMaterialRepository.Save(domain);

                    this.AddSuccessMessage("Requisição de material cadastrado com sucesso.");
                }
                catch (Exception exception)
                {
                    var errorMsg = "Não é possível salvar a requisição de material. Confira se os dados foram informados corretamente: " +
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
                return _requisicaoMaterialRepository.Find().Max(p => p.Numero) + 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        private long ProximoNumeroReservaMaterial()
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
            var domain = _requisicaoMaterialRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<RequisicaoMaterialModel>(domain);
                
                model.GridItens = domain.RequisicaoMaterialItems.Select(x =>
                    new RequisicaoMaterialItemModel
                    {
                        Descricao = x.Material.Descricao,
                        Referencia = x.Material.Referencia,
                        SituacaoRequisicaoMaterial = x.SituacaoRequisicaoMaterial,
                        QuantidadeAtendida = x.QuantidadeAtendida,
                        QuantidadeCancelada = x.RequisicaoMaterialItemCancelado != null ? x.RequisicaoMaterialItemCancelado.QuantidadeCancelada : 0,
                        QuantidadeSolicitada = x.QuantidadeSolicitada,
                        UnidadeMedida = x.Material.UnidadeMedida.Sigla,
                        Id = x.Id
                    }).ToList();
                
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a requisição de material.");
            return RedirectToAction("Index");
        }

        [HttpPost, PopulateViewData("PopulateViewDataNovoEditar")]
        public virtual ActionResult Editar(RequisicaoMaterialModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = _requisicaoMaterialRepository.Get(model.Id);
                    
                    Framework.UnitOfWork.Session.Current.Evict(domain.Requerente);

                    domain = Mapper.Unflat(model, domain);

                    domain.Requerente = _pessoaRepository.Get(model.Requerente);
                    domain.UnidadeRequerente = _pessoaRepository.Get(model.UnidadeRequerente);
                    domain.UnidadeRequisitada = _pessoaRepository.Get(model.UnidadeRequisitada);
                    domain.CentroCusto = _centroCustoRepository.Get(model.CentroCusto);
                    domain.TipoItem = _tipoItemRepository.Get(model.TipoItem);

                    ExcluaRequisicaoMaterialItens(model, domain);
                    AtualizeRequisicaoMaterialItens(model, domain);
                    IncluaNovosRequisicaoMaterialItens(model, domain);
                    domain.AtualizeSituacao();
                    domain.AtualizeReservaMaterial(_reservaEstoqueMaterialRepository);

                    _requisicaoMaterialRepository.SaveOrUpdate(domain);

                    this.AddSuccessMessage("Requisição de material atualizado com sucesso.");
                }
                catch (Exception exception)
                {
                    var errorMsg = "Não é possível salvar a requisição de material. Confira se os dados foram informados corretamente: " +
                        exception.Message;
                    this.AddErrorMessage(errorMsg);
                    _logger.Info(exception.GetMessage());

                    return new JsonResult { Data = "error" };
                }
            }

            return new JsonResult { Data = "sucesso" };
        }

        private void IncluaNovosRequisicaoMaterialItens(RequisicaoMaterialModel requisicaoMaterialModel, RequisicaoMaterial requisicaoMaterial)
        {
            requisicaoMaterialModel.GridItens.ForEach(x =>
            {
                var requisicaoMaterialItem = requisicaoMaterial.RequisicaoMaterialItems.FirstOrDefault(y => y.Id == x.Id);
                var material = _materialRepository.Find(y => y.Referencia == x.Referencia).First();
                if (requisicaoMaterialItem == null)
                {
                    requisicaoMaterialItem = new RequisicaoMaterialItem()
                    {
                        Material = material,
                        QuantidadeAtendida = x.QuantidadeAtendida,
                        QuantidadeSolicitada = x.QuantidadeSolicitada,
                        SituacaoRequisicaoMaterial = SituacaoRequisicaoMaterial.NaoAtendido
                    };
                    requisicaoMaterial.RequisicaoMaterialItems.Add(requisicaoMaterialItem);
                }
            });
        }

        private void AtualizeRequisicaoMaterialItens(RequisicaoMaterialModel requisicaoMaterialModel, RequisicaoMaterial requisicaoMaterial)
        {
            requisicaoMaterialModel.GridItens.ForEach(x =>
            {
                var reservaMaterialItem = requisicaoMaterial.RequisicaoMaterialItems.FirstOrDefault(y => y.Id == x.Id);
                if (reservaMaterialItem != null)
                {
                    reservaMaterialItem.QuantidadeSolicitada = x.QuantidadeSolicitada;
                }
            });
        }

        private void ExcluaRequisicaoMaterialItens(RequisicaoMaterialModel requisicaoMaterialModel, RequisicaoMaterial requisicaoMaterial)
        {
            var requisicaoMaterialsItensExcluir = new List<RequisicaoMaterialItem>();

            requisicaoMaterial.RequisicaoMaterialItems.ForEach(x =>
            {
                if (requisicaoMaterialModel.GridItens.All(y => y.Id != x.Id))
                {
                    requisicaoMaterialsItensExcluir.Add(x);
                }
            });

            requisicaoMaterialsItensExcluir.ForEach(x => requisicaoMaterial.RequisicaoMaterialItems.Remove(x));
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
                    var domain = _requisicaoMaterialRepository.Get(id);

                    domain.ReservaMaterial.AtualizeReservaEstoqueMaterialAoExcluir(_reservaEstoqueMaterialRepository);

                    _requisicaoMaterialRepository.Delete(domain);

                    this.AddSuccessMessage("Requisição de material excluída com sucesso");

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir a requisição de material: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return RedirectToAction("Editar", new { id });
        }
        #endregion

        #endregion

        #region Métodos

        #region PopulateViewData
        protected void PopulateViewData(PesquisaRequisicaoMaterialModel model)
        {
            var unidades = _pessoaRepository.Find(p => p.Unidade != null).OrderBy(o => o.NomeFantasia).ToList();
            ViewData["UnidadeRequerente"] = unidades.ToSelectList("NomeFantasia", model.UnidadeRequerente);
            
            ViewBag.AgruparPor = new SelectList(_colunasPesquisaReservaMaterial, "value", "key");
            ViewBag.OrdenarPor = new SelectList(_colunasPesquisaReservaMaterial, "value", "key");
        }

        protected void PopulateViewDataNovoEditar(RequisicaoMaterialModel model)
        {
            var unidades = _pessoaRepository.Find(p => p.Unidade != null).OrderBy(o => o.NomeFantasia).ToList();
            ViewData["UnidadeRequerente"] = unidades.ToSelectList("NomeFantasia", model.UnidadeRequerente);

            var unidadesRequisitadas = _depositoMaterialRepository.Find(p => p.Unidade != null && p.Ativo).Select(x => x.Unidade).ToList()
                .GroupBy(x => x.Id).Select(x => x.First()).OrderBy(o => o.NomeFantasia).ToList();

            ViewData["UnidadeRequisitada"] = unidadesRequisitadas.ToSelectList("NomeFantasia", model.UnidadeRequisitada);
            
            var tipoItems = _tipoItemRepository.Find().OrderBy(o => o.Descricao).ToList();
            ViewData["TipoItem"] = tipoItems.ToSelectList("Descricao", model.TipoItem);

            var centroCustos = _centroCustoRepository.Find().OrderBy(o => o.Nome).ToList();
            ViewData["CentroCusto"] = centroCustos.ToSelectList("Nome", model.CentroCusto);
        }
        #endregion

        #region ValidaNovoOuEditar

        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            
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