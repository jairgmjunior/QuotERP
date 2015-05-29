using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.Almoxarifado.Models;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Kendo.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Ninject.Extensions.Logging;
using WebGrease.Css.Extensions;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Controllers
{
    public partial class SaidaMaterialController : BaseController
    {
		#region Variaveis
        private readonly IRepository<SaidaMaterial> _saidaMaterialRepository;
        private readonly IRepository<DepositoMaterial> _depositoMaterialRepository;
        private readonly IRepository<Material> _materialRepository;
        private readonly IRepository<EstoqueMaterial> _estoqueMaterialRepository;
        private readonly IRepository<RequisicaoMaterial> _requisicaoMaterialRepository;
        private readonly IRepository<CentroCusto> _centroCustoRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly ILogger _logger;

        #endregion

        #region Construtores
        public SaidaMaterialController(ILogger logger, IRepository<SaidaMaterial> saidaMaterialRepository,
            IRepository<DepositoMaterial> depositoMaterialRepository, 
            IRepository<Material> materialRepository, IRepository<EstoqueMaterial> estoqueMaterialRepository,
            IRepository<RequisicaoMaterial> requisicaoMaterialRepository, IRepository<CentroCusto> centroCustoRepository,
            IRepository<Pessoa> pessoaRepository)
        {
            _saidaMaterialRepository = saidaMaterialRepository;
            _depositoMaterialRepository = depositoMaterialRepository;
            _materialRepository = materialRepository;
            _estoqueMaterialRepository = estoqueMaterialRepository;
            _requisicaoMaterialRepository = requisicaoMaterialRepository;
            _centroCustoRepository = centroCustoRepository;
            _pessoaRepository = pessoaRepository;
            _logger = logger;
        }
        #endregion

        #region Colunas Ordenação
        private static readonly Dictionary<string, string> ColunasOrdenacaoGrid = new Dictionary<string, string>
        {
            {"DataSaida", "DataSaida"},
            {"CentroCusto", "CentroCusto.Nome"},
            {"UnidadeOrigem", "DepositoMaterialOrigem.Unidade.NomeFantasia"},
            {"DepositoMaterialOrigem", "DepositoMaterialOrigem.Nome"},
            {"UnidadeDestino", "DepositoMaterialDestino.Unidade.NomeFantasia"},
            {"DepositoMaterialDestino", "DepositoMaterialDestino.Nome"}
        };

        #endregion

        #region Views

        #region Index
        [PopulateViewData("PopulateViewDataPesquisa")]
        public virtual ActionResult Index()
        {
            return View(new PesquisaSaidaMaterialModel { ModoConsulta = "Listar" });
        }

        private IQueryable<SaidaMaterial> ObtenhaQueryFiltrada(PesquisaSaidaMaterialModel model, StringBuilder filtros)
        {
            var saidaMateriais = _saidaMaterialRepository.Find();

            if (model.Material.HasValue)
            {
                saidaMateriais = saidaMateriais.Where(p => p.SaidaItemMateriais.Any(i => i.Material.Id == model.Material));
                filtros.AppendFormat("Material: {0}, ", _materialRepository.Get(model.Material.Value).Descricao);
            }

            if (model.UnidadeRetirada.HasValue)
            {
                saidaMateriais = saidaMateriais.Where(p => p.DepositoMaterialDestino.Unidade.Id == model.UnidadeRetirada);
                filtros.AppendFormat("Unidade destino: {0}, ", _pessoaRepository.Get(model.UnidadeRetirada.Value).NomeFantasia);
            }

            if (model.DepositoMaterialRetirada.HasValue)
            {
                saidaMateriais = saidaMateriais.Where(p => p.DepositoMaterialDestino.Id == model.DepositoMaterialRetirada);
                filtros.AppendFormat("Depósito destino: {0}, ", _depositoMaterialRepository.Get(model.DepositoMaterialRetirada.Value).Nome);
            }

            if (model.CentroCusto.HasValue)
            {
                saidaMateriais = saidaMateriais.Where(p => p.CentroCusto.Id == model.CentroCusto);
                filtros.AppendFormat("Centro de Custo: {0}, ", _centroCustoRepository.Get(model.CentroCusto.Value).Nome);
            }
            
            if (model.DataSaidaDe.HasValue && model.DataSaidaAte.HasValue)
            {
                saidaMateriais = saidaMateriais.Where(p => p.DataSaida.Date >= model.DataSaidaDe.Value
                                                         && p.DataSaida.Date <= model.DataSaidaAte.Value);
                
                filtros.AppendFormat("Data compra de '{0}' até '{1}', ",
                                     model.DataSaidaDe.Value.ToString("dd/MM/yyyy"),
                                     model.DataSaidaAte.Value.ToString("dd/MM/yyyy"));
            }

            return saidaMateriais;
        }

        public virtual ActionResult ObtenhaListaGridModel([DataSourceRequest] DataSourceRequest request, PesquisaSaidaMaterialModel model)
        {
            try
            {
                var saidaMaterials = ObtenhaQueryFiltrada(model, new StringBuilder());

                if (!request.Sorts.IsNullOrEmpty())
                {
                    foreach (SortDescriptor sortDescriptor in request.Sorts)
                    {
                        saidaMaterials = sortDescriptor.SortDirection.ToString() == "Descending"
                            ? saidaMaterials.OrderByDescending(ColunasOrdenacaoGrid[sortDescriptor.Member])
                            : saidaMaterials.OrderBy(ColunasOrdenacaoGrid[sortDescriptor.Member]);
                    }
                }

                saidaMaterials = saidaMaterials.OrderByDescending(o => o.DataAlteracao);

                var total = saidaMaterials.Count();

                if (request.Page > 0)
                {
                    saidaMaterials = saidaMaterials.Skip((request.Page - 1) * request.PageSize);
                }

                var resultado = saidaMaterials.Take(request.PageSize).ToList();

                var list = resultado.Select(p => new GridSaidaMaterialModel
                {
                    DataSaida = p.DataSaida,
                    CentroCusto = p.CentroCusto != null ? p.CentroCusto.Nome : "",
                    UnidadeOrigem = p.DepositoMaterialOrigem != null ? p.DepositoMaterialOrigem.Unidade.NomeFantasia : "",
                    DepositoMaterialOrigem = p.DepositoMaterialOrigem != null ? p.DepositoMaterialOrigem.Nome : "",
                    UnidadeDestino = p.DepositoMaterialDestino != null ? p.DepositoMaterialDestino.Unidade.NomeFantasia : "",
                    DepositoMaterialDestino = p.DepositoMaterialDestino!= null? p.DepositoMaterialDestino.Nome : ""
                }).ToList();

                var valorPage = request.Page;
                request.Page = 1;
                var data = list.ToDataSourceResult(request);
                request.Page = valorPage;

                var result = new DataSourceResult()
                {
                    AggregateResults = data.AggregateResults,
                    Data = data.Data,
                    Total = total
                };

                return Json(result);
            }
            catch (Exception ex)
            {
                var message = ex.GetMessage();
                _logger.Info(message);

                return Json(new DataSourceResult { Errors = message });
            }
        }

        #endregion
        
        #region Novo

		[PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo()
        {
            var model = new SaidaMaterialModel { DataSaida = DateTime.Now };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(SaidaMaterialModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<SaidaMaterial>(model);

                    // Itens da entrada
                    for (int i = 0; i < model.Materiais.Count; i++)
                    {
                        var idx = i;

                        var item = new SaidaItemMaterial
                        {
                            Material = _materialRepository.Load(model.Materiais[idx]),
                            MovimentacaoEstoqueMaterial = new MovimentacaoEstoqueMaterial
                            {
                                TipoMovimentacaoEstoqueMaterial = TipoMovimentacaoEstoqueMaterial.Saida,
                                Data = model.DataSaida,
                                Quantidade = model.Quantidades[idx]
                            }
                        };

                        domain.AddSaidaItemMaterial(item);

                        // Remover cada item do estoque
                        item.MovimentacaoEstoqueMaterial.EstoqueMaterial = EstoqueMaterial.AtualizarEstoque(_estoqueMaterialRepository,
                            domain.DepositoMaterialOrigem, item.Material, item.MovimentacaoEstoqueMaterial.Quantidade * -1);
                    }

                    _saidaMaterialRepository.Save(domain);

                    this.AddSuccessMessage("Saída de mercadoria cadastrada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar a saída de mercadoria. Confira se os dados foram informados corretamente: " + exception.Message);
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
            var domain = _saidaMaterialRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<SaidaMaterialModel>(domain);
                model.PermiteAlterar = PermiteAlterar(id);
                model.UnidadeOrigem = _depositoMaterialRepository.Get(model.DepositoMaterialOrigem).Unidade.Id;

                if (model.DepositoMaterialDestino.HasValue)
                    model.UnidadeDestino = _depositoMaterialRepository.Get(model.DepositoMaterialDestino).Unidade.Id;

                foreach (var item in domain.SaidaItemMateriais)
                {
                    model.SaidaItemMateriais.Add(item.Id);
                    model.Materiais.Add(item.Material.Id.GetValueOrDefault());
                    model.Quantidades.Add(item.MovimentacaoEstoqueMaterial.Quantidade);
                }

                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a saída de mercadoria.");
            return RedirectToAction("Index");
        }

        private bool PermiteAlterar(long? id)
        {
            var requisicaoMaterial =
            _requisicaoMaterialRepository.Get(y => y.SaidaMaterials.Any(x => x.Id == id));
            if (requisicaoMaterial != null)
            {
                return false;
            }

            return true;
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(SaidaMaterialModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _saidaMaterialRepository.Evict(_saidaMaterialRepository.Get(model.Id));
                    var saida = _saidaMaterialRepository.Get(model.Id);
                    
                    var domain = Mapper.Unflat(model, saida);

                    ExcluirSaidaItemMaterial(model, domain);
                    
                    for (int i = 0; i < model.Materiais.Count; i++)
                    {
                        var idx = i;

                        var materialId = model.Materiais[idx];
                        var material = _materialRepository.Get(materialId);
                        var saidaItemMaterialId = model.SaidaItemMateriais[idx];
                        var quantidade = model.Quantidades[idx];

                        if (saidaItemMaterialId == null)
                        {
                            domain.CrieSaidaItemMaterial(_estoqueMaterialRepository, material, quantidade);
                        }
                    }

                    _saidaMaterialRepository.Update(domain);

                    this.AddSuccessMessage("Saída de mercadoria atualizada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a saída de mercadoria. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }

        private void ExcluirSaidaItemMaterial(SaidaMaterialModel model, SaidaMaterial saida)
        {
            IList<SaidaItemMaterial> itensRemover = new List<SaidaItemMaterial>();

            saida.SaidaItemMateriais.ForEach(saidaItemMaterial =>
            {
                if (model.SaidaItemMateriais.Contains(saidaItemMaterial.Id) == false)
                {
                    itensRemover.Add(saidaItemMaterial);
                    EstoqueMaterial.AtualizarEstoque(_estoqueMaterialRepository, saida.DepositoMaterialOrigem,
                        saidaItemMaterial.Material, saidaItemMaterial.MovimentacaoEstoqueMaterial.Quantidade);
                }
            });

            itensRemover.ForEach(saidaItemMaterial => saida.RemoveSaidaItemMaterial(saidaItemMaterial));
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
					var domain = _saidaMaterialRepository.Get(id);

                    // Atualizar o estoque
                    foreach (var item in domain.SaidaItemMateriais)
                    {
                        EstoqueMaterial.AtualizarEstoque(_estoqueMaterialRepository,
                            domain.DepositoMaterialOrigem, item.Material, item.MovimentacaoEstoqueMaterial.Quantidade);
                    }

					_saidaMaterialRepository.Delete(domain);

                    this.AddSuccessMessage("Saída de mercadoria excluída com sucesso");
					return RedirectToAction("Index");
				}
				catch (Exception exception)
				{
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao excluir a saída de mercadoria: " + exception.Message);
					_logger.Info(exception.GetMessage());
				}
			}

			return RedirectToAction("Editar", new { id });
        }
        #endregion
		
        #endregion

		#region Métodos
		
        #region PopulateViewData
        protected void PopulateViewData(SaidaMaterialModel model)
        {
            // Unidade - Destino
            var unidadeDestinos = _depositoMaterialRepository.Find(p => p.Ativo)
                .Select(d => d.Unidade).OrderBy(o => o.Nome).Where(u => u.Unidade.Ativo).Distinct().ToList();
            ViewData["UnidadeDestino"] = unidadeDestinos.ToSelectList("NomeFantasia", model.UnidadeDestino);

            // Depósito de material - Destino
            if (model.UnidadeDestino.HasValue)
                ViewData["DepositoMaterialDestino"] = _depositoMaterialRepository.Find(d => d.Unidade.Id == model.UnidadeDestino).ToList().ToSelectList("Nome", model.DepositoMaterialDestino);
            else
                ViewData["DepositoMaterialDestino"] = new List<DepositoMaterial>().ToSelectList("Nome", model.DepositoMaterialDestino);

            // Unidade - Origem
            ViewData["UnidadeOrigem"] = unidadeDestinos.ToSelectList("NomeFantasia", model.UnidadeOrigem);

            // Depósito de material - Origem
            if (model.UnidadeOrigem.HasValue)
                ViewData["DepositoMaterialOrigem"] = _depositoMaterialRepository.Find(d => d.Unidade.Id == model.UnidadeOrigem).ToList().ToSelectList("Nome", model.DepositoMaterialOrigem);
            else
                ViewData["DepositoMaterialOrigem"] = new List<DepositoMaterial>().ToSelectList("Nome");

            // Materiais
            var materiais = _materialRepository.Find().ToList();
            ViewBag.MaterialReferenciasDicionario = materiais.Select(c => new { Id = c.Id.GetValueOrDefault(), c.Referencia })
                                                               .ToDictionary(k => k.Id, v => v.Referencia);
            ViewBag.MaterialDescricoesDicionario = materiais.Select(c => new { Id = c.Id.GetValueOrDefault(), c.Descricao })
                                                               .ToDictionary(k => k.Id, v => v.Descricao);
            ViewBag.MaterialUnidadesMedidaDicionario = materiais.Select(c => new { Id = c.Id.GetValueOrDefault(), c.UnidadeMedida.Sigla })
                                                               .ToDictionary(k => k.Id, v => v.Sigla);
        }

        #region PopulateViewDataPesquisa
        protected void PopulateViewDataPesquisa(PesquisaSaidaMaterialModel model)
        {
            var unidadeDestinos = _depositoMaterialRepository.Find(p => p.Ativo)
                .Select(d => d.Unidade).OrderBy(o => o.Nome).Where(u => u.Unidade.Ativo).Distinct().ToList();
            ViewData["UnidadeRetirada"] = unidadeDestinos.ToSelectList("NomeFantasia", model.UnidadeRetirada);

            ViewData["DepositoMaterialRetirada"] = new List<DepositoMaterial>().ToSelectList("Nome");

            var centroCustos = _centroCustoRepository.Find().OrderBy(o => o.Nome).ToList();
            ViewData["CentroCusto"] = centroCustos.ToSelectList("Nome", model.CentroCusto);
        }
        #endregion

        #endregion

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var saidaMaterial = (SaidaMaterialModel)model;

            if (saidaMaterial.Materiais.IsNullOrEmpty())
                ModelState.AddModelError("", "Adicione pelo menos um item à saída de mercadoria.");
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