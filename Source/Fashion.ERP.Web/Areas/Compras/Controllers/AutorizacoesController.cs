using System;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.Compras.Models;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Compras.Controllers
{
    public partial class AutorizacoesController : BaseController
    {
		#region Variaveis
        private readonly IRepository<ProcedimentoModuloCompras> _procedimentoModuloComprasRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public AutorizacoesController(ILogger logger, IRepository<ProcedimentoModuloCompras> procedimentoModuloComprasRepository,
            IRepository<Pessoa> pessoaRepository)
        {
            _procedimentoModuloComprasRepository = procedimentoModuloComprasRepository;
            _pessoaRepository = pessoaRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var procedimentoModuloComprass = _procedimentoModuloComprasRepository.Find();

            var list = procedimentoModuloComprass.Select(p => new GridAutorizacoesModel
            {
                Id = p.Id.GetValueOrDefault(),
                Procedimento = p.Descricao
            }).ToList();

            return View(list);
        }
        #endregion

        #region Autorizar

        [ImportModelStateFromTempData, PopulateViewData("PopulateAutorizar")]
        public virtual ActionResult Autorizar(long id)
        {
            var domain = _procedimentoModuloComprasRepository.Get(id);

            if (domain != null)
            {
                var model = new AutorizacoesModel {Procedimento = domain.Descricao};

                foreach (var item in domain.Funcionarios)
                    model.Funcionarios.Add(item.Id);

                return View("Autorizar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a autorização.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateAutorizar")]
        public virtual ActionResult Autorizar(AutorizacoesModel model)
        {
            if (model.Funcionarios.IsNullOrEmpty())
                ModelState.AddModelError("", "Adicione pelo menos um funcionário.");

            if (ModelState.IsValid)
            {
                try
                {
                    var domain = _procedimentoModuloComprasRepository.Get(model.Id);

                    domain.ClearFuncionario();
                    for (int i = 0; i < model.Funcionarios.Count; i++)
                    {
                        var idx = i;

                        domain.AddFuncionario(_pessoaRepository.Get(model.Funcionarios[idx]));
                    }

                    _procedimentoModuloComprasRepository.Update(domain);

                    this.AddSuccessMessage("Autorização atualizada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a autorização. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }
        #endregion

        #endregion

		#region Métodos

        #region PopulateAutorizar
        protected void PopulateAutorizar(AutorizacoesModel model)
        {
            // Funcionários
            var funcionarios = _pessoaRepository.Find(p => p.Funcionario != null && p.Funcionario.Ativo)
                .Select(s => new { s.Id, s.Nome }).ToList();
            ViewBag.FuncionariosDicionario = funcionarios.ToDictionary(t => t.Id, t => t.Nome);
        }
        #endregion

        #endregion
    }
}