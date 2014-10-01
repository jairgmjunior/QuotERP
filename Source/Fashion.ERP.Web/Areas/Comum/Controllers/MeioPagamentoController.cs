using System;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.Financeiro;
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
    public partial class MeioPagamentoController : BaseController
    {
		#region Variaveis
        private readonly IRepository<MeioPagamento> _meioPagamentoRepository;
        private readonly IRepository<RecebimentoChequeRecebido> _recebimentoRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public MeioPagamentoController(ILogger logger, IRepository<MeioPagamento> meioPagamentoRepository,
            IRepository<RecebimentoChequeRecebido> recebimentoRepository)
        {
            _meioPagamentoRepository = meioPagamentoRepository;
            _recebimentoRepository = recebimentoRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var meioPagamentos = _meioPagamentoRepository.Find();

            var list = meioPagamentos.Select(p => new GridMeioPagamentoModel
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
            return View(new MeioPagamentoModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Novo(MeioPagamentoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<MeioPagamento>(model);
                    _meioPagamentoRepository.Save(domain);

                    this.AddSuccessMessage("Meio de pagamento cadastrado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar o meio de pagamento. Confira se os dados foram informados corretamente.");
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
            var domain = _meioPagamentoRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<MeioPagamentoModel>(domain);
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o meio de pagamento.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Editar(MeioPagamentoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _meioPagamentoRepository.Get(model.Id));

                    _meioPagamentoRepository.Update(domain);

                    this.AddSuccessMessage("Meio de pagamento atualizado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o meio de pagamento. Confira se os dados foram informados corretamente.");
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
                    var domain = _meioPagamentoRepository.Get(id);
                    this.AddSuccessMessage("Meio de pagamento excluído com sucesso");

                    _meioPagamentoRepository.Delete(domain);
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir o meio de pagamento: " + exception.Message);
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
            var meioPagamento = model as MeioPagamentoModel;

            // Verificar duplicado
            if (_meioPagamentoRepository.Find(p => p.Descricao == meioPagamento.Descricao && p.Id != meioPagamento.Id).Any())
                ModelState.AddModelError("Descricao", "Já existe meio de pagamento cadastrado com esta descrição.");
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
            var domain = _meioPagamentoRepository.Get(id);

            // Verifica uso
            if (_recebimentoRepository.Find(p => p.MeioPagamento.Id == domain.Id).Any())
                ModelState.AddModelError("", "Não é possível excluir este meio de pagamento pois exitem recebimentos de cheques recebidos cadastrados com ele.");
        }
        #endregion

        #endregion
    }
}