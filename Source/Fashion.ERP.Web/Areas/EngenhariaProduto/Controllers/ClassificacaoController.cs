using System;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.EngenhariaProduto.Models;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers
{
    public partial class ClassificacaoController : BaseController
    {
		#region Variaveis
        private readonly IRepository<Classificacao> _classificacaoRepository;
        private readonly IRepository<Modelo> _modeloRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public ClassificacaoController(ILogger logger, IRepository<Classificacao> classificacaoRepository,
            IRepository<Modelo> modeloRepository)
        {
            _classificacaoRepository = classificacaoRepository;
            _modeloRepository = modeloRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var classificacaos = _classificacaoRepository.Find();

            var list = classificacaos.Select(p => new GridClassificacaoModel
            {
                Id = p.Id.GetValueOrDefault(),
                Descricao = p.Descricao,
                Ativo = p.Ativo
            }).ToList().OrderBy(o => o.Descricao);

            return View(list);
        }
        #endregion

        #region Novo

        public virtual ActionResult Novo()
        {
            return View(new ClassificacaoModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Novo(ClassificacaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<Classificacao>(model);
                    domain.Ativo = true;
                    _classificacaoRepository.Save(domain);

                    this.AddSuccessMessage("Classificação cadastrada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar a classificação. Confira se os dados foram informados corretamente: " + exception.Message);
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
            var domain = _classificacaoRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<ClassificacaoModel>(domain);
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a classificação.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Editar(ClassificacaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _classificacaoRepository.Get(model.Id));

                    _classificacaoRepository.Update(domain);

                    this.AddSuccessMessage("Classificação atualizada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a classificação. Confira se os dados foram informados corretamente: " + exception.Message);
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
                    var domain = _classificacaoRepository.Get(id);
                    _classificacaoRepository.Delete(domain);

                    this.AddSuccessMessage("Classificação excluída com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir a classificação: " + exception.Message);
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
                var domain = _classificacaoRepository.Get(id);

                if (domain != null)
                {
                    var situacao = !domain.Ativo;

                    domain.Ativo = situacao;
                    _classificacaoRepository.Update(domain);
                    this.AddSuccessMessage("Classificação {0} com sucesso", situacao ? "ativada" : "inativada");
                }
                else
                {
                    this.AddErrorMessage("O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao editar a situação da classificação: " + exception.Message);
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
            var classificacao = model as ClassificacaoModel;

            // Verificar duplicado
            if (_classificacaoRepository.Find(p => p.Descricao == classificacao.Descricao && p.Id != classificacao.Id).Any())
                ModelState.AddModelError("Descricao", "Já existe uma classificação cadastrada com esta descrição.");
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
            var domain = _classificacaoRepository.Get(id);

            // Verificar relacionamento
            if (_modeloRepository.Find().Any(p => p.Classificacao == domain))
                ModelState.AddModelError("", "Não é possível excluir esta classificação, pois existe(m) modelo(s) associadas a ela.");
        }
        #endregion

        #endregion
    }
}