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
    public partial class UnidadeMedidaController : BaseController
    {
		#region Variaveis
        private readonly IRepository<UnidadeMedida> _unidadeMedidaRepository;
        private readonly IRepository<Material> _materialRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public UnidadeMedidaController(ILogger logger, IRepository<UnidadeMedida> unidadeMedidaRepository,
            IRepository<Material> materialRepository)
        {
            _unidadeMedidaRepository = unidadeMedidaRepository;
            _materialRepository = materialRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var unidadeMedidas = _unidadeMedidaRepository.Find();

            var list = unidadeMedidas.Select(p => new GridUnidadeMedidaModel
            {
                Id = p.Id.GetValueOrDefault(),
                Sigla = p.Sigla,
                Descricao = p.Descricao,
                FatorMultiplicativo = p.FatorMultiplicativo,
                Ativo = p.Ativo
            }).ToList().OrderBy(o => o.Sigla);

            return View(list);
        }
        #endregion

        #region Novo

        public virtual ActionResult Novo()
        {
            return View(new UnidadeMedidaModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Novo(UnidadeMedidaModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<UnidadeMedida>(model);
                    domain.Ativo = true;

                    _unidadeMedidaRepository.Save(domain);

                    this.AddSuccessMessage("Unidade de medida cadastrada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar a unidade de medida. Confira se os dados foram informados corretamente: " + exception.Message);
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
            var domain = _unidadeMedidaRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<UnidadeMedidaModel>(domain);
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a unidade de medida.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Editar(UnidadeMedidaModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _unidadeMedidaRepository.Get(model.Id));

                    _unidadeMedidaRepository.Update(domain);

                    this.AddSuccessMessage("Unidade de medida atualizada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a unidade de medida. Confira se os dados foram informados corretamente: " + exception.Message);
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
                    var domain = _unidadeMedidaRepository.Load(id);
                    _unidadeMedidaRepository.Delete(domain);

                    this.AddSuccessMessage("Unidade de medida excluída com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir a unidade de medida: " + exception.Message);
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
                var domain = _unidadeMedidaRepository.Get(id);

                if (domain != null)
                {
                    var situacao = !domain.Ativo;

                    domain.Ativo = situacao;
                    _unidadeMedidaRepository.Update(domain);
                    this.AddSuccessMessage("Unidade de medida {0} com sucesso", situacao ? "ativado" : "inativado");
                }
                else
                {
                    this.AddErrorMessage("O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao editar a situação da unidade de medida: " + exception.Message);
                _logger.Info(exception.GetMessage());
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region ConsultaUnidadeMedida
        [AjaxOnly]
        public virtual ActionResult ConsultaUnidadeMedida(long? id /* Id da unidade de medida */)
        {
            try
            {
                var domain = _unidadeMedidaRepository.Get(id);

                if (domain != null)
                {
                    var unidadeMedida =
                        new
                        {
                            Id = domain.Id.GetValueOrDefault(),
                            domain.Sigla,
                            domain.Descricao,
                            domain.Ativo,
                            domain.FatorMultiplicativo
                        };
                    return Json(unidadeMedida, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception exception)
            {
                _logger.Error(exception.GetMessage());
            }

            return Json(new { Error = "Ocorreu um erro ao buscar a unidade de medida." }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion

        #region Métodos

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var unidadeMedida = model as UnidadeMedidaModel;

            // Verificar duplicado
            if (_unidadeMedidaRepository.Find(p => p.Sigla == unidadeMedida.Sigla && p.Id != unidadeMedida.Id).Any())
                ModelState.AddModelError("Sigla", "Já existe uma unidade de medida cadastrada com esta sigla.");
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
            var domain = _unidadeMedidaRepository.Get(id);

            // Verificar se existe um catálogo de material com esta unidade de medida
            if (_materialRepository.Find().Any(p => p.UnidadeMedida == domain))
                ModelState.AddModelError("", "Não é possível excluir esta unidade de medida, pois existe(m) catálogo(s) de material associadas a ela.");
        }
        #endregion

        #endregion
    }
}