using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
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
    public partial class GradeController : BaseController
    {
        #region Variaveis
        private readonly IRepository<Grade> _gradeRepository;
        private readonly IRepository<Tamanho> _tamanhoRepository;
        private readonly IRepository<Modelo> _modeloRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public GradeController(ILogger logger, IRepository<Grade> gradeRepository,
             IRepository<Tamanho> tamanhoRepository, IRepository<Modelo> modeloRepository)
        {
            _gradeRepository = gradeRepository;
            _tamanhoRepository = tamanhoRepository;
            _modeloRepository = modeloRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var grades = _gradeRepository.Find();

            var list = grades.Select(p => new GridGradeModel
            {
                Id = p.Id.GetValueOrDefault(),
                Descricao = p.Descricao,
                DataCriacao = p.DataCriacao,
                Ativo = p.Ativo
            }).ToList().OrderBy(o => o.Descricao);

            return View(list);
        }
        #endregion

        #region Novo

        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo()
        {
            return View(new GradeModel());
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(GradeModel model)
        {
            if (model.Tamanhos.IsNullOrEmpty())
                ModelState.AddModelError(string.Empty, "Adicione pelo menos um tamanho à grade.");

            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<Grade>(model);

                    var tamanhos = new Dictionary<Tamanho, int>();
                    for (int i = 0; i < model.Tamanhos.Count; i++)
                    {
                        var tamanhoId = model.Tamanhos[i];
                        tamanhos.Add(_tamanhoRepository.Load(tamanhoId), i + 1);
                    }
                    domain.Tamanhos = tamanhos;
                    domain.DataCriacao = DateTime.Now;
                    domain.Ativo = true;

                    _gradeRepository.Save(domain);

                    this.AddSuccessMessage("Grade cadastrada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar a grade. Confira se os dados foram informados corretamente: " + exception.Message);
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
            var domain = _gradeRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<GradeModel>(domain);
                model.Tamanhos = domain.Tamanhos.OrderBy(o => o.Value).Select(p => p.Key.Id ?? 0).ToList();

                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a grade.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(GradeModel model)
        {
            if (model.Tamanhos.IsNullOrEmpty())
                ModelState.AddModelError(string.Empty, "Adicione pelo menos um tamanho à grade.");

            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _gradeRepository.Get(model.Id));
                    
                    var tamanhos = new Dictionary<Tamanho, int>();
                    for (int i = 0; i < model.Tamanhos.Count; i++)
                    {
                        var tamanhoId = model.Tamanhos[i];
                        tamanhos.Add(_tamanhoRepository.Load(tamanhoId), i + 1);
                    }
                    domain.Tamanhos = tamanhos;

                    _gradeRepository.Update(domain);

                    this.AddSuccessMessage("Grade atualizada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a grade. Confira se os dados foram informados corretamente: " + exception.Message);
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
                    var domain = _gradeRepository.Get(id);
                    _gradeRepository.Delete(domain);

                    this.AddSuccessMessage("Grade excluída com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir a grade: " + exception.Message);
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
                var domain = _gradeRepository.Get(id);

                if (domain != null)
                {
                    var situacao = !domain.Ativo;

                    domain.Ativo = situacao;
                    _gradeRepository.Update(domain);
                    this.AddSuccessMessage("Grade {0} com sucesso", situacao ? "ativada" : "inativada");
                }
                else
                {
                    this.AddErrorMessage("O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao editar a situação da grade: " + exception.Message);
                _logger.Info(exception.GetMessage());
            }

            return RedirectToAction("Index");
        }
        #endregion

        #endregion

        #region Métodos

        #region PopulateViewData
        protected void PopulateViewData(GradeModel model)
        {
            var tamanhos = _tamanhoRepository.Find().OrderBy(p => p.Descricao).ToList();

            var tamanhosAtivo = tamanhos.Where(p => p.Ativo).ToList();
            ViewData["Tamanho"] = tamanhosAtivo.ToSelectList("Descricao");

            ViewBag.TamanhosDicionario = tamanhos.ToDictionary(t => t.Id, t => t.Descricao);
        }
        #endregion

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var grade = model as GradeModel;

            // Verificar duplicado
            if (_gradeRepository.Find(p => p.Descricao == grade.Descricao && p.Id != grade.Id).Any())
                ModelState.AddModelError("Descricao", "Já existe uma grade cadastrada com esta descrição.");
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
            var domain = _gradeRepository.Get(id);

            // Verificar relacionamento
            if (_modeloRepository.Find().Any(p => p.Grade == domain))
                ModelState.AddModelError("", "Não é possível excluir esta grade, pois existe(m) modelo(s) associadas a ela.");
        }
        #endregion

        #endregion
    }
}