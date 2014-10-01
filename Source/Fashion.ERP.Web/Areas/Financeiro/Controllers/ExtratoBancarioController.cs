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
    public partial class ExtratoBancarioController : BaseController
    {
        #region Variaveis
        private readonly IRepository<ExtratoBancario> _extratoBancarioRepository;
        private readonly IRepository<ContaBancaria> _contaBancariaRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public ExtratoBancarioController(ILogger logger, IRepository<ExtratoBancario> extratoBancarioRepository,
            IRepository<ContaBancaria> contaBancariaRepository)
        {
            _extratoBancarioRepository = extratoBancarioRepository;
            _contaBancariaRepository = contaBancariaRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var extratoBancarios = _extratoBancarioRepository.Find();

            var list = extratoBancarios.Select(p => new GridExtratoBancarioModel
            {
                Id = p.Id.GetValueOrDefault(),
                TipoLancamento = p.TipoLancamento.EnumToString(),
                Emissao = p.Emissao,
                Compensacao = p.Compensacao,
                Descricao = p.Descricao,
                Valor = p.Valor,
                Compensado = p.Compensado,
                ContaBancaria = p.ContaBancaria != null
                    ? string.Format("{0} - {1} - {2}", p.ContaBancaria.Banco.Codigo, p.ContaBancaria.Agencia, p.ContaBancaria.Conta)
                    : string.Empty
            }).OrderBy(o => o.Descricao).ToList();

            return View(list);
        }
        #endregion

        #region Novo

        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo()
        {
            return View(new ExtratoBancarioModel());
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(ExtratoBancarioModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Compensado = model.Compensacao.HasValue;
                    model.Cancelado = false;

                    var domain = Mapper.Unflat<ExtratoBancario>(model);
                    _extratoBancarioRepository.Save(domain);

                    this.AddSuccessMessage("Extrato bancário cadastrado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar o extrato bancário. Confira se os dados foram informados corretamente.");
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
            var domain = _extratoBancarioRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<ExtratoBancarioModel>(domain);
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o extrato bancário.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(ExtratoBancarioModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Compensado = model.Compensacao.HasValue;
                    var domain = Mapper.Unflat(model, _extratoBancarioRepository.Get(model.Id));

                    _extratoBancarioRepository.Update(domain);

                    this.AddSuccessMessage("Extrato bancário atualizado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o extrato bancário. Confira se os dados foram informados corretamente.");
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
                    var domain = _extratoBancarioRepository.Get(id);

                    _extratoBancarioRepository.Delete(domain);
                    this.AddSuccessMessage("Extrato bancário excluído com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir o extrato bancário: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return RedirectToAction("Editar", new { id });
        }
        #endregion

        #endregion

        #region Métodos

        #region PopulateViewData
        protected void PopulateViewData(ExtratoBancarioModel model)
        {
            var contaBancarias = _contaBancariaRepository.Find().ToList()
                .Select(c => new { c.Id, Nome = string.Format("{0} - {1} - {2}", c.Banco.Codigo, c.Agencia, c.Conta) })
                .ToList();

            ViewData["ContaBancaria"] = new SelectList(contaBancarias, "Id", "Nome", model.ContaBancaria);
        }
        #endregion

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var extratoBancario = model as ExtratoBancarioModel;

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