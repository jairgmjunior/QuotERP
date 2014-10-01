using System;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;
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
    public partial class DepositoMaterialController : BaseController
    {
		#region Variaveis
        private readonly IRepository<DepositoMaterial> _depositoMaterialRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public DepositoMaterialController(ILogger logger, IRepository<DepositoMaterial> depositoMaterialRepository,
            IRepository<Pessoa> pessoaRepository)
        {
            _depositoMaterialRepository = depositoMaterialRepository;
            _pessoaRepository = pessoaRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var depositoMaterials = _depositoMaterialRepository.Find();

            var list = depositoMaterials.Select(p => new GridDepositoMaterialModel
            {
                Id = p.Id.GetValueOrDefault(),
                Nome = p.Nome,
                DataAbertura = p.DataAbertura,
                Unidade = p.Unidade.Nome,
                Ativo = p.Ativo
            }).ToList();

            return View(list);
        }
        #endregion

        #region Novo

        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo()
        {
            return View(new DepositoMaterialModel());
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(DepositoMaterialModel model)
        {
            if (model.Funcionarios.IsNullOrEmpty())
                ModelState.AddModelError(string.Empty, "Adicione pelo menos um funcionário ao depósito.");

            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<DepositoMaterial>(model);

                    foreach (var funcionario in model.Funcionarios)
                        domain.AddFuncionario(_pessoaRepository.Load(funcionario));
                    
                    domain.Ativo = true;
                    _depositoMaterialRepository.Save(domain);

                    this.AddSuccessMessage("Depósito de material cadastrado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar o depósito de material. Confira se os dados foram informados corretamente: " + exception.Message);
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
            var domain = _depositoMaterialRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<DepositoMaterialModel>(domain);
                model.Funcionarios = domain.Funcionarios.OrderBy(f => f.Nome).Select(p => p.Id ?? 0).ToList();

                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o depósito de material.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(DepositoMaterialModel model)
        {
            if (model.Funcionarios.IsNullOrEmpty())
                ModelState.AddModelError(string.Empty, "Adicione pelo menos um funcionário ao depósito.");

            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _depositoMaterialRepository.Get(model.Id));

                    domain.ClearFuncionario();
                    foreach (var funcionario in model.Funcionarios)
                        domain.AddFuncionario(_pessoaRepository.Load(funcionario));

                    _depositoMaterialRepository.Update(domain);

                    this.AddSuccessMessage("Depósito de material atualizado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o depósito de material. Confira se os dados foram informados corretamente: " + exception.Message);
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
					var domain = _depositoMaterialRepository.Get(id);
					_depositoMaterialRepository.Delete(domain);

					this.AddSuccessMessage("Depósito de material excluído com sucesso");
					return RedirectToAction("Index");
				}
				catch (Exception exception)
				{
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao excluir o depósito de material: " + exception.Message);
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
                var domain = _depositoMaterialRepository.Get(id);

                if (domain != null)
                {
                    var situacao = !domain.Ativo;

                    domain.Ativo = situacao;
                    _depositoMaterialRepository.Update(domain);
                    this.AddSuccessMessage("Depósito de material {0} com sucesso", situacao ? "ativado" : "inativado");
                }
                else
                {
                    this.AddErrorMessage("O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao editar a situação do depósito de material: " + exception.Message);
                _logger.Info(exception.GetMessage());
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region DepositosPorUnidade
        /// <summary>
        /// Busca as subcategorias de acordo com o id da categoria.
        /// </summary>
        /// <param name="id">Id da categoria</param>
        /// <returns>Uma lista (Json) de categorias.</returns>
        [OutputCache(Duration = 60, VaryByParam = "id"), AjaxOnly]
        public virtual JsonResult DepositosPorUnidade(long id /* Id da unidade estocadora */)
        {
            var depositoMateriais = _depositoMaterialRepository
                .Find(p => p.Unidade.Id == id)
                .Select(s => new { s.Id, s.Nome });
            return Json(depositoMateriais, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion

		#region Métodos

        #region PopulateViewData
        protected void PopulateViewData(DepositoMaterialModel model)
        {
            // Unidades
            var unidades = _pessoaRepository.Find(p => p.Unidade != null && p.Unidade.Ativo).OrderBy(p => p.Nome).ToList();
            ViewData["Unidade"] = unidades.ToSelectList("Nome", model.Unidade);

            // Funcionários
            var funcionarios = _pessoaRepository.Find(p => p.Funcionario != null).OrderBy(p => p.Nome).ToList();

            var funcionariosAtivo = funcionarios.Where(p => p.Funcionario.Ativo).ToList();
            ViewData["Funcionario"] = funcionariosAtivo.ToSelectList("Nome");

            ViewBag.FuncionariosDicionario = funcionarios.ToDictionary(t => t.Id, t => t.Nome);

            model.Populate(_pessoaRepository, _depositoMaterialRepository);
        }
        #endregion

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var depositoMaterial = model as DepositoMaterialModel;
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