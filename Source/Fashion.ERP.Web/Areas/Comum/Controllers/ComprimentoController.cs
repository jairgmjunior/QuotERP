using System;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.EngenhariaProduto;
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
    public partial class ComprimentoController : BaseController
    {
		#region Variaveis
        private readonly IRepository<Comprimento> _comprimentoRepository;
        private readonly IRepository<Modelo> _modeloRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public ComprimentoController(ILogger logger, IRepository<Comprimento> comprimentoRepository,
            IRepository<Modelo> modeloRepository)
        {
            _comprimentoRepository = comprimentoRepository;
            _modeloRepository = modeloRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var comprimentos = _comprimentoRepository.Find();

            var list = comprimentos.Select(p => new GridComprimentoModel
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
            return View(new ComprimentoModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Novo(ComprimentoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<Comprimento>(model);
                    domain.Ativo = true;
                    _comprimentoRepository.Save(domain);

                    this.AddSuccessMessage("Comprimento cadastrado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar o comprimento. Confira se os dados foram informados corretamente: " + exception.Message);
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
            var domain = _comprimentoRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<ComprimentoModel>(domain);
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o comprimento.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Editar(ComprimentoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _comprimentoRepository.Get(model.Id));

                    _comprimentoRepository.Update(domain);

                    this.AddSuccessMessage("Comprimento atualizado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o comprimento. Confira se os dados foram informados corretamente: " + exception.Message);
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
                    var domain = _comprimentoRepository.Get(id);
                    _comprimentoRepository.Delete(domain);

                    this.AddSuccessMessage("Comprimento excluído com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir o artigo: " + exception.Message);
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
                var domain = _comprimentoRepository.Get(id);

                if (domain != null)
                {
                    var situacao = !domain.Ativo;

                    domain.Ativo = situacao;
                    _comprimentoRepository.Update(domain);
                    this.AddSuccessMessage("Comprimento {0} com sucesso", situacao ? "ativado" : "inativado");
                }
                else
                {
                    this.AddErrorMessage("O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao editar a situação do comprimento: " + exception.Message);
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
            var comprimento = model as ComprimentoModel;

            // Verificar duplicado
            if (_comprimentoRepository.Find(p => p.Descricao == comprimento.Descricao && p.Id != comprimento.Id).Any())
                ModelState.AddModelError("Descricao", "Já existe um comprimento cadastrado com esta descrição.");
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
            var domain = _comprimentoRepository.Get(id);

            // Verificar relacionamento
            if (_modeloRepository.Find().Any(p => p.Comprimento == domain))
                ModelState.AddModelError("", "Não é possível excluir este comprimento, pois existe(m) modelo(s) associadas a ele.");
        }
        #endregion

        public virtual JsonResult ObtenhaLista()
        {
            var comprimentos = _comprimentoRepository
                .Find(x => x.Ativo)
                .Select(s => new { s.Id, s.Descricao }).OrderBy(o => o.Descricao).ToList();

            return Json(comprimentos, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}