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
    public partial class TipoFornecedorController : BaseController
    {
		#region Variaveis
        private readonly IRepository<TipoFornecedor> _tipoFornecedorRepository;
        private readonly IRepository<Fornecedor> _fornecedorRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public TipoFornecedorController(ILogger logger, IRepository<TipoFornecedor> tipoFornecedorRepository,
            IRepository<Fornecedor> fornecedorRepository)
        {
            _tipoFornecedorRepository = tipoFornecedorRepository;
            _fornecedorRepository = fornecedorRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var tipoFornecedors = _tipoFornecedorRepository.Find();

            var list = tipoFornecedors.Select(p => new GridTipoFornecedorModel
            {
                Id = p.Id.GetValueOrDefault(),
                Descricao = p.Descricao
            }).ToList();

            return View(list);
        }
        #endregion

        #region Novo

        public virtual ActionResult Novo()
        {
            return View(new TipoFornecedorModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Novo(TipoFornecedorModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<TipoFornecedor>(model);
                    _tipoFornecedorRepository.Save(domain);

                    this.AddSuccessMessage("Tipo de fornecedor cadastrado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar o tipo de fornecedor. Confira se os dados foram informados corretamente.");
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
            var domain = _tipoFornecedorRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<TipoFornecedorModel>(domain);
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o tipo de fornecedor.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Editar(TipoFornecedorModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _tipoFornecedorRepository.Get(model.Id));

                    _tipoFornecedorRepository.Update(domain);

                    this.AddSuccessMessage("Tipo de fornecedor atualizado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o tipo de fornecedor. Confira se os dados foram informados corretamente.");
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
                    var domain = _tipoFornecedorRepository.Get(id);
                    _tipoFornecedorRepository.Delete(domain);

                    this.AddSuccessMessage("Tipo de fornecedor excluído com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir o tipo de fornecedor: " + exception.Message);
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
            var tipoFornecedor = model as TipoFornecedorModel;

            // Verificar duplicado
            if (_tipoFornecedorRepository.Find(p => p.Descricao == tipoFornecedor.Descricao && p.Id != tipoFornecedor.Id).Any())
                ModelState.AddModelError("Descricao", "Já existe tipo de fornecedor cadastrado com esta descrição.");
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
            var domain = _tipoFornecedorRepository.Get(id);

            // Verifica uso
            if (_fornecedorRepository.Find(p => p.TipoFornecedor.Id == domain.Id).Any())
                ModelState.AddModelError("", "Não é possível excluir este tipo de fornecedor pois exitem fornecedores cadastrados com ele.");
        }
        #endregion

        #endregion
    }
}