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
    public partial class ReferenciaController : BaseController
    {
        #region Variaveis
        private readonly IRepository<Referencia> _referenciaRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public ReferenciaController(ILogger logger, IRepository<Referencia> referenciaRepository, 
            IRepository<Pessoa> pessoaRepository)
        {
            _logger = logger;
            _referenciaRepository = referenciaRepository;
            _pessoaRepository = pessoaRepository;
        }
        #endregion

        #region Views

        #region Index
        [ChildActionOnly]
        public virtual ActionResult Index(long pessoaId)
        {
            //var cliente = _pessoaRepository.Get(pessoaId);
            TempData["pessoaId"] = pessoaId;

            //if (cliente != null)
            //{
            //    var clienteId = cliente.Id ?? 0;
            //    TempData["clienteId"] = clienteId;
                
            //}
            return View();
        }

        #endregion

        #region LerReferencias
        [AjaxOnly, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual ActionResult LerReferencias([DataSourceRequest] DataSourceRequest request, long pessoaId)
        {
            if (request.PageSize == 0)
                request.PageSize = 4;

            var cliente = _pessoaRepository.Load(pessoaId);

            var referencias = cliente.Cliente.Referencias;

            var list = referencias.Select(p => new GridReferenciaModel
            {
                Id = p.Id.GetValueOrDefault(),
                TipoReferencia = p.TipoReferencia.EnumToString(),
                Nome = p.Nome,
                Telefone = p.Telefone,
            });

            var result = list.ToDataSourceResult(request);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Novo

        [HttpGet, AjaxOnly]
        public virtual ActionResult Novo(long pessoaId)
        {
            var model = new ReferenciaModel { Cliente = pessoaId };
            return View(model);
        }

        [HttpPost, AjaxOnly]
        public virtual ContentResult Novo(ReferenciaModel model)
        {
            if (ModelState.IsValid)
            {
                var domain = Mapper.Unflat<Referencia>(model);
                var pessoa = _pessoaRepository.Get(model.Cliente);
                //pessoa.Cliente.AddReferencia(domain);
                domain.Cliente = pessoa.Cliente;

                _referenciaRepository.Save(domain);

                return Content("Referência cadastrada com sucesso!");
            }

            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Content(ModelState.JoinErrors("\r\n"));
        }
        #endregion

        #region Excluir

        [HttpPost, AjaxOnly]
        public virtual JsonResult Excluir([DataSourceRequest] DataSourceRequest request, GridReferenciaModel model)
        {
            var domain = _referenciaRepository.Get(model.Id);

            if (domain != null)
                _referenciaRepository.Delete(domain);
            else
                ModelState.AddModelError(string.Empty, "Não foi possível excluir esta referência, ID informado não corresponde a um item existente");

            return Json(ModelState.ToDataSourceResult());
        }
        #endregion

        #endregion

        #region Métodos

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var referencia = model as ReferenciaModel;

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