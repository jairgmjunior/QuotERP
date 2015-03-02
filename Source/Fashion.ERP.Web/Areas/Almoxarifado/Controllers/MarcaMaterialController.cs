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
    public partial class MarcaMaterialController : BaseController
    {
		#region Variaveis
        private readonly IRepository<MarcaMaterial> _marcaMaterialRepository;
        private readonly IRepository<Material> _materialRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public MarcaMaterialController(ILogger logger, IRepository<MarcaMaterial> marcaMaterialRepository,
            IRepository<Material> materialRepository)
        {
            _marcaMaterialRepository = marcaMaterialRepository;
            _materialRepository = materialRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var marcaMaterials = _marcaMaterialRepository.Find();

            var list = marcaMaterials.Select(p => new GridMarcaMaterialModel
            {
                Id = p.Id.GetValueOrDefault(),
                Nome = p.Nome,
                Ativo = p.Ativo
            }).OrderBy(o => o.Nome).ToList();

            return View(list);
        }
        #endregion

        #region Novo

        public virtual ActionResult Novo()
        {
            return View(new MarcaMaterialModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Novo(MarcaMaterialModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<MarcaMaterial>(model);
                    domain.Ativo = true;
                    _marcaMaterialRepository.Save(domain);

                    this.AddSuccessMessage("Marca do material cadastrada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar a marca do material. Confira se os dados foram informados corretamente: " + exception.Message);
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
            var domain = _marcaMaterialRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<MarcaMaterialModel>(domain);
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a marca do material.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Editar(MarcaMaterialModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _marcaMaterialRepository.Get(model.Id));

                    _marcaMaterialRepository.Update(domain);

                    this.AddSuccessMessage("Marca do material atualizada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a marca do material. Confira se os dados foram informados corretamente: " + exception.Message);
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
                    var domain = _marcaMaterialRepository.Get(id);
                    _marcaMaterialRepository.Delete(domain);

                    this.AddSuccessMessage("Marca do material excluída com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir a marca do material: " + exception.Message);
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
                var domain = _marcaMaterialRepository.Get(id);

                if (domain != null)
                {
                    var situacao = !domain.Ativo;

                    domain.Ativo = situacao;
                    _marcaMaterialRepository.Update(domain);
                    this.AddSuccessMessage("Marca do material {0} com sucesso", situacao ? "ativada" : "inativada");
                }
                else
                {
                    this.AddErrorMessage("O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao editar a situação da marca do material: " + exception.Message);
                _logger.Info(exception.GetMessage());
            }

            return RedirectToAction("Index");
        }
        #endregion

        #endregion

        #region Métodos

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var marcaMaterial = model as MarcaMaterialModel;

            // Verificar duplicado
            if (_marcaMaterialRepository.Find(p => p.Nome == marcaMaterial.Nome && p.Id != marcaMaterial.Id).Any())
                ModelState.AddModelError("Nome", "Já existe uma marca do material cadastrada com este nome.");
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
            var domain = _marcaMaterialRepository.Get(id);

            // Verificar se existe um material com esta subcategoria
            if (_materialRepository.Find().Any(p => p.MarcaMaterial == domain))
                ModelState.AddModelError("", "Não é possível excluir esta marca do material, pois existe(m) material(s) associados a ela.");
        }
        #endregion

        #endregion
    }
}