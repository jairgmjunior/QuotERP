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
    public partial class CorController : BaseController
    {
		#region Variaveis
        private readonly IRepository<Cor> _corRepository;
        private readonly IRepository<VariacaoModelo> _variacaoModeloRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public CorController(ILogger logger, IRepository<Cor> corRepository,
            IRepository<VariacaoModelo> variacaoModeloRepository)
        {
            _corRepository = corRepository;
            _variacaoModeloRepository = variacaoModeloRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var cors = _corRepository.Find();
            
            var list = cors.Select(p => new GridCorModel
            {
                Id = p.Id.GetValueOrDefault(),
                Nome = p.Nome,
                Ativo = p.Ativo
            }).OrderBy(o => o.Nome).ToList();

            return View(list);
        }
        #endregion

        #region Novo

        public virtual ActionResult Novo()
        {
            return View(new CorModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Novo(CorModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<Cor>(model);
                    domain.Ativo = true;
                    _corRepository.Save(domain);

                    this.AddSuccessMessage("Cor cadastrada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar a cor. Confira se os dados foram informados corretamente: " + exception.Message);
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
            var domain = _corRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<CorModel>(domain);
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a cor.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Editar(CorModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _corRepository.Get(model.Id));

                    _corRepository.Update(domain);

                    this.AddSuccessMessage("Cor atualizada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a cor. Confira se os dados foram informados corretamente: " + exception.Message);
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
					var domain = _corRepository.Get(id);
					_corRepository.Delete(domain);

					this.AddSuccessMessage("Cor excluída com sucesso");
					return RedirectToAction("Index");
				}
				catch (Exception exception)
				{
					ModelState.AddModelError(string.Empty, "Ocorreu um erro ao excluir a cor: " + exception.Message);
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
                var domain = _corRepository.Get(id);

                if (domain != null)
                {
                    var situacao = !domain.Ativo;

                    domain.Ativo = situacao;
                    _corRepository.Update(domain);
                    this.AddSuccessMessage("Cor {0} com sucesso", situacao ? "ativado" : "inativado");
                }
                else
                {
                    this.AddErrorMessage("O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao editar a situação da cor: " + exception.Message);
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
            var cor = model as CorModel;

            // Verificar duplicado
            if (_corRepository.Find(p => p.Nome == cor.Nome && p.Id != cor.Id).Any())
                ModelState.AddModelError("Nome", "Já existe uma cor cadastrada com este nome.");
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
            var domain = _corRepository.Get(id);

            // Verificar se existe uma VariacaoModelo com esta cor.
            if (_variacaoModeloRepository.Find().Any(p => p.Cores.Any(q => q == domain)))
                ModelState.AddModelError("", "Não é possível excluir esta cor, pois existe(m) variação(ões) de modelo associadas a ela.");
        }
        #endregion
        
        #endregion
    }
}