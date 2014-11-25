using System;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;
using Fashion.ERP.Web.Areas.Comum.Models;

namespace Fashion.ERP.Web.Areas.Comum.Controllers
{
    public partial class SetorProducaoController : BaseController
    {
        #region Variaveis
        private readonly IRepository<Modelo> _modeloRepository;
        private readonly IRepository<SetorProducao> _setorProducaoRepository;
        private readonly IRepository<DepartamentoProducao> _departamentoProducaoRepository;
        private readonly IRepository<OperacaoProducao> _operacaoProducaoRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public SetorProducaoController(ILogger logger, 
            IRepository<SetorProducao> setorProducaoRepository,
            IRepository<DepartamentoProducao> departamentoProducaoRepository, 
            IRepository<OperacaoProducao> operacaoProducaoRepository,
            IRepository<Modelo> modeloRepository)
        {
            _modeloRepository = modeloRepository;
            _setorProducaoRepository = setorProducaoRepository;
            _departamentoProducaoRepository = departamentoProducaoRepository;
            _operacaoProducaoRepository = operacaoProducaoRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var setorProducaos = _setorProducaoRepository.Find();

            var list = setorProducaos.Select(p => new GridSetorProducaoModel
            {
                Id = p.Id.GetValueOrDefault(),
                Nome = p.Nome,
                DepartamentoProducao = p.DepartamentoProducao.Nome,
                Ativo = p.Ativo
            }).ToList().OrderBy(o => o.Nome);

            return View(list);
        }
        #endregion

        #region Novo

        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo()
        {
            return View(new SetorProducaoModel());
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(SetorProducaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<SetorProducao>(model);
                    domain.Ativo = true;
                    _setorProducaoRepository.Save(domain);

                    this.AddSuccessMessage("Setor produtivo cadastrado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar o setor produtivo. Confira se os dados foram informados corretamente: " + exception.Message);
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
            var domain = _setorProducaoRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<SetorProducaoModel>(domain);
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o setor produtivo.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(SetorProducaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _setorProducaoRepository.Get(model.Id));

                    _setorProducaoRepository.Update(domain);

                    this.AddSuccessMessage("Setor produtivo atualizado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o setor produtivo. Confira se os dados foram informados corretamente: " + exception.Message);
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
                    var domain = _setorProducaoRepository.Get(id);
                    _setorProducaoRepository.Delete(domain);

                    this.AddSuccessMessage("Setor produtivo excluído com sucesso");
                    return RedirectToAction("Index");

                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir o setor produtivo: " + exception.Message);
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
                var domain = _setorProducaoRepository.Get(id);

                if (domain != null)
                {
                    var situacao = !domain.Ativo;

                    domain.Ativo = situacao;
                    _setorProducaoRepository.Update(domain);
                    this.AddSuccessMessage("Setor produtivo {0} com sucesso", situacao ? "ativado" : "inativado");
                }
                else
                {
                    this.AddErrorMessage("O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao editar a situação do setor produtivo: " + exception.Message);
                _logger.Info(exception.GetMessage());
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region SetoresDepartamento
        [OutputCache(Duration = 60, VaryByParam = "id"), AjaxOnly]
        public virtual JsonResult SetoresDepartamento_(long? IdDepartamento /* Id do departamento de produção*/)
        {
            var setores = _setorProducaoRepository
                .Find(p => p.DepartamentoProducao.Id == IdDepartamento)
                .Select(s => new { s.Id, s.Nome }).ToList().OrderBy(o => o.Nome);

            return Json(setores, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 60, VaryByParam = "id"), AjaxOnly]
        public virtual JsonResult SetoresDepartamento(long? id /* Id do departamento*/, long? modeloId)
        {
            var modelo = _modeloRepository.Get(modeloId);

            var setores = modelo.SequenciaProducoes
                .Where(s => s.DepartamentoProducao.Id == id && s.SetorProducao != null)
                .Select(sp => new { sp.SetorProducao.Id, sp.SetorProducao.Nome })
                .ToList();

            return Json(setores, JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]
        public virtual JsonResult SetoresDepartamento2(String NomeDepartamento)
        {
            var setores = _setorProducaoRepository
                .Find(p => p.DepartamentoProducao.Nome == NomeDepartamento)
                .Select(s => new { s.Id, s.Nome }).ToList().OrderBy(o => o.Nome);

            return Json(setores, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region Métodos

        #region PopulateViewData
        protected void PopulateViewData(SetorProducaoModel model)
        {
            var departamentoProducoes = _departamentoProducaoRepository.Find(p => p.Ativo).OrderBy(p => p.Nome).ToList();
            ViewData["DepartamentoProducao"] = departamentoProducoes.ToSelectList("Nome", model.DepartamentoProducao);

            model.Populate(_departamentoProducaoRepository, _setorProducaoRepository);
        }
        #endregion

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var setorProducao = model as SetorProducaoModel;

            // Verificar duplicado
            if (_setorProducaoRepository.Find(p => p.Nome == setorProducao.Nome && p.Id != setorProducao.Id && setorProducao.DepartamentoProducao == p.DepartamentoProducao.Id).Any())
                ModelState.AddModelError("Nome", "Já existe um setor produtivo cadastrado neste departamento com este nome.");

        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
            var domain = _setorProducaoRepository.Get(id);

            // Verificar relacionamento
            if (_operacaoProducaoRepository.Find().Any(p => p.SetorProducao == domain))
                ModelState.AddModelError("", "Não é possível excluir este setor produtivo, pois existe operação do setores associadas a ele.");
        }
        #endregion

        #endregion
    }
}