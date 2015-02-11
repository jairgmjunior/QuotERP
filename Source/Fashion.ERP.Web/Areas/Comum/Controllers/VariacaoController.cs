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
    public partial class VariacaoController : BaseController
    {
		#region Variaveis
        private readonly IRepository<Variacao> _variacaoRepository;
        private readonly IRepository<VariacaoModelo> _variacaoModeloRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public VariacaoController(ILogger logger, IRepository<Variacao> variacaoRepository,
            IRepository<VariacaoModelo> variacaoModeloRepository)
        {
            _variacaoRepository = variacaoRepository;
            _variacaoModeloRepository = variacaoModeloRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var variacoes = _variacaoRepository.Find();
            
            var list = variacoes.Select(p => new GridVariacaoModel()
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
            return View(new VariacaoModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Novo(VariacaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<Variacao>(model);
                    domain.Ativo = true;
                    _variacaoRepository.Save(domain);

                    this.AddSuccessMessage("Variação cadastrada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar a variação. Confira se os dados foram informados corretamente: " + exception.Message);
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
            var domain = _variacaoRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<VariacaoModel>(domain);
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a variação.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Editar(VariacaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _variacaoRepository.Get(model.Id));

                    _variacaoRepository.Update(domain);

                    this.AddSuccessMessage("Variação atualizada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a variação. Confira se os dados foram informados corretamente: " + exception.Message);
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
					var domain = _variacaoRepository.Get(id);
                    _variacaoRepository.Delete(domain);

					this.AddSuccessMessage("Variação excluída com sucesso");
					return RedirectToAction("Index");
				}
				catch (Exception exception)
				{
					ModelState.AddModelError(string.Empty, "Ocorreu um erro ao excluir a variação: " + exception.Message);
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
                var domain = _variacaoRepository.Get(id);

                if (domain != null)
                {
                    var situacao = !domain.Ativo;

                    domain.Ativo = situacao;
                    _variacaoRepository.Update(domain);
                    this.AddSuccessMessage("Variação {0} com sucesso", situacao ? "ativado" : "inativado");
                }
                else
                {
                    this.AddErrorMessage("O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao editar a situação da variação: " + exception.Message);
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
            var variacao = model as VariacaoModel;

            // Verificar duplicado
            if (_variacaoRepository.Find(p => p.Nome == variacao.Nome && p.Id != variacao.Id).Any())
                ModelState.AddModelError("Nome", "Já existe uma variação cadastrada com este nome.");
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
            var domain = _variacaoRepository.Get(id);
            
            if (_variacaoModeloRepository.Find().Any(p => p.Variacao.Id == domain.Id))
                ModelState.AddModelError("", "Não é possível excluir esta variação, pois existe(m) variação(ões) de modelo associadas a ela.");
        }
        #endregion

        #endregion
    }
}