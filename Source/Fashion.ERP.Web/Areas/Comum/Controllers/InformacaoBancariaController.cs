using System.Globalization;
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
    public partial class InformacaoBancariaController : BaseController
    {
        #region Variaveis
        private readonly IRepository<InformacaoBancaria> _informacaoBancariaRepository;
        private readonly IRepository<Banco> _bancoRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public InformacaoBancariaController(ILogger logger, IRepository<InformacaoBancaria> informacaoBancariaRepository, 
            IRepository<Banco> bancoRepository)
        {
            _logger = logger;
            _informacaoBancariaRepository = informacaoBancariaRepository;
            _bancoRepository = bancoRepository;
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

        #region LerInformacaoBancarias
        [AjaxOnly, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual ActionResult LerInformacaoBancarias([DataSourceRequest] DataSourceRequest request, long pessoaId)
        {
            if (request.PageSize == 0)
                request.PageSize = 4;

            var query = _informacaoBancariaRepository.Find(p => p.Pessoa.Id == pessoaId);

            var list = query.Select(p => new GridInformacaoBancariaModel
            {
                Id = p.Id.GetValueOrDefault(),
                Banco = p.Banco.Codigo.ToString(CultureInfo.InvariantCulture),
                Agencia = p.Agencia,
                Conta = p.Conta,
                TipoConta = p.TipoConta.HasValue ? p.TipoConta.Value.EnumToString() : string.Empty,
                DataAbertura = p.DataAbertura.HasValue ? p.DataAbertura.ToString() : string.Empty,
                Titular = p.Titular,
                Telefone = p.Telefone
            });

            var result = list.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Novo

        [HttpGet, AjaxOnly]
        public virtual ActionResult Novo(long pessoaId)
        {
            var model = new InformacaoBancariaModel { Pessoa = pessoaId };

            var bancos = _bancoRepository.Find().OrderBy(o => o.Codigo).Select(p => new { p.Id, Nome = p.Codigo + " - " + p.Nome }).ToList();
            ViewData["Banco"] = new SelectList(bancos, "Id", "Nome");
            return View(model);
        }

        [HttpPost, AjaxOnly]
        public virtual ContentResult Novo(InformacaoBancariaModel model)
        {
            if (ModelState.IsValid)
            {
                var domain = Mapper.Unflat<InformacaoBancaria>(model);

                _informacaoBancariaRepository.Save(domain);

                return Content("Informação bancária cadastrada com sucesso!");
            }

            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Content(ModelState.JoinErrors("\r\n"));
        }
        #endregion

        #region Excluir

        [HttpPost, AjaxOnly]
        public virtual JsonResult Excluir([DataSourceRequest] DataSourceRequest request, GridInformacaoBancariaModel model)
        {
            var domain = _informacaoBancariaRepository.Get(model.Id);

            if (domain != null)
                _informacaoBancariaRepository.Delete(domain);
            else
                ModelState.AddModelError(string.Empty, "Não foi possível excluir esta informação bancária, ID informado não corresponde a um item existente");

            return Json(ModelState.ToDataSourceResult());
        }
        #endregion 

        #endregion

        #region Métodos

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var informacaoBancaria = model as InformacaoBancariaModel;

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