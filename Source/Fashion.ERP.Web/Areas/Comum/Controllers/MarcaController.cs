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
    public partial class MarcaController : BaseController
    {
		#region Variaveis
        private readonly IRepository<Marca> _marcaRepository;
        private readonly IRepository<Modelo> _modeloRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public MarcaController(ILogger logger, IRepository<Marca> marcaRepository,
            IRepository<Modelo> modeloRepository)
        {
            _marcaRepository = marcaRepository;
            _modeloRepository = modeloRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var marcas = _marcaRepository.Find();

            var list = marcas.Select(p => new GridMarcaModel
            {
                Id = p.Id.GetValueOrDefault(),
                Nome = p.Nome,
                Ativo = p.Ativo
            }).ToList().OrderBy(o => o.Nome);

            return View(list);
        }
        #endregion

        #region Novo

        public virtual ActionResult Novo()
        {
            return View(new MarcaModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Novo(MarcaModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<Marca>(model);
                    domain.Ativo = true;
                    _marcaRepository.Save(domain);

                    this.AddSuccessMessage("Marca cadastrada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar a marca. Confira se os dados foram informados corretamente: " + exception.Message);
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
            var domain = _marcaRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<MarcaModel>(domain);
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a marca.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Editar(MarcaModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _marcaRepository.Get(model.Id));

                    _marcaRepository.Update(domain);

                    this.AddSuccessMessage("Marca atualizada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a marca. Confira se os dados foram informados corretamente: " + exception.Message);
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
                    var domain = _marcaRepository.Get(id);
                    _marcaRepository.Delete(domain);

                    this.AddSuccessMessage("Marca excluída com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir a marca: " + exception.Message);
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
                var domain = _marcaRepository.Get(id);

                if (domain != null)
                {
                    var situacao = !domain.Ativo;

                    domain.Ativo = situacao;
                    _marcaRepository.Update(domain);
                    this.AddSuccessMessage("Marca {0} com sucesso", situacao ? "ativada" : "inativada");
                }
                else
                {
                    this.AddErrorMessage("O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao editar a situação da marca: " + exception.Message);
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
            var marca = model as MarcaModel;

            // Verificar duplicado
            if (_marcaRepository.Find(p => p.Nome == marca.Nome && p.Id != marca.Id).Any())
                ModelState.AddModelError("Nome", "Já existe uma marca cadastrada com este nome.");
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
            var domain = _marcaRepository.Get(id);

            // Verificar relacionamento
            if (_modeloRepository.Find().Any(p => p.Marca == domain))
                ModelState.AddModelError("", "Não é possível excluir esta marca, pois existe(m) croqui(s) associadas a ela.");
        }
        #endregion

        #endregion
    }
}