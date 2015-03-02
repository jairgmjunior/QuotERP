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
    public partial class FamiliaController : BaseController
    {
		#region Variaveis
        private readonly IRepository<Familia> _familiaRepository;
        private readonly IRepository<Material> _materialRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public FamiliaController(ILogger logger, IRepository<Familia> familiaRepository,
            IRepository<Material> materialRepository)
        {
            _familiaRepository = familiaRepository;
            _materialRepository = materialRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var familias = _familiaRepository.Find();

            var list = familias.Select(p => new GridFamiliaModel
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
            return View(new FamiliaModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Novo(FamiliaModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<Familia>(model);
                    domain.Ativo = true;
                    _familiaRepository.Save(domain);

                    this.AddSuccessMessage("Família cadastrada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar a família. Confira se os dados foram informados corretamente: " + exception.Message);
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
            var domain = _familiaRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<FamiliaModel>(domain);
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a família.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Editar(FamiliaModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _familiaRepository.Get(model.Id));

                    _familiaRepository.Update(domain);

                    this.AddSuccessMessage("Família atualizada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a família. Confira se os dados foram informados corretamente: " + exception.Message);
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
                    var domain = _familiaRepository.Get(id);
                    _familiaRepository.Delete(domain);

                    this.AddSuccessMessage("Família excluída com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir a família: " + exception.Message);
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
                var domain = _familiaRepository.Get(id);

                if (domain != null)
                {
                    var situacao = !domain.Ativo;

                    domain.Ativo = situacao;
                    _familiaRepository.Update(domain);
                    this.AddSuccessMessage("Família {0} com sucesso", situacao ? "ativado" : "inativado");
                }
                else
                {
                    this.AddErrorMessage("O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao editar a situação da família: " + exception.Message);
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
            var familia = model as FamiliaModel;

            // Verificar duplicado
            if (_familiaRepository.Find(p => p.Nome == familia.Nome && p.Id != familia.Id).Any())
                ModelState.AddModelError("Nome", "Já existe uma família cadastrada com este nome.");
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
            var domain = _familiaRepository.Get(id);

            // Verificar se existe um material com esta família
            if (_materialRepository.Find().Any(p => p.Familia == domain))
                ModelState.AddModelError("", "Não é possível excluir esta família, pois existe(m) material(s) associados a ela.");
        }
        #endregion

        #endregion
    }
}