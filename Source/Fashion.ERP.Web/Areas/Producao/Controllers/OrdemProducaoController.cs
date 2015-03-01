using System;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.Producao;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.Producao.Models;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Producao.Controllers
{
    public partial class OrdemProducaoController : BaseController
    {
		#region Variaveis
        private readonly IRepository<OrdemProducao> _ordemProducaoRepository;
        private readonly IRepository<DepartamentoProducao> _departamentoProducaoRepository;
        private readonly IRepository<FichaTecnica> _fichaTecnicaRepository;
        private readonly IRepository<Tamanho> _tamanhoRepository;
        private readonly IRepository<Variacao> _variacaoRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public OrdemProducaoController(ILogger logger, IRepository<OrdemProducao> ordemProducaoRepository,
            IRepository<DepartamentoProducao> departamentoProducaoRepository, IRepository<FichaTecnica> fichaTecnicaRepository,
            IRepository<Tamanho> tamanhoRepository, IRepository<Variacao> variacaoRepository)
        {
            _ordemProducaoRepository = ordemProducaoRepository;
            _departamentoProducaoRepository = departamentoProducaoRepository;
            _fichaTecnicaRepository = fichaTecnicaRepository;
            _tamanhoRepository = tamanhoRepository;
            _variacaoRepository = variacaoRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        [PopulateViewData("PopulateViewDataPesquisa")]
        public virtual ActionResult Index()
        {
            var ordemProducoes = _ordemProducaoRepository.Find();

            var model = new PesquisaOrdemProducaoModel { ModoConsulta = "Listar" };

            model.Grid = ordemProducoes.Select(p => new GridOrdemProducaoModel
            {
                Id = p.Id.GetValueOrDefault(),
                Numero = p.Numero,
                Tag = p.FichaTecnica.Tag,
                Ano = p.FichaTecnica.Ano,
                Data = p.Data,
                Descricao = p.FichaTecnica.Descricao,
                Situacao = p.SituacaoOrdemProducao.EnumToString(),
                //Foto = p.FichaTecnica.
            }).ToList();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewDataPesquisa")]
        public virtual ActionResult Index(PesquisaOrdemProducaoModel model)
        {
            var ordemProducoes = _ordemProducaoRepository.Find();
            return View(model);
        }

        #endregion

        #region Novo

		[PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo()
		{
		    var fichaTecnica = _fichaTecnicaRepository.Find().FirstOrDefault();

		    var model = new OrdemProducaoModel();

            foreach (var tamanho in fichaTecnica.FichaTecnicaMatriz.Grade.Tamanhos)
		    {
		        model.ItemTamanhos.Add(tamanho.Key.Id.GetValueOrDefault());
		    }

            foreach (var variacaoMatrizes in fichaTecnica.FichaTecnicaMatriz.FichaTecnicaVariacaoMatrizs)
		    {
		        model.ItemVariacaoMatrizes.Add(variacaoMatrizes.Variacao.Id.GetValueOrDefault());
		    }

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(OrdemProducaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<OrdemProducao>(model);
                    _ordemProducaoRepository.Save(domain);

                    this.AddSuccessMessage("OrdemProducao cadastrado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar o OrdemProducao. Confira se os dados foram informados corretamente: " + exception.Message);
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
            var domain = _ordemProducaoRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<OrdemProducaoModel>(domain);
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o OrdemProducao.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(OrdemProducaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _ordemProducaoRepository.Get(model.Id));

                    _ordemProducaoRepository.Update(domain);

                    this.AddSuccessMessage("OrdemProducao atualizado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o OrdemProducao. Confira se os dados foram informados corretamente: " + exception.Message);
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
					var domain = _ordemProducaoRepository.Get(id);
					_ordemProducaoRepository.Delete(domain);

					this.AddSuccessMessage("OrdemProducao excluído com sucesso");
					return RedirectToAction("Index");
				}
				catch (Exception exception)
				{
					ModelState.AddModelError(string.Empty, "Ocorreu um erro ao excluir o OrdemProducao: " + exception.Message);
					_logger.Info(exception.GetMessage());
				}
			}

			return RedirectToAction("Editar", new { id });
        }
        #endregion
		
        #endregion

		#region Métodos
		
        #region PopulateViewData
        protected void PopulateViewData(OrdemProducaoModel model)
        {
            // Departamentos
            var departamentos = _departamentoProducaoRepository.Find().OrderBy(p => p.Nome).ToList();

            var departamentosAtivo = departamentos.Where(p => p.Ativo).ToList();
            ViewData["DepartamentoProducao"] = departamentosAtivo.ToSelectList("Nome");

            ViewBag.DepartamentosDicionario = departamentos.ToDictionary(t => t.Id, t => t.Nome);

            // Tamanhos
            var tamanhos = _tamanhoRepository.Find().ToList();
            ViewBag.TamanhosDicionario = tamanhos.ToDictionary(t => t.Id, t => t.Descricao);

            // Variações
            var variacoes = _variacaoRepository.Find().ToList();
            ViewBag.VariacoesDicionario = variacoes.ToDictionary(t => t.Id, t => t.Nome);
        }
        #endregion

        #region PopulateViewDataPesquisa
        protected void PopulateViewDataPesquisa(PesquisaOrdemProducaoModel model)
        {
        }
        #endregion

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var ordemProducao = model as OrdemProducaoModel;
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
        }
        #endregion

        #endregion
    }
}