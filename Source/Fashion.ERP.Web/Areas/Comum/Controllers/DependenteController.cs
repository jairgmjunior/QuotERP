using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Areas.Comum.Models;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Repository;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Comum.Controllers
{
    public partial class DependenteController : BaseController
    {
        #region Variaveis
        private readonly IRepository<Dependente> _dependenteRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<GrauDependencia> _grauDependenciaRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public DependenteController(ILogger logger, IRepository<Dependente> dependenteRepository,
            IRepository<Pessoa> pessoaRepository, IRepository<GrauDependencia> grauDependenciaRepository)
        {
            _logger = logger;
            _dependenteRepository = dependenteRepository;
            _pessoaRepository = pessoaRepository;
            _grauDependenciaRepository = grauDependenciaRepository;
        }
        #endregion

        #region Views

        #region Index
        [ChildActionOnly]
        public virtual ActionResult Index(long pessoaId)
        {
            TempData["clienteId"] = pessoaId;
            return View();
        }

        #endregion

        #region LerDependentes
        [AjaxOnly, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual ActionResult LerDependentes([DataSourceRequest] DataSourceRequest request, long clienteId)
        {
            if (request.PageSize == 0)
                request.PageSize = 4;

            var cliente = _pessoaRepository.Load(clienteId);

            var dependentes = cliente.Cliente.Dependentes;

            var list = dependentes.Select(p => new GridDependenteModel
            {
                Id = p.Id.GetValueOrDefault(),
                Nome = p.Nome,
                GrauDependencia = p.GrauDependencia.Descricao
            });

            var result = list.ToDataSourceResult(request);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Novo

        [HttpGet, AjaxOnly, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(long pessoaId)
        {
            var model = new DependenteModel { Cliente = pessoaId };
            return View(model);
        }

        [HttpPost, AjaxOnly]
        public virtual ContentResult Novo(DependenteModel model)
        {
            if (ModelState.IsValid)
            {
                var domain = Mapper.Unflat<Dependente>(model);
                var cliente = _pessoaRepository.Get(model.Cliente);
                cliente.Cliente.AddDependente(domain);
                _pessoaRepository.Save(cliente);

                return Content("Dependente cadastrado com sucesso!");
            }

            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Content(ModelState.JoinErrors("\r\n"));
        }
        #endregion

        #region Excluir

        [HttpPost, AjaxOnly]
        public virtual JsonResult Excluir([DataSourceRequest] DataSourceRequest request, GridDependenteModel model)
        {
            var domain = _dependenteRepository.Get(model.Id);

            if (domain != null)
                _dependenteRepository.Delete(domain);
            else
                ModelState.AddModelError(string.Empty, "Não foi possível excluir este dependente, ID informado não corresponde a um item existente");

            return Json(ModelState.ToDataSourceResult());
        }
        #endregion

        #endregion

        #region Métodos

        #region PopulateViewData
        protected void PopulateViewData(DependenteModel model)
        {
            var grauDependencias = _grauDependenciaRepository.Find().OrderBy(o => o.Descricao).ToList();
            ViewData["GrauDependencia"] = grauDependencias.ToSelectList("Descricao", model.GrauDependencia);
        }
        #endregion

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var dependente = model as DependenteModel;

        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
        }
        #endregion

        #endregion
    }
}