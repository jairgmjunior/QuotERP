using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.Almoxarifado.Models;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;

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

        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public SaidaMaterialController(ILogger logger, IRepository<SaidaMaterial> saidaMaterialRepository,
            IRepository<DepositoMaterial> depositoMaterialRepository, 
            IRepository<Material> materialRepository, IRepository<EstoqueMaterial> estoqueMaterialRepository,
            IRepository<RequisicaoMaterial> requisicaoMaterialRepository)
        {
            _saidaMaterialRepository = saidaMaterialRepository;
            _depositoMaterialRepository = depositoMaterialRepository;
            _materialRepository = materialRepository;
            _estoqueMaterialRepository = estoqueMaterialRepository;
            _requisicaoMaterialRepository = requisicaoMaterialRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var saidaMaterials = _saidaMaterialRepository.Find().OrderByDescending(x => x.DataAlteracao);

            var list = saidaMaterials.Select(p => new GridSaidaMaterialModel
            {
                Id = p.Id.GetValueOrDefault(),
                DataSaida = p.DataSaida,
                CentroCusto = p.CentroCusto.Nome,
                UnidadeOrigem = p.DepositoMaterialOrigem.Unidade.NomeFantasia,
                DepositoMaterialOrigem = p.DepositoMaterialOrigem.Nome,
                UnidadeDestino = p.DepositoMaterialDestino.Unidade.NomeFantasia,
                DepositoMaterialDestino = p.DepositoMaterialDestino.Nome
            }).ToList();

            return View(list);
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

            saida.SaidaItemMateriais.Each(saidaItemMaterial =>
            {
                if (model.SaidaItemMateriais.Contains(saidaItemMaterial.Id) == false)
                {
                    itensRemover.Add(saidaItemMaterial);
                    EstoqueMaterial.AtualizarEstoque(_estoqueMaterialRepository, saida.DepositoMaterialOrigem,
                        saidaItemMaterial.Material, saidaItemMaterial.MovimentacaoEstoqueMaterial.Quantidade);
                }
            });

            itensRemover.Each(saidaItemMaterial => saida.RemoveSaidaItemMaterial(saidaItemMaterial));
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