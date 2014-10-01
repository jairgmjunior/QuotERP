using System;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
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
    public partial class TamanhoController : BaseController
    {
		#region Variaveis
        private readonly IRepository<Tamanho> _tamanhoRepository;
        private readonly IRepository<Grade> _gradeRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public TamanhoController(ILogger logger, IRepository<Tamanho> tamanhoRepository,
            IRepository<Grade> gradeRepository)
        {
            _tamanhoRepository = tamanhoRepository;
            _gradeRepository = gradeRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var tamanhos = _tamanhoRepository.Find();

            var list = tamanhos.Select(p => new GridTamanhoModel
            {
                Id = p.Id.GetValueOrDefault(),
                Descricao = p.Descricao,
                Sigla = p.Sigla,
                Ativo = p.Ativo
            }).ToList();

            return View(list);
        }
        #endregion

        #region Novo

        public virtual ActionResult Novo()
        {
            return View(new TamanhoModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Novo(TamanhoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<Tamanho>(model);
                    domain.Ativo = true;
                    _tamanhoRepository.Save(domain);

                    this.AddSuccessMessage("Tamanho cadastrado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar o tamanho. Confira se os dados foram informados corretamente: " + exception.Message);
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
            var domain = _tamanhoRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<TamanhoModel>(domain);
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o tamanho.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Editar(TamanhoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _tamanhoRepository.Get(model.Id));

                    _tamanhoRepository.Update(domain);

                    this.AddSuccessMessage("Tamanho atualizado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o tamanho. Confira se os dados foram informados corretamente: " + exception.Message);
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
                    var domain = _tamanhoRepository.Get(id);
                    _tamanhoRepository.Delete(domain);

                    this.AddSuccessMessage("Tamanho excluído com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir o tamanho: " + exception.Message);
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
                var domain = _tamanhoRepository.Get(id);

                if (domain != null)
                {
                    var situacao = !domain.Ativo;

                    domain.Ativo = situacao;
                    _tamanhoRepository.Update(domain);
                    this.AddSuccessMessage("Tamanho {0} com sucesso", situacao ? "ativado" : "inativado");
                }
                else
                {
                    this.AddErrorMessage("O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao editar a situação do tamanho: " + exception.Message);
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
            var tamanho = model as TamanhoModel;

            // Verificar duplicado
            if (_tamanhoRepository.Find(p => p.Sigla == tamanho.Sigla && p.Id != tamanho.Id).Any())
                ModelState.AddModelError("Sigla", "Já existe tamanho cadastrado com esta sigla.");
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
            var domain = _tamanhoRepository.Get(id);

            // Verificar relacionamento
            if (_gradeRepository.Find().ToList().Any(p => p.Tamanhos.Any(t => t.Key.Id == domain.Id)))
                ModelState.AddModelError("", "Não é possível excluir este tamanho, pois existe(m) grade(s) de tamanho associado a ele.");
        }
        #endregion

        #endregion
    }
}