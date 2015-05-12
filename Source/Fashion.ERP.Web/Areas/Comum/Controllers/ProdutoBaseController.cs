using System;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.EngenhariaProduto;
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
    public partial class ProdutoBaseController : BaseController
    {
		#region Variaveis
        private readonly IRepository<ProdutoBase> _produtoBaseRepository;
        private readonly IRepository<Modelo> _modeloRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public ProdutoBaseController(ILogger logger, IRepository<ProdutoBase> produtoBaseRepository,
            IRepository<Modelo> modeloRepository)
        {
            _produtoBaseRepository = produtoBaseRepository;
            _modeloRepository = modeloRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var produtoBases = _produtoBaseRepository.Find();

            var list = produtoBases.Select(p => new GridProdutoBaseModel
            {
                Id = p.Id.GetValueOrDefault(),
                Descricao = p.Descricao,
                Ativo = p.Ativo
            }).ToList().OrderBy(o => o.Descricao);

            return View(list);
        }
        #endregion

        #region Novo

        public virtual ActionResult Novo()
        {
            return View(new ProdutoBaseModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Novo(ProdutoBaseModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<ProdutoBase>(model);
                    domain.Ativo = true;
                    _produtoBaseRepository.Save(domain);

                    this.AddSuccessMessage("Produto base cadastrado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar o produto base. Confira se os dados foram informados corretamente: " + exception.Message);
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
            var domain = _produtoBaseRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<ProdutoBaseModel>(domain);
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o produto base.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Editar(ProdutoBaseModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _produtoBaseRepository.Get(model.Id));

                    _produtoBaseRepository.Update(domain);

                    this.AddSuccessMessage("Produto base atualizado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o produto base. Confira se os dados foram informados corretamente: " + exception.Message);
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
                    var domain = _produtoBaseRepository.Get(id);
                    _produtoBaseRepository.Delete(domain);

                    this.AddSuccessMessage("Produto base excluído com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir o produto base: " + exception.Message);
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
                var domain = _produtoBaseRepository.Get(id);

                if (domain != null)
                {
                    var situacao = !domain.Ativo;

                    domain.Ativo = situacao;
                    _produtoBaseRepository.Update(domain);
                    this.AddSuccessMessage("Produto base {0} com sucesso", situacao ? "ativado" : "inativado");
                }
                else
                {
                    this.AddErrorMessage("O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao editar a situação do produto base: " + exception.Message);
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
            var produtoBase = model as ProdutoBaseModel;

            // Verificar duplicado
            if (_produtoBaseRepository.Find(p => p.Descricao == produtoBase.Descricao && p.Id != produtoBase.Id).Any())
                ModelState.AddModelError("Descricao", "Já existe um produto base cadastrado com esta descrição.");
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
            var domain = _produtoBaseRepository.Get(id);

            // Verificar relacionamento
            if (_modeloRepository.Find().Any(p => p.ProdutoBase == domain))
                ModelState.AddModelError("", "Não é possível excluir este produto base, pois existe(m) modelo(s) associadas a ele.");
        }
        #endregion

        public virtual JsonResult ObtenhaLista()
        {
            var produtosBase = _produtoBaseRepository
                .Find(x => x.Ativo)
                .Select(s => new { s.Id, s.Descricao }).OrderBy(o => o.Descricao).ToList();

            return Json(produtosBase, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}