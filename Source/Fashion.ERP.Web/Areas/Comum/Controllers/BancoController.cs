using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.Comum.Models;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Comum.Controllers
{
    public partial class BancoController : BaseController
    {
		#region Variaveis
        private readonly IRepository<Banco> _bancoRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public BancoController(ILogger logger, IRepository<Banco> bancoRepository)
        {
            _bancoRepository = bancoRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Pesquisar Banco

        #region Pesquisar
        [ChildActionOnly, OutputCache(Duration = 3600)]
        public virtual ActionResult Pesquisar()
        {
            PreencheColuna();
            return PartialView();
        }
        #endregion

        #region PesquisarCodigo
        [HttpGet, AjaxOnly]
        public virtual ActionResult PesquisarCodigo(int? codigo)
        {
            if (codigo.HasValue)
            {
                var banco = _bancoRepository.Find(p => p.Codigo == codigo.Value).FirstOrDefault();

                if (banco != null)
                    return Json(new { banco.Id, banco.Codigo, banco.Nome }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { erro = "Nenhum banco encontrado." }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PesquisarId
        [HttpGet, AjaxOnly]
        public virtual ActionResult PesquisarId(long id)
        {
            var banco = _bancoRepository.Get(id);

            if (banco != null)
                return Json(new { banco.Id, banco.Codigo, banco.Nome }, JsonRequestBehavior.AllowGet);

            return Json(new { erro = "Nenhum banco encontrado." }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PesquisarFiltro
        [HttpPost, AjaxOnly]
        public virtual ActionResult PesquisarFiltro(PesquisarModel model)
        {
            var bancos = _bancoRepository.Find(model.Filtrar<Banco>()).ToList();

            var list = bancos.Select(p => new GridBancoModel
            {
                Id = p.Id.GetValueOrDefault(),
                Codigo = p.Codigo,
                Nome = p.Nome,
            }).ToList();

            return Json(list);
        }
        #endregion

        #region PreencheColuna
        private void PreencheColuna()
        {
            var coluna = new[]
                              {
                                  new { value = "Nome", text = "Nome"},
                                  new { value = "Codigo", text = "Código"}
                              };
            ViewData["ColunaPesquisa"] = new SelectList(coluna, "value", "text");
        }
        #endregion

        #endregion

        #endregion
    }
}