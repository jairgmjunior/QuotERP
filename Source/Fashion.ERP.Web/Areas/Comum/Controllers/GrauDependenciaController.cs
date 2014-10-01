using System;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Areas.Comum.Models;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Comum.Controllers
{
    public partial class GrauDependenciaController : BaseController
    {
        #region Variaveis
        private readonly IRepository<GrauDependencia> _grauDependenciaRepository;
        private readonly IRepository<Dependente> _dependenteRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public GrauDependenciaController(ILogger logger, IRepository<GrauDependencia> grauDependenciaRepository,
            IRepository<Dependente> dependenteRepository)
        {
            _grauDependenciaRepository = grauDependenciaRepository;
            _dependenteRepository = dependenteRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var pessoas = _grauDependenciaRepository.Find();

            var list = pessoas.Select(p => new GridGrauDependenciaModel
            {
                Id = p.Id.GetValueOrDefault(),
                Descricao = p.Descricao
            }).ToList();

            return View(list);
        }
        #endregion

        #region Novo

        public virtual ActionResult Novo()
        {
            return View(new GrauDependenciaModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Novo(GrauDependenciaModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<GrauDependencia>(model);
                    _grauDependenciaRepository.Save(domain);

                    this.AddSuccessMessage("Grau de dependência cadastrado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar o grau de dependência. Confira se os dados foram informados corretamente.");
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
            var domain = _grauDependenciaRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<GrauDependenciaModel>(domain);

                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o grau de dependência.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Editar(GrauDependenciaModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _grauDependenciaRepository.Get(model.Id));

                    _grauDependenciaRepository.Update(domain);

                    this.AddSuccessMessage("Grau de dependência atualizado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o grau de dependência. Confira se os dados foram informados corretamente.");
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
                    var domain = _grauDependenciaRepository.Get(id);
                    _grauDependenciaRepository.Delete(domain);

                    this.AddSuccessMessage("Grau de dependência excluído com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("",
                                             "Ocorreu um erro ao excluir o grau de dependência: " + exception.Message);
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
            var grauDependencia = model as GrauDependenciaModel;

            // Verificar duplicado
            if (_grauDependenciaRepository.Find(p => p.Descricao == grauDependencia.Descricao && p.Id != grauDependencia.Id).Any())
                ModelState.AddModelError("Descricao", "Já existe grau de dependência cadastrado com esta descrição.");
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
            var domain = _grauDependenciaRepository.Get(id);

            // Verifica uso
            if (_dependenteRepository.Find(p => p.GrauDependencia.Id == domain.Id).Any())
                ModelState.AddModelError("", "Não é possível excluir este grau de dependência pois exitem dependentes cadastrados com ele.");
        }
        #endregion

        #endregion
    }
}