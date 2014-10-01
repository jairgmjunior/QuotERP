using System;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.EngenhariaProduto.Models;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers
{
    public partial class BarraController : BaseController
    {
		#region Variaveis
        private readonly IRepository<Barra> _barraRepository;
        private readonly IRepository<Modelo> _modeloRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public BarraController(ILogger logger, IRepository<Barra> barraRepository,
            IRepository<Modelo> modeloRepository)
        {
            _barraRepository = barraRepository;
            _modeloRepository = modeloRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var tipoBarras = _barraRepository.Find();

            var list = tipoBarras.Select(p => new GridBarraModel
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
            return View(new BarraModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Novo(BarraModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<Barra>(model);
                    domain.Ativo = true;
                    _barraRepository.Save(domain);

                    this.AddSuccessMessage("Barra cadastrada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar a barra. Confira se os dados foram informados corretamente: " + exception.Message);
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
            var domain = _barraRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<BarraModel>(domain);
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a barra.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Editar(BarraModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _barraRepository.Get(model.Id));

                    _barraRepository.Update(domain);

                    this.AddSuccessMessage("Barra atualizada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a barra. Confira se os dados foram informados corretamente: " + exception.Message);
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
                    var domain = _barraRepository.Get(id);
                    _barraRepository.Delete(domain);

                    this.AddSuccessMessage("Barra excluída com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir a barra: " + exception.Message);
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
                var domain = _barraRepository.Get(id);

                if (domain != null)
                {
                    var situacao = !domain.Ativo;

                    domain.Ativo = situacao;
                    _barraRepository.Update(domain);
                    this.AddSuccessMessage("Tipo de barra {0} com sucesso", situacao ? "ativado" : "inativado");
                }
                else
                {
                    this.AddErrorMessage("O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao editar a situação do tipo de barra: " + exception.Message);
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
            var barra = model as BarraModel;

            // Verificar duplicado
            if (_barraRepository.Find(p => p.Descricao == barra.Descricao && p.Id != barra.Id).Any())
                ModelState.AddModelError("Descricao", "Já existe um tipo de barra cadastrado com esta descrição.");
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
            var domain = _barraRepository.Get(id);

            // Verificar relacionamento
            if (_modeloRepository.Find().Any(p => p.Barra == domain))
                ModelState.AddModelError("", "Não é possível excluir esta barra, pois existe(m) modelo(s) associadas a ela.");
        }
        #endregion

        #endregion
    }
}