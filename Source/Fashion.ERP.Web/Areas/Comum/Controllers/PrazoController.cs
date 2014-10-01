using System;
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
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Comum.Controllers
{
    public partial class PrazoController : BaseController
    {
		#region Variaveis
        private readonly IRepository<Prazo> _prazoRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public PrazoController(ILogger logger, IRepository<Prazo> prazoRepository)
        {
            _prazoRepository = prazoRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var prazos = _prazoRepository.Find();

            var list = prazos.Select(p => new GridPrazoModel
            {
                Id = p.Id.GetValueOrDefault(),
                Descricao = p.Descricao,
                Ativo = p.Ativo
            }).ToList();

            return View(list);
        }
        #endregion

        #region Novo

		[PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo()
        {
            return View(new PrazoModel());
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(PrazoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<Prazo>(model);
					domain.Ativo = true;
                    _prazoRepository.Save(domain);

                    this.AddSuccessMessage("PrazoDescricao cadastrado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar o PrazoDescricao. Confira se os dados foram informados corretamente: " + exception.Message);
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
            var domain = _prazoRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<PrazoModel>(domain);
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o PrazoDescricao.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(PrazoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _prazoRepository.Get(model.Id));

                    _prazoRepository.Update(domain);

                    this.AddSuccessMessage("PrazoDescricao atualizado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o PrazoDescricao. Confira se os dados foram informados corretamente: " + exception.Message);
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
					var domain = _prazoRepository.Get(id);
					_prazoRepository.Delete(domain);

					this.AddSuccessMessage("PrazoDescricao excluído com sucesso");
					return RedirectToAction("Index");
				}
				catch (Exception exception)
				{
					ModelState.AddModelError(string.Empty, "Ocorreu um erro ao excluir o PrazoDescricao: " + exception.Message);
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
                var domain = _prazoRepository.Get(id);

                if (domain != null)
                {
                    var situacao = !domain.Ativo;

                    domain.Ativo = situacao;
                    _prazoRepository.Update(domain);
                    this.AddSuccessMessage("PrazoDescricao {0} com sucesso", situacao ? "ativado" : "inativado");
                }
                else
                {
                    this.AddErrorMessage("O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao editar a situação do prazo: " + exception.Message);
                _logger.Info(exception.GetMessage());
            }

            return RedirectToAction("Index");
        }
        #endregion

        #endregion

		#region Métodos
		
        #region PopulateViewData
        protected void PopulateViewData(PrazoModel model)
        {
        }
        #endregion

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var prazo = (PrazoModel)model;

            // Padrão
            if (prazo.Padrao.HasValue && prazo.Padrao.Value && _prazoRepository.Find().Any(p => p.Padrao))
                ModelState.AddModelError("Padrao", "Já existe um prazo padrão.");
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