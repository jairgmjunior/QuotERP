using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.Almoxarifado.Models;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;
using Fashion.ERP.Domain.Almoxarifado;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Controllers
{
    public partial class EntradaMaterialController : BaseController
    {
		#region Variaveis
        private readonly IRepository<EntradaMaterial> _entradaMaterialRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<DepositoMaterial> _depositoMaterialRepository;
        private readonly IRepository<UnidadeMedida> _unidadeMedidaRepository;
        private readonly IRepository<Material> _materialRepository;
        private readonly IRepository<EstoqueMaterial> _estoqueMaterialRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public EntradaMaterialController(ILogger logger, IRepository<EntradaMaterial> entradaMaterialRepository,
            IRepository<Pessoa> pessoaRepository, IRepository<DepositoMaterial> depositoMaterialRepository,
            IRepository<UnidadeMedida> unidadeMedidaRepository, IRepository<Material> materialRepository,
            IRepository<EstoqueMaterial> estoqueMaterialRepository)
        {
            _entradaMaterialRepository = entradaMaterialRepository;
            _pessoaRepository = pessoaRepository;
            _depositoMaterialRepository = depositoMaterialRepository;
            _unidadeMedidaRepository = unidadeMedidaRepository;
            _materialRepository = materialRepository;
            _estoqueMaterialRepository = estoqueMaterialRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var entradaMaterials = _entradaMaterialRepository.Find();

            var list = entradaMaterials.Select(p => new GridEntradaMaterialModel
            {
                Id = p.Id.GetValueOrDefault(),
                DataEntrada = p.DataEntrada,
                UnidadeDestino = p.DepositoMaterialDestino.Unidade.NomeFantasia,
                DepositoMaterialDestino = p.DepositoMaterialDestino.Nome,
                OrigemFornecedor = p.DepositoMaterialOrigem != null ? p.DepositoMaterialOrigem.Nome : p.Fornecedor.Nome,
            }).ToList();

            return View(list);
        }
        #endregion

        #region Novo
        
        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo()
        {
            var model = new EntradaMaterialModel {DataEntrada = DateTime.Now};

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(EntradaMaterialModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<EntradaMaterial>(model);

                    // Itens da entrada
                    for (int i = 0; i < model.Materiais.Count; i++)
                    {
                        var idx = i;

                        var unidadeMedida = _unidadeMedidaRepository.Get(model.UnidadeMedidas[idx]);

                        var item = new EntradaItemMaterial
                        {
                            Material = _materialRepository.Load(model.Materiais[idx]),
                            QuantidadeCompra = model.QuantidadeCompras[idx],
                            UnidadeMedidaCompra = unidadeMedida,
                            MovimentacaoEstoqueMaterial = new MovimentacaoEstoqueMaterial
                            {
                                Quantidade = model.Quantidades[idx],
                                Data = DateTime.Now,
                                TipoMovimentacaoEstoqueMaterial = TipoMovimentacaoEstoqueMaterial.Entrada
                            }
                        };

                        domain.AddEntradaItemMaterial(item);

                        // Incluir cada item ao estoque
                        item.MovimentacaoEstoqueMaterial.EstoqueMaterial = EstoqueMaterial.AtualizarEstoque(_estoqueMaterialRepository,
                            domain.DepositoMaterialDestino, item.Material, item.MovimentacaoEstoqueMaterial.Quantidade);
                    }
                    
                    _entradaMaterialRepository.Save(domain);

                    this.AddSuccessMessage("Entrada de mercadoria cadastrada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar a entrada de mercadoria. Confira se os dados foram informados corretamente: " + exception.Message);
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
            var domain = _entradaMaterialRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<EntradaMaterialModel>(domain);

                model.UnidadeOrigem = model.DepositoMaterialOrigem.HasValue ? _depositoMaterialRepository.Get(model.DepositoMaterialOrigem).Unidade.Id : null;
                model.UnidadeDestino = _depositoMaterialRepository.Get(model.DepositoMaterialDestino).Unidade.Id;

                foreach (var item in domain.EntradaItemMateriais)
                {
                    model.EntradaItemMateriais.Add(item.Id);
                    model.Materiais.Add(item.Material.Id.GetValueOrDefault());
                    model.UnidadeMedidas.Add(item.UnidadeMedidaCompra.Id.GetValueOrDefault());
                    model.QuantidadeCompras.Add(item.QuantidadeCompra);
                    model.Quantidades.Add(item.MovimentacaoEstoqueMaterial.Quantidade);
                }

                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a entrada de mercadoria.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(EntradaMaterialModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _entradaMaterialRepository.Evict(_entradaMaterialRepository.Get(model.Id));
                    var entrada = _entradaMaterialRepository.Get(model.Id);
                    var depositoAntigo = entrada.DepositoMaterialDestino;

                    var domain = Mapper.Unflat(model, entrada);

                    // Remover os itens antigos do estoque
                    var entradaItens = domain.EntradaItemMateriais.ToList();
                    foreach (var item in entradaItens)
                    {
                        
                        // Se o depósito atual for diferente do antigo, remover todos os itens do antigo
                        if (depositoAntigo != domain.DepositoMaterialDestino)
                        {
                            item.MovimentacaoEstoqueMaterial.EstoqueMaterial = EstoqueMaterial.AtualizarEstoque(_estoqueMaterialRepository,
                               depositoAntigo, item.Material, item.MovimentacaoEstoqueMaterial.Quantidade * -1);
                        }
                        // Remover apenas se o item foi excluído
                        else if (model.EntradaItemMateriais.Contains(item.Id) == false)
                        {
                            item.MovimentacaoEstoqueMaterial.EstoqueMaterial = EstoqueMaterial.AtualizarEstoque(_estoqueMaterialRepository,
                                domain.DepositoMaterialDestino, item.Material, item.MovimentacaoEstoqueMaterial.Quantidade * -1);

                            domain.RemoveEntradaItemMaterial(item);
                        }
                    }

                    // Itens da entrada
                    for (int i = 0; i < model.Materiais.Count; i++)
                    {
                        var idx = i;

                        // Adicionar apenas se o item é novo
                        // ou o depósito é diferente do atual
                        if (model.EntradaItemMateriais[idx].HasValue == false
                            || depositoAntigo != domain.DepositoMaterialDestino)
                        {
                            var unidadeMedida = _unidadeMedidaRepository.Get(model.UnidadeMedidas[idx]);

                            var item = new EntradaItemMaterial
                            {
                                Material = _materialRepository.Load(model.Materiais[idx]),
                                QuantidadeCompra = model.QuantidadeCompras[idx],
                                UnidadeMedidaCompra = unidadeMedida,
                                MovimentacaoEstoqueMaterial = new MovimentacaoEstoqueMaterial()
                                {
                                    TipoMovimentacaoEstoqueMaterial = TipoMovimentacaoEstoqueMaterial.Entrada,
                                    Quantidade = model.Quantidades[idx],
                                    Data = DateTime.Now
                                }
                            };

                            domain.AddEntradaItemMaterial(item);

                            // Incluir cada item ao estoque
                            item.MovimentacaoEstoqueMaterial.EstoqueMaterial = EstoqueMaterial.AtualizarEstoque(_estoqueMaterialRepository,
                                domain.DepositoMaterialDestino, item.Material, item.MovimentacaoEstoqueMaterial.Quantidade);
                        }
                    }

                    _entradaMaterialRepository.Update(domain);

                    this.AddSuccessMessage("Entrada de mercadoria atualizada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a entrada de mercadoria. Confira se os dados foram informados corretamente: " + exception.Message);
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
					var domain = _entradaMaterialRepository.Get(id);

                    // Atualizar o estoque
                    foreach (var item in domain.EntradaItemMateriais)
                    {
                        EstoqueMaterial.AtualizarEstoque(_estoqueMaterialRepository,
                            domain.DepositoMaterialDestino, item.Material, item.MovimentacaoEstoqueMaterial.Quantidade * -1);
                    }

				    _entradaMaterialRepository.Delete(domain);

                    this.AddSuccessMessage("Entrada de mercadoria excluída com sucesso");
					return RedirectToAction("Index");
				}
				catch (Exception exception)
				{
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao excluir a entrada de mercadoria: " + exception.Message);
					_logger.Info(exception.GetMessage());
				}
			}

			return RedirectToAction("Editar", new { id });
        }
        #endregion
		
        #endregion

		#region Métodos

        #region PopulateViewData
        protected void PopulateViewData(EntradaMaterialModel model)
        {
            var unidadeDestinos = _depositoMaterialRepository.Find(p => p.Ativo)
                .Select(d => d.Unidade).OrderBy(o => o.Nome).Where(u => u.Unidade.Ativo).Distinct().ToList();
            ViewData["UnidadeDestino"] = unidadeDestinos.ToSelectList("NomeFantasia", model.UnidadeDestino);

            if (model.UnidadeDestino.HasValue)
                ViewData["DepositoMaterialDestino"] = _depositoMaterialRepository.Find(d => d.Unidade.Id == model.UnidadeDestino).ToList().ToSelectList("Nome", model.DepositoMaterialDestino);
            else
                ViewData["DepositoMaterialDestino"] = new List<DepositoMaterial>().ToSelectList("Nome");

            ViewData["UnidadeOrigem"] = unidadeDestinos.ToSelectList("NomeFantasia", model.UnidadeOrigem);

            if (model.UnidadeOrigem.HasValue)
                ViewData["DepositoMaterialOrigem"] = _depositoMaterialRepository.Find(d => d.Unidade.Id == model.UnidadeOrigem).ToList().ToSelectList("Nome", model.DepositoMaterialOrigem);
            else
                ViewData["DepositoMaterialOrigem"] = new List<DepositoMaterial>().ToSelectList("Nome");
                
            
            var unidadeMedidas = _unidadeMedidaRepository.Find().OrderBy(p => p.Sigla).ToList();
            var unidadeMediasAtivo = unidadeMedidas.Where(p => p.Ativo).ToList();
            ViewData["UnidadeMedida"] = unidadeMediasAtivo.ToSelectList("Sigla", model.UnidadeDestino);

            // Catálogo de materiais
            var materiais = _materialRepository.Find().ToList();
            ViewBag.MaterialReferenciasDicionario = materiais.Select(c => new { Id = c.Id.GetValueOrDefault(), c.Referencia })
                                                               .ToDictionary(k => k.Id, v => v.Referencia);
            ViewBag.MaterialDescricoesDicionario = materiais.Select(c => new { Id = c.Id.GetValueOrDefault(), c.Descricao })
                                                               .ToDictionary(k => k.Id, v => v.Descricao);

            var unidadeMedidasSigla = unidadeMedidas.Select(c => new { Id = c.Id.GetValueOrDefault(), c.Sigla }).ToDictionary(k => k.Id, v => v.Sigla);
            ViewBag.UnidadeMedidasDicionario = unidadeMedidasSigla;

            var unidadeMedidasFator = unidadeMedidas.Select(c => new { Id = c.Id.GetValueOrDefault(), c.FatorMultiplicativo }).ToDictionary(k => k.Id, v => v.FatorMultiplicativo);
            ViewBag.FatoresDicionario = unidadeMedidasFator;
        }
        #endregion

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var entradaMaterial = (EntradaMaterialModel)model;

            if (entradaMaterial.Materiais.IsNullOrEmpty())
                ModelState.AddModelError("", "Adicione pelo menos um item à entrada de mercadoria.");

            if (entradaMaterial.Fornecedor != null && entradaMaterial.DepositoMaterialOrigem != null)
                ModelState.AddModelError("DepositoMaterialOrigem", "Selecione um fornecedor ou um depósito de origem, não os dois.");
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