using System;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.ERP.Reporting.Helpers;
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
    public partial class DepartamentoProducaoController : BaseController
    {
		#region Variaveis
        private readonly IRepository<DepartamentoProducao> _departamentoProducaoRepository;
        private readonly IRepository<SetorProducao> _setorProducaoRepository;
        private readonly IRepository<SequenciaProducao> _sequenciaProdutivaRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public DepartamentoProducaoController(ILogger logger, IRepository<DepartamentoProducao> departamentoProducaoRepository,
            IRepository<SetorProducao> setorProducaoRepository, IRepository<SequenciaProducao> sequenciaProdutivaRepository)
        {
            _departamentoProducaoRepository = departamentoProducaoRepository;
            _setorProducaoRepository = setorProducaoRepository;
            _sequenciaProdutivaRepository = sequenciaProdutivaRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var departamentoProducoes = _departamentoProducaoRepository.Find().Select(p => new GridDepartamentoProducaoModel
            {
                Id = p.Id.GetValueOrDefault(),
                Nome = p.Nome,
                Criacao = p.Criacao.ToSimNao(),
                Producao = p.Producao.ToSimNao(),
                Ativo = p.Ativo
            }).ToList().OrderBy(o => o.Nome);

            return View(departamentoProducoes);
        }
        #endregion

        #region Novo

        public virtual ActionResult Novo()
        {
            return View(new DepartamentoProducaoModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Novo(DepartamentoProducaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<DepartamentoProducao>(model);
                    domain.Ativo = true;
                    _departamentoProducaoRepository.Save(domain);

                    this.AddSuccessMessage("Departamento produtivo cadastrado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar o departamento produtivo. Confira se os dados foram informados corretamente: " + exception.Message);
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
            var domain = _departamentoProducaoRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<DepartamentoProducaoModel>(domain);
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o departamento produtivo.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Editar(DepartamentoProducaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _departamentoProducaoRepository.Get(model.Id));

                    _departamentoProducaoRepository.Update(domain);

                    this.AddSuccessMessage("Departamento produtivo atualizado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o departamento produtivo. Confira se os dados foram informados corretamente: " + exception.Message);
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
                    var domain = _departamentoProducaoRepository.Get(id);
                    _departamentoProducaoRepository.Delete(domain);

                    this.AddSuccessMessage("Departamento produtivo excluído com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir o departamento produtivo: " + exception.Message);
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
                var domain = _departamentoProducaoRepository.Get(id);

                if (domain != null)
                {
                    var situacao = !domain.Ativo;

                    domain.Ativo = situacao;
                    _departamentoProducaoRepository.Update(domain);
                    this.AddSuccessMessage("Departamento produtivo {0} com sucesso", situacao ? "ativado" : "inativado");
                }
                else
                {
                    this.AddErrorMessage("O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao editar a situação do departamento produtivo: " + exception.Message);
                _logger.Info(exception.GetMessage());
            }

            return RedirectToAction("Index");
        }
        #endregion

        
        public virtual JsonResult GetDepartamentos()
        {
            var departamentos = _departamentoProducaoRepository
                .Find()
                .Select(s => new { s.Id, s.Nome }).ToList().OrderBy(o => o.Nome);

            return Json(departamentos, JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult GetDepartamentosSelectList()
        {
            var departamentos = _departamentoProducaoRepository.Find().ToList().ToSelectList("Nome");

            return Json(departamentos, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Métodos

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var departamentoProducao = model as DepartamentoProducaoModel;

            // Verificar duplicado
            if (_departamentoProducaoRepository.Find(p => p.Nome == departamentoProducao.Nome && p.Id != departamentoProducao.Id).Any())
                ModelState.AddModelError("Nome", "Já existe departamento produtivo cadastrada com este nome.");
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
            var domain = _departamentoProducaoRepository.Get(id);

            // Verificar relacionamento
            if (_setorProducaoRepository.Find().Any(p => p.DepartamentoProducao == domain))
                ModelState.AddModelError("", "Não é possível excluir este departamento produtivo, pois existe setor produtivo associado a ele.");

            if (_sequenciaProdutivaRepository.Find().Any(p => p.DepartamentoProducao == domain))
                ModelState.AddModelError("", "Não é possível excluir este departamento produtivo, pois existe uma sequência de produção associado a ele.");
        }
        #endregion

        #endregion
    }
}