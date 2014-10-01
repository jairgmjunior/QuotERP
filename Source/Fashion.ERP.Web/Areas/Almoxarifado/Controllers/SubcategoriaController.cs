using System;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.Almoxarifado.Models;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Controllers
{
    public partial class SubcategoriaController : BaseController
    {
		#region Variaveis
        private readonly IRepository<Subcategoria> _subcategoriaRepository;
        private readonly IRepository<Categoria> _categoriaRepository;
        private readonly IRepository<Material> _materialRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public SubcategoriaController(ILogger logger, IRepository<Subcategoria> subcategoriaRepository,
            IRepository<Categoria> categoriaRepository, IRepository<Material> materialRepository)
        {
            _subcategoriaRepository = subcategoriaRepository;
            _categoriaRepository = categoriaRepository;
            _materialRepository = materialRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var subcategorias = _subcategoriaRepository.Find();

            var list = subcategorias.Select(p => new GridSubcategoriaModel
            {
                Id = p.Id.GetValueOrDefault(),
                Nome = p.Nome,
                Categoria = p.Categoria.Nome,
                Ativo = p.Ativo
            }).ToList().OrderBy(o => o.Nome);

            return View(list);
        }
        #endregion

        #region Novo

        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo()
        {
            return View(new SubcategoriaModel());
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(SubcategoriaModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<Subcategoria>(model);
                    domain.Ativo = true;
                    _subcategoriaRepository.Save(domain);

                    this.AddSuccessMessage("Subcategoria cadastrada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar a subcategoria. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }
        #endregion

        #region Editar

        [ImportModelStateFromTempData, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(long id)
        {
            var domain = _subcategoriaRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<SubcategoriaModel>(domain);
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a subcategoria.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(SubcategoriaModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _subcategoriaRepository.Get(model.Id));

                    _subcategoriaRepository.Update(domain);

                    this.AddSuccessMessage("Subcategoria atualizada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a subcategoria. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
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
                    var domain = _subcategoriaRepository.Get(id);
                    _subcategoriaRepository.Delete(domain);

                    this.AddSuccessMessage("Subcategoria excluída com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir a subcategoria: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return RedirectToAction("Editar", new { id });
        }
        #endregion

        #region EditarSituacao
        [HttpPost]
        public virtual ActionResult EditarSituacao(long id)
        {
            try
            {
                var domain = _subcategoriaRepository.Get(id);

                if (domain != null)
                {
                    var situacao = !domain.Ativo;

                    domain.Ativo = situacao;
                    _subcategoriaRepository.Update(domain);
                    this.AddSuccessMessage("Subcategoria {0} com sucesso", situacao ? "ativada" : "inativada");
                }
                else
                {
                    this.AddErrorMessage("O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao editar a situação da subcategoria: " + exception.Message);
                _logger.Info(exception.GetMessage());
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Subcategorias
        /// <summary>
        /// Busca as subcategorias de acordo com o id da categoria.
        /// </summary>
        /// <param name="id">Id da categoria</param>
        /// <returns>Uma lista (Json) de categorias.</returns>
        [OutputCache(Duration = 60, VaryByParam = "id"), AjaxOnly]
        public virtual JsonResult Subcategorias(long id)
        {
            var subcategorias = _subcategoriaRepository
                .Find(p => p.Categoria.Id == id && p.Ativo)
                .Select(s => new { s.Id, s.Nome });
            return Json(subcategorias, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion

        #region Métodos

        #region PopulateViewData
        protected void PopulateViewData(SubcategoriaModel model)
        {
            var categorias = _categoriaRepository.Find(p => p.Ativo).OrderBy(o => o.Nome).ToList();
            ViewData["Categoria"] = categorias.ToSelectList("Nome", model.Categoria);

            model.Populate(_subcategoriaRepository, _categoriaRepository);
        }
        #endregion

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var subcategoria = model as SubcategoriaModel;

            // Verificar duplicado
            if (_subcategoriaRepository.Find(p => p.Nome == subcategoria.Nome && p.Categoria.Id == subcategoria.Categoria && p.Id != subcategoria.Id).Any())
                ModelState.AddModelError("Nome", "Já existe uma subcategoria cadastrada com este nome para esta categoria.");
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
            var domain = _subcategoriaRepository.Get(id);

            // Verificar se existe um catálogo de material com esta subcategoria
            if (_materialRepository.Find().Any(p => p.Subcategoria == domain))
                ModelState.AddModelError("", "Não é possível excluir esta subcategoria, pois existe(m) catálogo(s) de material associadas a ela.");
        }
        #endregion

        #endregion
    }
}