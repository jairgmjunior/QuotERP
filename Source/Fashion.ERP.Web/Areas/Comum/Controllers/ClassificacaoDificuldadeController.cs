using System;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Reporting.Helpers;
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
    public partial class ClassificacaoDificuldadeController : BaseController
    {
		#region Variaveis
        private readonly IRepository<ClassificacaoDificuldade> _classificacaoDificuldadeRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public ClassificacaoDificuldadeController(ILogger logger, IRepository<ClassificacaoDificuldade> classificacaoDificuldadeRepository)
        {
            _classificacaoDificuldadeRepository = classificacaoDificuldadeRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var classificacaoDificuldades = _classificacaoDificuldadeRepository.Find();

            var list = classificacaoDificuldades.Select(p => new GridClassificacaoDificuldadeModel
            {
                Id = p.Id.GetValueOrDefault(),
                Descricao = p.Descricao,
                Criacao = p.Criacao.ToSimNao(),
                Producao = p.Producao.ToSimNao(),
                Ativo = p.Ativo
            }).ToList();

            return View(list);
        }
        #endregion

        #region Novo

		[PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo()
        {
            return View(new ClassificacaoDificuldadeModel());
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(ClassificacaoDificuldadeModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<ClassificacaoDificuldade>(model);
					domain.Ativo = true;
                    _classificacaoDificuldadeRepository.Save(domain);

                    this.AddSuccessMessage("Classificação da dificuldade cadastrada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar a classificação da dificuldade. Confira se os dados foram informados corretamente: " + exception.Message);
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
            var domain = _classificacaoDificuldadeRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<ClassificacaoDificuldadeModel>(domain);
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a classificação da dificuldade.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(ClassificacaoDificuldadeModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _classificacaoDificuldadeRepository.Get(model.Id));

                    _classificacaoDificuldadeRepository.Update(domain);

                    this.AddSuccessMessage("Classificação da dificuldade atualizada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a classificação da dificuldade. Confira se os dados foram informados corretamente: " + exception.Message);
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
					var domain = _classificacaoDificuldadeRepository.Get(id);
					_classificacaoDificuldadeRepository.Delete(domain);

                    this.AddSuccessMessage("Classificação da dificuldade excluída com sucesso");
					return RedirectToAction("Index");
				}
				catch (Exception exception)
				{
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao excluir a classificação da dificuldade: " + exception.Message);
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
                var domain = _classificacaoDificuldadeRepository.Get(id);

                if (domain != null)
                {
                    var situacao = !domain.Ativo;

                    domain.Ativo = situacao;
                    _classificacaoDificuldadeRepository.Update(domain);
                    this.AddSuccessMessage("Classificação da dificuldade {0} com sucesso", situacao ? "ativada" : "inativada");
                }
                else
                {
                    this.AddErrorMessage("O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao editar a situação da classificação da dificuldade: " + exception.Message);
                _logger.Info(exception.GetMessage());
            }

            return RedirectToAction("Index");
        }
        #endregion

        #endregion

		#region Métodos
		
        #region PopulateViewData
        protected void PopulateViewData(ClassificacaoDificuldadeModel model)
        {
        }
        #endregion

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var classificacaoDificuldade = model as ClassificacaoDificuldadeModel;
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