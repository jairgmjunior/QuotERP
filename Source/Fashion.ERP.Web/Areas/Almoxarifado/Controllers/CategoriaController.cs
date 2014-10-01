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
    public partial class CategoriaController : BaseController
    {
        #region Variaveis
        private readonly IRepository<Categoria> _categoriaRepository;
        private readonly IRepository<Subcategoria> _subcategoriaRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public CategoriaController(ILogger logger, IRepository<Categoria> categoriaRepository,
            IRepository<Subcategoria> subcategoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
            _subcategoriaRepository = subcategoriaRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var categorias = _categoriaRepository.Find();

            var list = categorias.Select(p => new GridCategoriaModel
            {
                Id = p.Id.GetValueOrDefault(),
                Nome = p.Nome,
                CodigoNcm = p.CodigoNcm,
                TipoCategoria = p.TipoCategoria.EnumToString(),
                GeneroCategoria = p.GeneroCategoria.EnumToString(),
                Ativo = p.Ativo
            }).OrderBy(o => o.Nome).ToList();

            return View(list);
        }
        #endregion

        #region Novo

        public virtual ActionResult Novo()
        {
            return View(new CategoriaModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Novo(CategoriaModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<Categoria>(model);
                    domain.Ativo = true;
                    _categoriaRepository.Save(domain);

                    this.AddSuccessMessage("Categoria cadastrada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar a categoria. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }
        #endregion

        #region Editar

        [ImportModelStateFromTempData]
        public virtual ActionResult Editar(long id)
        {
            var domain = _categoriaRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<CategoriaModel>(domain);
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a categoria.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Editar(CategoriaModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _categoriaRepository.Get(model.Id));

                    _categoriaRepository.Update(domain);

                    this.AddSuccessMessage("Categoria atualizada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a categoria. Confira se os dados foram informados corretamente: " + exception.Message);
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
                    var domain = _categoriaRepository.Get(id);
                    _categoriaRepository.Delete(domain);

                    this.AddSuccessMessage("Categoria excluída com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir a categoria: " + exception.Message);
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
                var domain = _categoriaRepository.Get(id);

                if (domain != null)
                {
                    var situacao = !domain.Ativo;

                    domain.Ativo = situacao;
                    _categoriaRepository.Update(domain);
                    this.AddSuccessMessage("Categoria {0} com sucesso", situacao ? "ativada" : "inativada");
                }
                else
                {
                    this.AddErrorMessage("O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao editar a situação da categoria: " + exception.Message);
                _logger.Info(exception.GetMessage());
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region GeneroCategoria
        [AjaxOnly]
        public virtual ActionResult GeneroCategoria(long? id)
        {
            try
            {
                var domain = _categoriaRepository.Get(id);

                if (domain != null)
                    return Json(new { GeneroCategoria = Enum.GetName(typeof(GeneroCategoria), domain.GeneroCategoria) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                _logger.Error(exception.GetMessage());
            }

            return Json(new { Error = "Ocorreu um erro ao buscar o gênero da categoria." }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region NcmCategoria
        [AjaxOnly]
        public virtual ActionResult NcmCategoria(long? id)
        {
            try
            {
                var domain = _categoriaRepository.Get(id);

                if (domain != null)
                    return Json(new { Ncm = domain.CodigoNcm }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                _logger.Error(exception.GetMessage());
            }

            return Json(new { Error = "Ocorreu um erro ao buscar o código NCM da categoria." }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion

        #region Métodos

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var categoria = model as CategoriaModel;

            // Verificar duplicado
            if (_categoriaRepository.Find(p => p.Nome == categoria.Nome && p.Id != model.Id).Any())
                ModelState.AddModelError("Nome", "Já existe uma categoria cadastrada com este nome.");
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
            var domain = _categoriaRepository.Get(id);

            // Verificar se existe uma subcategoria com esta categoria
            if (_subcategoriaRepository.Find().Any(p => p.Categoria == domain))
                ModelState.AddModelError("", "Não é possível excluir esta categoria, pois existe(m) subcateria(s) associadas a ela.");
        }
        #endregion

        #endregion
    }
}