using System;
using System.Collections.Generic;
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
using Fashion.Framework.UnitOfWork.DinamicFilter;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Comum.Controllers
{
    public partial class CentroCustoController : BaseController
    {
        #region Variaveis
        private readonly IRepository<CentroCusto> _centroCustoRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public CentroCustoController(ILogger logger, IRepository<CentroCusto> centroCustoRepository)
        {
            _centroCustoRepository = centroCustoRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var centroCustos = _centroCustoRepository.Find();

            var list = centroCustos.Select(p => new GridCentroCustoModel
            {
                Id = p.Id.GetValueOrDefault(),
                Nome = p.Nome,
                Codigo = p.Codigo,
                Ativo = p.Ativo
            }).ToList();

            return View(list);
        }
        #endregion

        #region Novo

        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo()
        {
            return View(new CentroCustoModel());
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(CentroCustoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<CentroCusto>(model);

                    var proximoCodigo = _centroCustoRepository.Find().Any()
                                            ? _centroCustoRepository.Find().Max(p => p.Codigo) + 1
                                            : 1;
                    domain.Codigo = proximoCodigo;
                    domain.Ativo = true;
                    _centroCustoRepository.Save(domain);

                    this.AddSuccessMessage("Centro de custo cadastrado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar o centro de custo. Confira se os dados foram informados corretamente: " + exception.Message);
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
            var domain = _centroCustoRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<CentroCustoModel>(domain);
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o centro de custo.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(CentroCustoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _centroCustoRepository.Get(model.Id));

                    _centroCustoRepository.Update(domain);

                    this.AddSuccessMessage("Centro de custo atualizado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o centro de custo. Confira se os dados foram informados corretamente: " + exception.Message);
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
                    var domain = _centroCustoRepository.Get(id);
                    _centroCustoRepository.Delete(domain);

                    this.AddSuccessMessage("Centro de custo excluído com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao excluir o centro de custo: " + exception.Message);
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
                var domain = _centroCustoRepository.Get(id);

                if (domain != null)
                {
                    var situacao = !domain.Ativo;

                    domain.Ativo = situacao;
                    _centroCustoRepository.Update(domain);
                    this.AddSuccessMessage("Centro de custo {0} com sucesso", situacao ? "ativado" : "inativado");
                }
                else
                {
                    this.AddErrorMessage("O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao editar a situação do centro de custo: " + exception.Message);
                _logger.Info(exception.GetMessage());
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Pesquisar CentroCusto

        #region Pesquisar
        [ChildActionOnly, OutputCache(Duration = 3600)]
        public virtual ActionResult Pesquisar()
        {
            PreencheColuna();
            return PartialView();
        }
        #endregion

        #region Pesquisar
        [HttpPost, AjaxOnly]
        public virtual ActionResult PesquisarFiltro(PesquisarModel model)
        {
            var filters = new List<FilterExpression>
            {
                // Ativo
                new FilterExpression("Ativo", ComparisonOperator.IsEqual, true, LogicOperator.And),
                // Filtro da tela
                model.Filtrar<CentroCusto>()
            };

            var materiais = _centroCustoRepository.Find(filters.ToArray()).ToList();

            var list = materiais.Select(p => new GridCentroCustoModel
            {
                Id = p.Id.GetValueOrDefault(),
                Codigo = p.Codigo,
                Nome = p.Nome
            }).ToList();

            return Json(list);
        }
        #endregion

        #region PesquisarCodigo
        [HttpGet, AjaxOnly]
        public virtual ActionResult PesquisarCodigo(long? codigo)
        {
            if (codigo.HasValue)
            {
                var centroCusto = _centroCustoRepository.Find(p => p.Codigo == codigo && p.Ativo).FirstOrDefault();

                if (centroCusto != null)
                    return Json(new { centroCusto.Id, centroCusto.Codigo, centroCusto.Nome }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { erro = "Nenhum centro de custo encontrado." }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PesquisarId
        [HttpGet, AjaxOnly]
        public virtual ActionResult PesquisarId(long id)
        {
            var centroCusto = _centroCustoRepository.Get(id);

            if (centroCusto != null)
                return Json(new { centroCusto.Id, centroCusto.Codigo, centroCusto.Nome }, JsonRequestBehavior.AllowGet);

            return Json(new { erro = "Nenhum centro de custo encontrado." }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PreencheColuna
        private void PreencheColuna()
        {
            var coluna = new[]
                              {
                                  new { value = "Nome", text = "Nome"},
                                  new { value = "Codigo", text = "Código"},
                              };
            ViewData["ColunaPesquisa"] = new SelectList(coluna, "value", "text");
        }
        #endregion

        #endregion

        #endregion

        #region Métodos

        #region PopulateViewData
        protected void PopulateViewData(CentroCustoModel model)
        {
        }
        #endregion

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var centroCusto = model as CentroCustoModel;
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