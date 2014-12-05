using System;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;
using Fashion.ERP.Web.Areas.Comum.Models;
using Fashion.ERP.Domain.EngenhariaProduto;


namespace Fashion.ERP.Web.Areas.Comum.Controllers
{
    public partial class OperacaoProducaoController : BaseController
    {
		#region Variaveis
        private readonly IRepository<OperacaoProducao> _operacaoProducaoRepository;
        private readonly IRepository<SetorProducao> _setorProducaoRepository;
        private readonly IRepository<DepartamentoProducao> _departamentoProducaoRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public OperacaoProducaoController(ILogger logger, IRepository<OperacaoProducao> operacaoProducaoRepository,
            IRepository<SetorProducao> setorProducaoRepository, IRepository<DepartamentoProducao> departamentoProducaoRepository)
        {
            _operacaoProducaoRepository = operacaoProducaoRepository;
            _setorProducaoRepository = setorProducaoRepository;
            _departamentoProducaoRepository = departamentoProducaoRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var operacaoProducaos = _operacaoProducaoRepository.Find();

            var list = operacaoProducaos.Select(p => new GridOperacaoProducaoModel
            {
                Id = p.Id.GetValueOrDefault(),
                Descricao = p.Descricao,
                Tempo = p.Tempo,
                Custo = p.Custo,
                Ativo = p.Ativo,
                SetorProducao = p.SetorProducao.Nome,
                DepartamentoProducao = p.SetorProducao.DepartamentoProducao.Nome
            }).ToList().OrderBy(o => o.Descricao);

            return View(list);
        }
        #endregion

        #region Novo

        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo()
        {
            return View(new OperacaoProducaoModel());
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(OperacaoProducaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<OperacaoProducao>(model);
                    domain.Ativo = true;
                    _operacaoProducaoRepository.Save(domain);

                    this.AddSuccessMessage("Operação dos setores cadastrada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar a operação do setores. Confira se os dados foram informados corretamente: " + exception.Message);
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
            var domain = _operacaoProducaoRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<OperacaoProducaoModel>(domain);
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a operação do setores.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(OperacaoProducaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _operacaoProducaoRepository.Get(model.Id));

                    _operacaoProducaoRepository.Update(domain);

                    this.AddSuccessMessage("Operação dos setores atualizada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a operação do setores. Confira se os dados foram informados corretamente: " + exception.Message);
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
                    var domain = _operacaoProducaoRepository.Get(id);
                    _operacaoProducaoRepository.Delete(domain);

                    this.AddSuccessMessage("Operação do setores excluída com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("",
                                             "Ocorreu um erro ao excluir a operação do setores: " + exception.Message);
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
                var domain = _operacaoProducaoRepository.Get(id);

                if (domain != null)
                {
                    var situacao = !domain.Ativo;

                    domain.Ativo = situacao;
                    _operacaoProducaoRepository.Update(domain);
                    this.AddSuccessMessage("Operação dos setores {0} com sucesso", situacao ? "ativado" : "inativado");
                }
                else
                {
                    this.AddErrorMessage("O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao editar a situação da operação do setores: " + exception.Message);
                _logger.Info(exception.GetMessage());
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region OperacoesPorSetor
        [AjaxOnly]
        public virtual JsonResult OperacoesPorSetor(String NomeSetor)
        {
            var operacores = _operacaoProducaoRepository
                .Find(p => p.SetorProducao.Nome == NomeSetor)
                .Select(s => new { s.Id, s.Descricao }).ToList().OrderBy(o => o.Descricao);

            return Json(operacores, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion

        #region Métodos

        #region PopulateViewData
        protected void PopulateViewData(OperacaoProducaoModel model)
        {
            var setorProducao = _setorProducaoRepository.Get(p => p.Id == model.SetorProducao);
                
            var setorProducaoId = 0L;
            var departamentoProducaoId = 0L;

            if (setorProducao != null)
            {
                setorProducaoId = setorProducao.Id.GetValueOrDefault();
                departamentoProducaoId = setorProducao.DepartamentoProducao.Id.GetValueOrDefault();
            }

            var departamentos = _departamentoProducaoRepository
                .Find(p => p.Ativo)
                .OrderBy(p => p.Nome).ToList();
            ViewData["DepartamentoProducao"] = departamentos.ToSelectList("Nome", departamentoProducaoId);

            var setores = _setorProducaoRepository.Find(p => p.Ativo);

            if (departamentos.Any())
                setores = setores.Where(p => p.DepartamentoProducao.Id == departamentoProducaoId);

            var setorProducoes = setores.OrderBy(p => p.Nome).ToList();
            ViewData["SetorProducao"] = setorProducoes.ToSelectList("Nome", setorProducaoId);

            model.Populate(_departamentoProducaoRepository, _setorProducaoRepository, _operacaoProducaoRepository);
        }
        #endregion

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var operacaoProducao = model as OperacaoProducaoModel;

            // Verificar duplicado
            if (_operacaoProducaoRepository.Find(p => p.Descricao == operacaoProducao.Descricao && p.Id != operacaoProducao.Id && p.SetorProducao.Id == operacaoProducao.SetorProducao).Any())
                ModelState.AddModelError("Descricao", "Já existe uma operação do setores neste setor do departamento cadastrado com esta descrição.");
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
            // todo: Verificar relacionamento
        }
        #endregion

        #endregion
    }
}