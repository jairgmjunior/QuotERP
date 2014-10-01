using System.Linq;
using System.Net;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.Comum.Models;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Repository;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Comum.Controllers
{
    public partial class EnderecoController : BaseController
    {
        #region Variaveis
        private readonly IRepository<Endereco> _enderecoRepository;
        private readonly IRepository<Cidade> _cidadeRepository;
        private readonly IRepository<UF> _ufRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public EnderecoController(ILogger logger, IRepository<Endereco> enderecoRepository,
            IRepository<UF> ufRepository, IRepository<Cidade> cidadeRepository)
        {
            _logger = logger;
            _enderecoRepository = enderecoRepository;
            _ufRepository = ufRepository;
            _cidadeRepository = cidadeRepository;
        }
        #endregion

		#region Index
        [ChildActionOnly]
        public virtual ActionResult Index(long pessoaId)
        {
            TempData["pessoaId"] = pessoaId;
            return View();
        }

        #endregion

        #region LerEnderecos
        [AjaxOnly, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual ActionResult LerEnderecos([DataSourceRequest] DataSourceRequest request, long pessoaId)
        {
            if (request.PageSize == 0)
                request.PageSize = 4;

            var query = _enderecoRepository.Find(p => p.Pessoa.Id == pessoaId);

            var list = query.Select(p => new GridEnderecoModel
            {
                Id = p.Id.GetValueOrDefault(),
                TipoEndereco = p.TipoEndereco.EnumToString(),
                Logradouro = p.Logradouro,
                Numero = p.Numero,
                Complemento = p.Complemento,
                Bairro = p.Bairro,
                Cep = p.Cep,
                Cidade = p.Cidade.Nome
            });

            var result = list.ToDataSourceResult(request);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Cidades
        [OutputCache(Duration = 60, VaryByParam = "id"), AjaxOnly]
        public virtual JsonResult Cidades(/* Id da UF */long? id)
        {
            if (!id.HasValue)
                return Json(new object(), JsonRequestBehavior.AllowGet);

            var cidades = _cidadeRepository
                .Find(p => p.UF.Id == id)
                .Select(s => new { s.Id, s.Nome }).ToList();
            return Json(cidades, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Novo

        [HttpGet, AjaxOnly, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(long pessoaId)
        {
            var model = new EnderecoModel { Pessoa = pessoaId };
            return View(model);
        }

        [HttpPost, AjaxOnly]
        public virtual ContentResult Novo(EnderecoModel model)
        {
            if (ModelState.IsValid)
            {
                var domain = Mapper.Unflat<Endereco>(model);

                _enderecoRepository.Save(domain);

                return Content("Endereço cadastrado com sucesso!");
            }

            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Content(ModelState.JoinErrors("\r\n"));
        }
        #endregion

        #region Excluir

        [HttpPost, AjaxOnly]
        public virtual JsonResult Excluir([DataSourceRequest] DataSourceRequest request, GridEnderecoModel model)
        {
            var domain = _enderecoRepository.Get(model.Id);

            if (domain != null)
                _enderecoRepository.Delete(domain);
            else
                ModelState.AddModelError(string.Empty, "Não foi possível excluir este endereço, ID informado não corresponde a um item existente");

            return Json(ModelState.ToDataSourceResult());
        }
        #endregion

        #region Métodos

        #region PopulateViewData
        protected void PopulateViewData(EnderecoModel model)
        {
            var ufId = 0L;
            var cidadeId = 0L;
            var cidade = _cidadeRepository.Get(model.Cidade);
            if (cidade != null)
            {
                ufId = cidade.UF.Id.GetValueOrDefault();
                cidadeId = cidade.Id.GetValueOrDefault();
            }
            var ufs = _ufRepository.Find().OrderBy(o => o.Nome).ToList();
            ViewData["Uf"] = ufs.ToSelectList("Nome", ufId);

            var cidades = _cidadeRepository.Find(p => p.UF.Id == ufId).OrderBy(o => o.Nome).ToList();
            ViewData["Cidade"] = cidades.ToSelectList("Nome", cidadeId);
        }
        #endregion

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var endereco = model as EnderecoModel;

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
