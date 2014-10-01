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
    public partial class ProfissaoController : BaseController
    {
		#region Variaveis
        private readonly IRepository<Profissao> _profissaoRepository;
        private readonly IRepository<Cliente> _clienteRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public ProfissaoController(ILogger logger, IRepository<Profissao> profissaoRepository,
            IRepository<Cliente> clienteRepository)
        {
            _profissaoRepository = profissaoRepository;
            _clienteRepository = clienteRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var profissaos = _profissaoRepository.Find();

            var list = profissaos.Select(p => new GridProfissaoModel
            {
                Id = p.Id.GetValueOrDefault(),
                Nome = p.Nome
            }).ToList();

            return View(list);
        }
        #endregion

        #region Novo

        public virtual ActionResult Novo()
        {
            return View(new ProfissaoModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Novo(ProfissaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<Profissao>(model);
                    _profissaoRepository.Save(domain);

                    this.AddSuccessMessage("Profissão cadastrada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar a profissão. Confira se os dados foram informados corretamente.");
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
            var domain = _profissaoRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<ProfissaoModel>(domain);
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a profissão.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Editar(ProfissaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _profissaoRepository.Get(model.Id));

                    _profissaoRepository.Update(domain);

                    this.AddSuccessMessage("Profissão atualizada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a profissão. Confira se os dados foram informados corretamente.");
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
                    var domain = _profissaoRepository.Get(id);
                    _profissaoRepository.Delete(domain);

                    this.AddSuccessMessage("Profissão excluída com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir a profissão: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return RedirectToAction("Editar", new { id });
        }
        #endregion

        #endregion

        #region Métodos

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var profissao = model as ProfissaoModel;

            // Verificar duplicado
            if (_profissaoRepository.Find(p => p.Nome == profissao.Nome && p.Id != profissao.Id).Any())
                ModelState.AddModelError("Nome", "Já existe profissão cadastrada com este nome.");
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
            var domain = _profissaoRepository.Get(id);

            // Verifica uso
            if (_clienteRepository.Find(p => p.Profissao.Id == domain.Id).Any())
                ModelState.AddModelError("", "Não é possível excluir esta profissão pois exitem clientes cadastrados com ela.");
        }
        #endregion

        #endregion
    }
}