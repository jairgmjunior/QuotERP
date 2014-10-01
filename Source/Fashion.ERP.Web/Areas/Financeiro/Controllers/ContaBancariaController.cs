using System;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Financeiro;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.Financeiro.Models;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Financeiro.Controllers
{
    public partial class ContaBancariaController : BaseController
    {
        #region Variaveis
        private readonly IRepository<ContaBancaria> _contaBancariaRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public ContaBancariaController(ILogger logger, IRepository<ContaBancaria> contaBancariaRepository)
        {
            _contaBancariaRepository = contaBancariaRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var contaBancarias = _contaBancariaRepository.Find();

            var list = contaBancarias.Select(p => new GridContaBancariaModel
            {
                Id = p.Id.GetValueOrDefault(),
                Agencia = p.Agencia,
                NomeAgencia = p.NomeAgencia,
                Conta = p.Conta,
                TipoContaBancaria = p.TipoContaBancaria.EnumToString(),
                Gerente = p.Gerente,
                Abertura = p.Abertura,
                Telefone = p.Telefone,
                Banco = p.Banco.Nome
            }).OrderBy(o => o.Agencia).ToList();

            return View(list);
        }
        #endregion

        #region Novo

        public virtual ActionResult Novo()
        {
            return View(new ContaBancariaModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Novo(ContaBancariaModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<ContaBancaria>(model);
                    _contaBancariaRepository.Save(domain);

                    this.AddSuccessMessage("Conta bancária cadastrado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar a conta bancária. Confira se os dados foram informados corretamente.");
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
            var domain = _contaBancariaRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<ContaBancariaModel>(domain);
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a conta bancária.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Editar(ContaBancariaModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _contaBancariaRepository.Get(model.Id));

                    _contaBancariaRepository.Update(domain);

                    this.AddSuccessMessage("Conta bancária atualizado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a conta bancária. Confira se os dados foram informados corretamente.");
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
                    var domain = _contaBancariaRepository.Get(id);
                    _contaBancariaRepository.Delete(domain);

                    this.AddSuccessMessage("Conta bancária excluído com sucesso");

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir a conta bancária: " + exception.Message);
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
            var contaBancaria = model as ContaBancariaModel;

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