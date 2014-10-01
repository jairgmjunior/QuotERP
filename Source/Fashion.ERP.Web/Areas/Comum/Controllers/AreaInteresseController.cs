using System;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.Comum.Models;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Comum.Controllers
{
    public partial class AreaInteresseController : BaseController
    {
		#region Variaveis
        private readonly IRepository<AreaInteresse> _areaInteresseRepository;
        private readonly IRepository<Cliente> _clienteRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public AreaInteresseController(ILogger logger, IRepository<AreaInteresse> areaInteresseRepository,
            IRepository<Cliente> clienteRepository)
        {
            _areaInteresseRepository = areaInteresseRepository;
            _clienteRepository = clienteRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var areaInteresses = _areaInteresseRepository.Find();

            var list = areaInteresses.Select(p => new GridAreaInteresseModel
            {
                Id = p.Id.GetValueOrDefault(),
                Nome = p.Nome
            }).ToList();

            return View(list);
        }
        #endregion

        #region Novo

        public virtual ActionResult Novo()
        {
            return View(new AreaInteresseModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Novo(AreaInteresseModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<AreaInteresse>(model);
                    _areaInteresseRepository.Save(domain);

                    this.AddSuccessMessage("Área de interesse cadastrada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar a área de interesse. Confira se os dados foram informados corretamente.");
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
            var domain = _areaInteresseRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<AreaInteresseModel>(domain);
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a área de interesse.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Editar(AreaInteresseModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _areaInteresseRepository.Get(model.Id));

                    _areaInteresseRepository.Update(domain);

                    this.AddSuccessMessage("Área de interesse atualizada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a área de interesse. Confira se os dados foram informados corretamente.");
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
                    var domain = _areaInteresseRepository.Get(id);
                    _areaInteresseRepository.Delete(domain);

                    this.AddSuccessMessage("Área de interesse excluída com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir a área de interesse: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return RedirectToAction("Editar", new { id });
        }
        #endregion

        #endregion

        #region Métodos

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var areaInteresse = model as AreaInteresseModel;

            // Verificar duplicado
            if (_areaInteresseRepository.Find(p => p.Nome == areaInteresse.Nome && p.Id != areaInteresse.Id).Any())
                ModelState.AddModelError("Nome", "Já existe área de interesse cadastrada com este nome.");
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
            var domain = _areaInteresseRepository.Get(id);

            // Verifica se existe cliente cadastrado com esta área de interesse
            if (_clienteRepository.Find(p => p.AreaInteresse.Id == domain.Id).Any())
                ModelState.AddModelError("", "Não é possível excluir esta área de interesse pois exitem clientes cadastrados com ela.");
        }
        #endregion

        #endregion
    }
}