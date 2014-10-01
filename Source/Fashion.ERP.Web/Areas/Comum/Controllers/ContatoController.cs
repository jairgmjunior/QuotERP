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
    public partial class ContatoController : BaseController
    {
        #region Variaveis
        private readonly IRepository<Contato> _contatoRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public ContatoController(ILogger logger, IRepository<Contato> contatoRepository)
        {
            _logger = logger;
            _contatoRepository = contatoRepository;
        }
        #endregion

        #region Views

        #region Index
        [ChildActionOnly]
        public virtual ActionResult Index(long pessoaId)
        {
            TempData["pessoaId"] = pessoaId;
            return View();
        }

        #endregion

        #region LerContatos
        [AjaxOnly, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual ActionResult LerContatos([DataSourceRequest] DataSourceRequest request, long pessoaId)
        {
            if (request.PageSize == 0)
                request.PageSize = 4;

            var query = _contatoRepository.Find(p => p.Pessoa.Id == pessoaId);

            var list = query.Select(p => new GridContatoModel
            {
                Id = p.Id.GetValueOrDefault(),
                TipoContato = p.TipoContato.EnumToString(),
                Nome = p.Nome,
                Telefone = p.Telefone,
                Operadora = p.Operadora,
                Email = p.Email
            });

            var result = list.ToDataSourceResult(request);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Novo

        [HttpGet, AjaxOnly]
        public virtual ActionResult Novo(long pessoaId)
        {
            var model = new ContatoModel { Pessoa = pessoaId };
            return View(model);
        }

        [HttpPost, AjaxOnly]
        public virtual ContentResult Novo(ContatoModel model)
        {
            if (ModelState.IsValid)
            {
                var domain = Mapper.Unflat<Contato>(model);

                _contatoRepository.Save(domain);

                return Content("Contato cadastrado com sucesso!");
            }

            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Content(ModelState.JoinErrors("\r\n"));
        }
        #endregion

        #region Excluir

        [HttpPost, AjaxOnly, ExportModelStateToTempData]
        public virtual JsonResult Excluir([DataSourceRequest] DataSourceRequest request, GridContatoModel model)
        {
            if (ModelState.IsValid)
            {
                var domain = _contatoRepository.Get(model.Id);

                if (domain != null)
                    _contatoRepository.Delete(domain);
                else
                    ModelState.AddModelError(string.Empty, "Não foi possível excluir este contato, ID informado não corresponde a um item existente");
            }

            return Json(ModelState.ToDataSourceResult());
        }
        #endregion

        #endregion
        
        #region Métodos

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var contato = model as ContatoModel;

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