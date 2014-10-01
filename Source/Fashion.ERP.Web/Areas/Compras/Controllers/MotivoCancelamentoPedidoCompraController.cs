using System;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Web.Areas.Compras.Models;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Compras.Controllers
{
    public partial class MotivoCancelamentoPedidoCompraController : BaseController
    {
        #region Variaveis
        private readonly IRepository<MotivoCancelamentoPedidoCompra> _motivoCancelamentoPedidoRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public MotivoCancelamentoPedidoCompraController(ILogger logger, IRepository<MotivoCancelamentoPedidoCompra> motivoCancelamentoPedidoRepository)
        {
            _motivoCancelamentoPedidoRepository = motivoCancelamentoPedidoRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var motivos = _motivoCancelamentoPedidoRepository.Find();

            var list = motivos.Select(p => new GridMotivoCancelamentoPedidoCompraModel
            {
                Id = p.Id.GetValueOrDefault(),
                Descricao = p.Descricao,
                Ativo = p.Ativo
            }).OrderBy(o => o.Descricao).ToList();

            return View(list);
        }
        #endregion

        #region Novo

        public virtual ActionResult Novo()
        {
            return View(new MotivoCancelamentoPedidoCompraModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Novo(MotivoCancelamentoPedidoCompraModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<MotivoCancelamentoPedidoCompra>(model);
                    domain.Ativo = true;
                    _motivoCancelamentoPedidoRepository.Save(domain);

                    this.AddSuccessMessage("Motivo de cancelamento do pedido cadastrado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar o motivo de cancelamento do pedido. Confira se os dados foram informados corretamente: " + exception.Message);
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
            var domain = _motivoCancelamentoPedidoRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<MotivoCancelamentoPedidoCompraModel>(domain);
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o motivo de cancelamento do pedido.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Editar(MotivoCancelamentoPedidoCompraModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _motivoCancelamentoPedidoRepository.Get(model.Id));

                    _motivoCancelamentoPedidoRepository.Update(domain);

                    this.AddSuccessMessage("Motivo de cancelamento do pedido atualizado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a motivo de cancelamento do pedido. Confira se os dados foram informados corretamente: " + exception.Message);
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
                    var domain = _motivoCancelamentoPedidoRepository.Get(id);
                    _motivoCancelamentoPedidoRepository.Delete(domain);

                    this.AddSuccessMessage("Motivo de cancelamento do pedido excluído com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir o motivo de cancelamento do pedido: " + exception.Message);
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
                var domain = _motivoCancelamentoPedidoRepository.Get(id);

                if (domain != null)
                {
                    var situacao = !domain.Ativo;

                    domain.Ativo = situacao;
                    _motivoCancelamentoPedidoRepository.Update(domain);
                    this.AddSuccessMessage("Motivo de cancelamento do pedido {0} com sucesso", situacao ? "ativado" : "inativado");
                }
                else
                {
                    this.AddErrorMessage("O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao editar a situação do motivo de cancelamento do pedido: " + exception.Message);
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
            var motivoModel = model as MotivoCancelamentoPedidoCompraModel;

            // Verificar duplicado
            if (_motivoCancelamentoPedidoRepository.Find(p => p.Descricao == motivoModel.Descricao && p.Id != model.Id).Any())
                ModelState.AddModelError("Descrição", "Já existe um motivo de cancelamento de pedido cadastrado com este nome.");
        }
        #endregion

        #endregion
    }
}