using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Reporting.Almoxarifado;
using Fashion.ERP.Reporting.Helpers;
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
using NHibernate.Linq;
using Ninject.Extensions.Logging;
using Fashion.ERP.Domain.Almoxarifado;
using Telerik.Reporting;
using Telerik.Reporting.Drawing;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Controllers
{
    public partial class EntradaMaterialController : BaseController
    {
		#region Variaveis
        private readonly IRepository<EntradaMaterial> _entradaMaterialRepository;
        private readonly IRepository<DepositoMaterial> _depositoMaterialRepository;
        private readonly IRepository<UnidadeMedida> _unidadeMedidaRepository;
        private readonly IRepository<Material> _materialRepository;
        private readonly IRepository<EstoqueMaterial> _estoqueMaterialRepository;
        private readonly IRepository<RecebimentoCompra> _recebimentoCompraRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly ILogger _logger;
        private Dictionary<string, string> _colunasPesquisaEntradaMaterial;
        #endregion

        #region Construtores
        public EntradaMaterialController(ILogger logger, IRepository<EntradaMaterial> entradaMaterialRepository,
            IRepository<DepositoMaterial> depositoMaterialRepository,
            IRepository<UnidadeMedida> unidadeMedidaRepository, IRepository<Material> materialRepository,
            IRepository<EstoqueMaterial> estoqueMaterialRepository, IRepository<RecebimentoCompra> recebimentoCompraRepository,
            IRepository<Pessoa> pessoaRepository)
        {
            _entradaMaterialRepository = entradaMaterialRepository;
            _depositoMaterialRepository = depositoMaterialRepository;
            _unidadeMedidaRepository = unidadeMedidaRepository;
            _materialRepository = materialRepository;
            _estoqueMaterialRepository = estoqueMaterialRepository;
            _recebimentoCompraRepository = recebimentoCompraRepository;
            _pessoaRepository = pessoaRepository;
            _logger = logger;

            PreecheColunasPesquisa();
        }
        #endregion

        #region Colunas Ordenação
        private static readonly Dictionary<string, string> ColunasOrdenacaoGrid = new Dictionary<string, string>
        {
            {"DataEntrada", "DataEntrada"},
            {"UnidadeDestino", "DepositoMaterialDestino.Unidade.NomeFantasia"},
            {"DepositoMaterialDestino", "DepositoMaterialDestino.Nome"}
        };
        #endregion

        #region Views

        #region Index
        [PopulateViewData("PopulateViewDataPesquisa")]
        public virtual ActionResult Index()
        {
            var model = new PesquisaEntradaMaterialModel { ModoConsulta = "Listar" };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewDataPesquisa")]
        public virtual ActionResult Index(PesquisaEntradaMaterialModel model)
        {
            try
            {
                #region Filtros
                var filtros = new StringBuilder();

                var entradaMateriais = ObtenhaQueryFiltrada(model, filtros);

                #endregion

                // Verifica se é uma listagem
                if (model.ModoConsulta == "Listar")
                {
                    if (model.OrdenarPor != null)
                    {
                        entradaMateriais = model.OrdenarEm == "asc"
                            ? entradaMateriais.OrderBy(model.OrdenarPor)
                            : entradaMateriais.OrderByDescending(model.OrdenarPor);
                    }
                    else
                    {
                        entradaMateriais = entradaMateriais.OrderByDescending(x => x.DataAlteracao);
                    }

                    model.Grid = entradaMateriais.Select(p => new GridEntradaMaterialModel
                    {
                        Id = p.Id.GetValueOrDefault(),
                        DataEntrada = p.DataEntrada,
                        UnidadeDestino = p.DepositoMaterialDestino.Unidade.NomeFantasia,
                        DepositoMaterialDestino = p.DepositoMaterialDestino.Nome,
                        OrigemFornecedor = p.DepositoMaterialOrigem != null ? p.DepositoMaterialOrigem.Nome : p.Fornecedor.Nome,
                    }).ToList();

                    return View(model);
                }

                // Se não é uma listagem, gerar o relatório
                var result = entradaMateriais
                    .Fetch(p => p.DepositoMaterialDestino).Fetch(p => p.DepositoMaterialOrigem)
                    .ToList();

                if (!result.Any())
                    return Json(new { Error = "Nenhum item encontrado." });

                #region Montar Relatório

                Report report = new ListaEntradaMaterialReport { DataSource = result };

                if (filtros.Length > 2)
                    report.ReportParameters["Filtros"].Value = filtros.ToString().Substring(0, filtros.Length - 2);

                var grupo = report.Groups.First(p => p.Name.Equals("Grupo"));

                if (model.AgruparPor != null)
                {
                    grupo.Groupings.Add("=Fields." + model.AgruparPor);

                    var key = _colunasPesquisaEntradaMaterial.First(p => p.Value == model.AgruparPor).Key;
                    var titulo = string.Format("= \"{0}: \" + Fields.{1}", key, model.AgruparPor);
                    grupo.GroupHeader.GetTextBox("Titulo").Value = titulo;
                }
                else
                {
                    report.Groups.Remove(grupo);
                }

                if (model.OrdenarPor != null)
                    report.Sortings.Add("=Fields." + model.OrdenarPor, model.OrdenarEm == "asc" ? SortDirection.Asc : SortDirection.Desc);
                #endregion

                var filename = report.ToByteStream().SaveFile(".pdf");

                return Json(new { Url = filename });
            }
            catch (Exception exception)
            {
                var message = exception.GetMessage();
                _logger.Info(message);

                if (HttpContext.Request.IsAjaxRequest())
                    return Json(new { Error = message });

                ModelState.AddModelError(string.Empty, message);
                return View(model);
            }
        }

        #endregion

        private IQueryable<EntradaMaterial> ObtenhaQueryFiltrada(PesquisaEntradaMaterialModel model, StringBuilder filtros)
        {
            var entradaMateriais = _entradaMaterialRepository.Find();
            if (!string.IsNullOrWhiteSpace(model.Referencia))
            {
                entradaMateriais = entradaMateriais.Where(p => p.EntradaItemMateriais.Any(i => i.Material.Referencia == model.Referencia));
                filtros.AppendFormat("Referência: {0}, ", model.Referencia);
            }

            if (model.UnidadeDestino.HasValue)
            {
                entradaMateriais = entradaMateriais.Where(p => p.DepositoMaterialDestino.Unidade.Id == model.UnidadeDestino);
                filtros.AppendFormat("Unidade destino: {0}, ", _pessoaRepository.Get(model.UnidadeDestino.Value).NomeFantasia);
            }

            if (model.DepositoMaterialDestino.HasValue)
            {
                entradaMateriais = entradaMateriais.Where(p => p.DepositoMaterialDestino.Id == model.DepositoMaterialDestino);
                filtros.AppendFormat("Depósito destino: {0}, ", _depositoMaterialRepository.Get(model.DepositoMaterialDestino.Value).Nome);
            }

            if (model.Fornecedor.HasValue)
            {
                entradaMateriais = entradaMateriais.Where(p => p.Fornecedor.Id == model.Fornecedor);
                filtros.AppendFormat("Fornecedor: {0}, ", _pessoaRepository.Get(model.Fornecedor.Value).Nome);
            }

            if (model.UnidadeOrigem.HasValue)
            {
                entradaMateriais = entradaMateriais.Where(p => p.DepositoMaterialOrigem.Unidade.Id == model.UnidadeOrigem);
                filtros.AppendFormat("Unidade origem: {0}, ", _pessoaRepository.Get(model.UnidadeOrigem.Value).NomeFantasia);
            }

            if (model.DepositoMaterialOrigem.HasValue)
            {
                entradaMateriais = entradaMateriais.Where(p => p.DepositoMaterialOrigem.Id == model.UnidadeOrigem);
                filtros.AppendFormat("Depósito origem: {0}, ", _depositoMaterialRepository.Get(model.DepositoMaterialOrigem.Value).Nome);
            }

            if (model.DataEntradaDe.HasValue && model.DataEntradaAte.HasValue)
            {
                entradaMateriais = entradaMateriais.Where(p => p.DataEntrada.Date >= model.DataEntradaDe.Value
                                                         && p.DataEntrada.Date <= model.DataEntradaAte.Value);
                filtros.AppendFormat("Data compra de '{0}' até '{1}', ",
                                     model.DataEntradaDe.Value.ToString("dd/MM/yyyy"),
                                     model.DataEntradaAte.Value.ToString("dd/MM/yyyy"));
            }

            if (!string.IsNullOrWhiteSpace(model.ReferenciaExterna))
            {
                entradaMateriais =
                    entradaMateriais.Where(
                        p =>
                            p.EntradaItemMateriais.SelectMany(x => x.Material.ReferenciaExternas)
                                .Any(y => y.Referencia == model.ReferenciaExterna));
                filtros.AppendFormat("Referência externa: {0}, ", model.ReferenciaExterna);
            }

            return entradaMateriais;
        }

        public virtual ActionResult ObtenhaListaGridModel([DataSourceRequest] DataSourceRequest request, PesquisaEntradaMaterialModel model)
        {
            try
            {
                var filtros = new StringBuilder();

                var requisicaoMaterials = ObtenhaQueryFiltrada(model, filtros);

                if (!request.Sorts.IsNullOrEmpty())
                {
                    foreach (SortDescriptor sortDescriptor in request.Sorts)
                    {
                        requisicaoMaterials = sortDescriptor.SortDirection.ToString() == "Descending"
                            ? requisicaoMaterials.OrderByDescending(ColunasOrdenacaoGrid[sortDescriptor.Member])
                            : requisicaoMaterials.OrderBy(ColunasOrdenacaoGrid[sortDescriptor.Member]);
                    }
                }

                requisicaoMaterials = requisicaoMaterials.OrderByDescending(o => o.DataAlteracao);

                var total = requisicaoMaterials.Count();

                if (request.Page > 0)
                {
                    requisicaoMaterials = requisicaoMaterials.Skip((request.Page - 1) * request.PageSize);
                }

                var resultado = requisicaoMaterials.Take(request.PageSize).ToList();

                var list = resultado.Select(p => new GridEntradaMaterialModel
                {
                    Id = p.Id.GetValueOrDefault(),
                    DataEntrada = p.DataEntrada.Date,
                    UnidadeDestino = p.DepositoMaterialDestino.Unidade.NomeFantasia,
                    DepositoMaterialDestino = p.DepositoMaterialDestino.Nome,
                    OrigemFornecedor = p.DepositoMaterialOrigem != null ? p.DepositoMaterialOrigem.Nome : p.Fornecedor.Nome,
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
                return this.Json(new DataSourceResult
                {
                    Errors = ex.GetMessage()
                });
            }
        }

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
                                Data = model.DataEntrada,
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

				    if (_recebimentoCompraRepository.Find(x => x.EntradaMaterial.Id == id).Any())
				    {
				        this.AddErrorMessage("Não é possível excluir uma entrada de material criada automaticamente a partir de um recebimento de compra. " 
                            +" É necessário excluir o recebimento de compra para completar a operação.");
                        return RedirectToAction("Editar", new { id });
				    }

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

            // materiais
            var materiais = _materialRepository.Find().Select(s => new {s.Id, s.Referencia, s.Descricao}).ToList();
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

        #region PopulateViewDataPesquisa
        protected void PopulateViewDataPesquisa(PesquisaEntradaMaterialModel model)
        {
            var unidadeDestinos = _depositoMaterialRepository.Find(p => p.Ativo)
                .Select(d => d.Unidade).OrderBy(o => o.Nome).Where(u => u.Unidade.Ativo).Distinct().ToList();
            ViewData["UnidadeDestino"] = unidadeDestinos.ToSelectList("NomeFantasia", model.UnidadeDestino);
            
            ViewData["DepositoMaterialDestino"] = new List<DepositoMaterial>().ToSelectList("Nome");

            ViewData["UnidadeOrigem"] = unidadeDestinos.ToSelectList("NomeFantasia", model.UnidadeOrigem);
            
            ViewData["DepositoMaterialOrigem"] = new List<DepositoMaterial>().ToSelectList("Nome");

            ViewBag.OrdenarPor = new SelectList(_colunasPesquisaEntradaMaterial, "value", "key");
            ViewBag.AgruparPor = new SelectList(_colunasPesquisaEntradaMaterial, "value", "key");
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

        #region PreecheColunasPesquisa
        private void PreecheColunasPesquisa()
        {
            _colunasPesquisaEntradaMaterial = new Dictionary<string, string>
                           {
                               {"Data entrada", "DataEntrada"},
                               {"Unidade destino", "DepositoMaterialDestino.Unidade.Id"},
                               {"DepositoMaterialDestino", "DepositoMaterialDestino.Id"},
                               {"Fornecedor", "Fornecedor.Id"},
                           };

        }
        #endregion

        #endregion
    }
}