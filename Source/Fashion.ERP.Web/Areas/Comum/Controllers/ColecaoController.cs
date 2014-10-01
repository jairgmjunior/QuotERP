using System;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.ERP.Web.Areas.Comum.Models;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Comum.Controllers
{
    public partial class ColecaoController : BaseController
    {
		#region Variaveis
        private readonly IRepository<Colecao> _colecaoRepository;
        private readonly IRepository<Modelo> _modeloRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public ColecaoController(ILogger logger, IRepository<Colecao> colecaoRepository,
            IRepository<Modelo> modeloRepository)
        {
            _colecaoRepository = colecaoRepository;
            _modeloRepository = modeloRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var colecaos = _colecaoRepository.Find();

            var list = colecaos.Select(p => new GridColecaoModel
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
            return View(new ColecaoModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Novo(ColecaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<Colecao>(model);
                    domain.Ativo = true;
                    _colecaoRepository.Save(domain);

                    this.AddSuccessMessage("Coleção cadastrada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar a coleção. Confira se os dados foram informados corretamente: " + exception.Message);
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
            var domain = _colecaoRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<ColecaoModel>(domain);
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a coleção.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Editar(ColecaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _colecaoRepository.Get(model.Id));

                    _colecaoRepository.Update(domain);

                    this.AddSuccessMessage("Coleção atualizada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a coleção. Confira se os dados foram informados corretamente: " + exception.Message);
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
                    var domain = _colecaoRepository.Get(id);
                    _colecaoRepository.Delete(domain);

                    this.AddSuccessMessage("Coleção excluída com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir a coleção: " + exception.Message);
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
                var domain = _colecaoRepository.Get(id);

                if (domain != null)
                {
                    var situacao = !domain.Ativo;

                    domain.Ativo = situacao;
                    _colecaoRepository.Update(domain);
                    this.AddSuccessMessage("Coleção {0} com sucesso", situacao ? "ativada" : "inativada");
                }
                else
                {
                    this.AddErrorMessage("O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao editar a situação da coleção: " + exception.Message);
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
            var colecao = model as ColecaoModel;

            // Verificar duplicado
            if (_colecaoRepository.Find(p => p.Descricao == colecao.Descricao && p.Id != colecao.Id).Any())
                ModelState.AddModelError("Descricao", "Já existe uma coleção cadastrada com esta descrição.");
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
            var domain = _colecaoRepository.Get(id);

            // Verificar relacionamento
            if (_modeloRepository.Find().Any(p => p.Colecao == domain))
                ModelState.AddModelError("", "Não é possível excluir esta coleção, pois existe(m) croqui(s) associadas a ela.");
        }
        #endregion

        #endregion
    }
}