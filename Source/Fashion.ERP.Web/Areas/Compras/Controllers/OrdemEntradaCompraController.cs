using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.Compras.Models;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Compras.Controllers
{
    public partial class OrdemEntradaCompraController : BaseController
    {
		#region Variaveis
        private readonly IRepository<OrdemEntradaCompra> _ordemEntradaCompraRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<Material> _materialRepository;
        private readonly IRepository<PedidoCompra> _pedidoCompraRepository;
        private readonly IRepository<ParametroModuloCompra> _parametroModuloCompraRepository;
        private readonly IRepository<ConferenciaEntradaMaterial> _ordemEntradaMaterialRepository;
        private readonly IRepository<PedidoCompraItem> _pedidoCompraItemRepository;
        private readonly IRepository<UnidadeMedida> _unidadeMedidaRepository;
        private readonly bool _necessitaAutorizacao;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public OrdemEntradaCompraController(ILogger logger, IRepository<OrdemEntradaCompra> ordemEntradaCompraRepository,
            IRepository<Pessoa> pessoaRepository, IRepository<Material> materialRepository,
            IRepository<PedidoCompra> pedidoCompraRepository, IRepository<ParametroModuloCompra> parametroModuloCompraRepository,
            IRepository<ConferenciaEntradaMaterial> ordemEntradaMaterialRepository,
            IRepository<PedidoCompraItem> pedidoCompraItemRepository, IRepository<UnidadeMedida> unidadeMedidaRepository)
        {
            _ordemEntradaCompraRepository = ordemEntradaCompraRepository;
            _pessoaRepository = pessoaRepository;
            _materialRepository = materialRepository;
            _pedidoCompraRepository = pedidoCompraRepository;
            _parametroModuloCompraRepository = parametroModuloCompraRepository;
            _ordemEntradaMaterialRepository = ordemEntradaMaterialRepository;
            _pedidoCompraItemRepository = pedidoCompraItemRepository;
            _unidadeMedidaRepository = unidadeMedidaRepository;
            _logger = logger;

            var parametroModuloCompra = parametroModuloCompraRepository.Find().FirstOrDefault();
            _necessitaAutorizacao = parametroModuloCompra != null && parametroModuloCompra.ValidaRecebimentoPedido;
        }
        #endregion

        #region Views

        #region Index
        [PopulateViewData("PopulateViewDataPesquisa")]
        public virtual ActionResult Index()
        {
            var ordemEntradaCompras = _ordemEntradaCompraRepository.Find();

            var model = new PesquisaOrdemEntradaCompraModel();

            model.Grid = ordemEntradaCompras.Select(p => new GridOrdemEntradaCompraModel
            {
                Id = p.Id.GetValueOrDefault(),
                Data = p.Data,
                Fornecedor = p.Fornecedor.Nome,
                Numero = p.Numero,
                Situacao = p.SituacaoOrdemEntradaCompra.EnumToString()
            }).OrderByDescending(o => o.Id).Take(20).ToList();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewDataPesquisa")]
        public virtual ActionResult Index(PesquisaOrdemEntradaCompraModel model)
        {
            var ordemEntradaCompras = _ordemEntradaCompraRepository.Find();

            try
            {
                #region Filtros
                var filtros = new StringBuilder();

                if (model.UnidadeEstocadora.HasValue)
                {
                    ordemEntradaCompras = ordemEntradaCompras.Where(p => p.UnidadeEstocadora.Id == model.UnidadeEstocadora);
                    filtros.AppendFormat("Unidade estocadora: {0}, ", _pessoaRepository.Get(model.UnidadeEstocadora.Value).NomeFantasia);
                }

                if (model.Numero.HasValue)
                {
                    ordemEntradaCompras = ordemEntradaCompras.Where(p => p.Numero == model.Numero.Value);
                    filtros.AppendFormat("Número: {0}, ", model.Numero.Value);
                }

                if (model.Fornecedor.HasValue)
                {
                    ordemEntradaCompras = ordemEntradaCompras.Where(p => p.Fornecedor.Id == model.Fornecedor);
                    filtros.AppendFormat("Fornecedor: {0}, ", _pessoaRepository.Get(model.Fornecedor.Value).Nome);
                }

                if (model.SituacaoOrdemEntradaCompra.HasValue)
                {
                    ordemEntradaCompras = ordemEntradaCompras.Where(p => p.SituacaoOrdemEntradaCompra == model.SituacaoOrdemEntradaCompra);
                    filtros.AppendFormat("Situação: {0}, ", model.SituacaoOrdemEntradaCompra.Value.EnumToString());
                }

                if (model.DataInicial.HasValue && model.DataFinal.HasValue)
                {
                    ordemEntradaCompras = ordemEntradaCompras.Where(p => p.Data.Date >= model.DataInicial.Value
                                          && p.Data.Date <= model.DataFinal.Value);
                    filtros.AppendFormat("Data de '{0}' até '{1}', ",
                                          model.DataInicial.Value.ToString("dd/MM/yyyy"),
                                          model.DataFinal.Value.ToString("dd/MM/yyyy"));
                }

                #endregion

                model.Grid = ordemEntradaCompras.Select(p => new GridOrdemEntradaCompraModel
                {
                    Id = p.Id.GetValueOrDefault(),
                    Fornecedor = p.Fornecedor.Nome,
                    Data = p.Data,
                    Situacao = p.SituacaoOrdemEntradaCompra.EnumToString(),
                    Numero = p.Numero
                }).ToList();

            }
            catch (Exception exception)
            {
                var message = exception.GetMessage();
                _logger.Info(message);

                if (HttpContext.Request.IsAjaxRequest())
                    return Json(new { Error = message });

                ModelState.AddModelError(string.Empty, message);
            }

            return View(model);
        }
        #endregion

        #region Detalhar

        public virtual ActionResult Detalhar(long id)
        {
            var domain = _ordemEntradaCompraRepository.Get(id);

            var model = new DetalharOrdemEntradaCompraModel
            {
                Id = id,
                Numero = domain.Numero.ToString(CultureInfo.InvariantCulture),
                Data = domain.Data.ToString("dd/MM/yyyy"),
                Fornecedor = string.Format("{0} - {1}", domain.Fornecedor.CpfCnpj, domain.Fornecedor.Nome),
                Situacao = domain.SituacaoOrdemEntradaCompra.EnumToString(),
                Comprador = string.Format("{0} - {1}", domain.Comprador.CpfCnpj, domain.Comprador.Nome),
                Observacao = domain.Observacao
            };

            foreach (var item in domain.ConferenciaEntradaMaterial.OrdemEntradaItemMateriais)
            {
                model.PedidoCompras.Add(new GridDetalharPedidoCompra
                {
                    Id = item.Id.GetValueOrDefault(),
                    Numero = item.PedidoCompraItem.PedidoCompra.Numero,
                    Data = item.PedidoCompraItem.PedidoCompra.DataCompra,
                    Referencia = item.PedidoCompraItem.Material.Referencia,
                    Descricao = item.PedidoCompraItem.Material.Descricao,
                    UnidadeMedida = item.PedidoCompraItem.UnidadeMedida.Sigla,
                    Quantidade = item.Quantidade,
                    ValorUnitario = item.PedidoCompraItem.ValorUnitario,
                    ValorTotal = item.Quantidade * item.PedidoCompraItem.ValorUnitario
                });
            }
            
            model.OrdemEntradas.Add(new GridDetalharOrdemEntrada
            {
                Id = domain.ConferenciaEntradaMaterial.Id.GetValueOrDefault(),
                Data = domain.ConferenciaEntradaMaterial.Data,
                Quantidade = domain.ConferenciaEntradaMaterial.OrdemEntradaItemMateriais.Count,
                Situacao = domain.ConferenciaEntradaMaterial.SituacaoOrdemEntrada.EnumToString(),
                Observacao = domain.Observacao
            });

            return View(model);
        }

        #endregion

        #region Novo

        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo()
        {
            var model = new OrdemEntradaCompraModel { Numero = ProximoOrdemEntradaCompraNumero() };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(OrdemEntradaCompraModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<OrdemEntradaCompra>(model);

                    domain.Numero = ProximoOrdemEntradaCompraNumero();
                    domain.Data = DateTime.Now;
                    domain.DataAlteracao = DateTime.Now;
                    domain.SituacaoOrdemEntradaCompra = SituacaoOrdemEntradaCompra.NaoRecebida;

                    // Salvar ConferenciaEntradaMaterial
                    var ordemEntradaMaterial = new ConferenciaEntradaMaterial
                    {
                        Numero = ProximoOrdemEntradaMaterialNumero(),
                        SituacaoOrdemEntrada = SituacaoOrdemEntrada.Aberta,
                        Data = domain.Data,
                        DataAtualizacao = domain.DataAlteracao,
                        Observacao = domain.Observacao,
                        Comprador = domain.Comprador
                    };

                    // Itens do catálogo de material
                    for (int i = 0; i < model.Itens.Count; i++)
                    {
                        var idx = i;

                        if (model.Quantidades[idx] > 0)
                        {

                            // Gravar as quantidades recebidas pelo pedido de compra no atributo quantidadeEntrega e a dataEntrega do PedidoCompraItem
                            var pedidoCompraItem = _pedidoCompraItemRepository.Get(model.Itens[idx]);
                            pedidoCompraItem.QuantidadeEntrega += model.Quantidades[idx];
                            pedidoCompraItem.DataEntrega = DateTime.Now;
                            if (pedidoCompraItem.QuantidadeEntrega < pedidoCompraItem.Quantidade)
                                pedidoCompraItem.SituacaoCompra = SituacaoCompra.AtendidoParcial;
                            else
                                pedidoCompraItem.SituacaoCompra = SituacaoCompra.AtendidoTotal;

                            var item = new ConferenciaEntradaMaterialItem
                            {
                                Quantidade = model.Quantidades[idx],
                                PedidoCompraItem = pedidoCompraItem
                            };

                            ordemEntradaMaterial.AddOrdemEntradaItemMaterial(item);
                        }
                    }

                    // Pedidos de compra
                    var pedidosCompra = model.PedidoCompras.Distinct().ToList();
                    foreach (var idPedidoCompra in pedidosCompra)
                    {
                        // Atualiza o Pedido
                        var pedidoCompra = _pedidoCompraRepository.Get(idPedidoCompra);

                        if (pedidoCompra.PedidoCompraItens.All(p => p.SituacaoCompra == SituacaoCompra.AtendidoTotal))
                            pedidoCompra.SituacaoCompra = SituacaoCompra.AtendidoTotal;
                        else
                            pedidoCompra.SituacaoCompra = SituacaoCompra.AtendidoParcial;

                        _pedidoCompraRepository.Update(pedidoCompra);

                        // Adiciona à Ordem
                        domain.AddPedidoCompra(pedidoCompra);
                    }

                    ordemEntradaMaterial = _ordemEntradaMaterialRepository.Save(ordemEntradaMaterial);
                    domain.ConferenciaEntradaMaterial = ordemEntradaMaterial;

                    // Salva a ordem de entrada
                    _ordemEntradaCompraRepository.Save(domain);

                    this.AddSuccessMessage("Recebimento de compra cadastrado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar o recebimento de compra. Confira se os dados foram informados corretamente: " + exception.Message);
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
            var domain = _ordemEntradaCompraRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<OrdemEntradaCompraModel>(domain);

                foreach (var item in domain.ConferenciaEntradaMaterial.OrdemEntradaItemMateriais)
                {
                    model.Itens.Add(item.PedidoCompraItem.Id.GetValueOrDefault());
                    model.PedidoCompras.Add(item.PedidoCompraItem.PedidoCompra.Id.GetValueOrDefault());
                    model.Datas.Add(item.PedidoCompraItem.PedidoCompra.DataCompra);
                    model.Materiais.Add(item.PedidoCompraItem.Material.Id.GetValueOrDefault());
                    model.UnidadeMedidas.Add(item.PedidoCompraItem.UnidadeMedida.Id.GetValueOrDefault());
                    model.Quantidades.Add(item.Quantidade);
                    model.ValorUnitarios.Add(item.PedidoCompraItem.ValorUnitario);
                    model.ValorTotais.Add(item.PedidoCompraItem.ValorUnitario * item.Quantidade);
                }

                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o recebimento de compra.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(OrdemEntradaCompraModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _ordemEntradaCompraRepository.Get(model.Id));

                    var oldOrdemEntradaItemMaterial = domain.ConferenciaEntradaMaterial.OrdemEntradaItemMateriais.ToList();

                    domain.DataAlteracao = DateTime.Now;

                    // Alterar ConferenciaEntradaMaterial
                    domain.ConferenciaEntradaMaterial.DataAtualizacao = domain.DataAlteracao;
                    domain.ConferenciaEntradaMaterial.Observacao = domain.Observacao;
                    domain.ConferenciaEntradaMaterial.Comprador = domain.Comprador;
                    domain.ConferenciaEntradaMaterial.ClearOrdemEntradaItemMaterial();

                    // Remove a quantidade dos itens que não estão na lista
                    var oldItens = oldOrdemEntradaItemMaterial
                        .Where(p => model.Itens.Any(i => i == p.PedidoCompraItem.Id.GetValueOrDefault()) == false);
                    foreach (var item in oldItens)
                    {
                        var pedidoCompraItem = _pedidoCompraItemRepository.Get(item.PedidoCompraItem.Id);
                        pedidoCompraItem.QuantidadeEntrega -= item.Quantidade;

                        var pedidoCompra = pedidoCompraItem.PedidoCompra;

                        if (pedidoCompra.PedidoCompraItens.All(p => p.SituacaoCompra == SituacaoCompra.AtendidoTotal))
                            pedidoCompra.SituacaoCompra = SituacaoCompra.AtendidoTotal;
                        else
                            pedidoCompra.SituacaoCompra = SituacaoCompra.AtendidoParcial;
                    }

                    // Itens do catálogo de material
                    for (int i = 0; i < model.Itens.Count; i++)
                    {
                        var idx = i;

                        if (model.Quantidades[idx] > 0)
                        {

                            var oldPedidoCompraItem = oldOrdemEntradaItemMaterial
                                .FirstOrDefault(p => p.PedidoCompraItem.Id == model.Itens[idx]);
                            var qtdAntiga =  oldPedidoCompraItem != null ? oldPedidoCompraItem.Quantidade : 0;

                            // Gravar as quantidades recebidas pelo pedido de compra no atributo quantidadeEntrega e a dataEntrega do PedidoCompraItem
                            var pedidoCompraItem = _pedidoCompraItemRepository.Get(model.Itens[idx]);
                            pedidoCompraItem.QuantidadeEntrega += model.Quantidades[idx] - qtdAntiga;
                            pedidoCompraItem.DataEntrega = DateTime.Now;
                            if (pedidoCompraItem.QuantidadeEntrega < pedidoCompraItem.Quantidade)
                                pedidoCompraItem.SituacaoCompra = SituacaoCompra.AtendidoParcial;
                            else
                                pedidoCompraItem.SituacaoCompra = SituacaoCompra.AtendidoTotal;

                            var item = new ConferenciaEntradaMaterialItem
                            {
                                Quantidade = model.Quantidades[idx],
                                PedidoCompraItem = pedidoCompraItem
                            };

                            domain.ConferenciaEntradaMaterial.AddOrdemEntradaItemMaterial(item);
                        }

                    }

                    // Pedidos de compra
                    var pedidosCompra = model.PedidoCompras.Distinct().ToList();
                    foreach (var idPedidoCompra in pedidosCompra)
                    {
                        // Atualiza o Pedido
                        var pedidoCompra = _pedidoCompraRepository.Get(idPedidoCompra);

                        if (pedidoCompra.PedidoCompraItens.All(p => p.SituacaoCompra == SituacaoCompra.AtendidoTotal))
                            pedidoCompra.SituacaoCompra = SituacaoCompra.AtendidoTotal;
                        else
                            pedidoCompra.SituacaoCompra = SituacaoCompra.AtendidoParcial;

                        _pedidoCompraRepository.Update(pedidoCompra);

                        // Adiciona à Ordem
                        domain.AddPedidoCompra(pedidoCompra);
                    }

                    // Salva a ordem de entrada
                    _ordemEntradaCompraRepository.Update(domain);

                    this.AddSuccessMessage("Recebimento de compra atualizado com sucesso.");
                    return RedirectToAction("Detalhar", new { id = model.Id });
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o recebimento de compra. Confira se os dados foram informados corretamente: " + exception.Message);
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
					var domain = _ordemEntradaCompraRepository.Get(id);

                    foreach (var item in domain.ConferenciaEntradaMaterial.OrdemEntradaItemMateriais)
				    {
                        var pedidoCompraItem = _pedidoCompraItemRepository.Get(item.PedidoCompraItem.Id);
                        pedidoCompraItem.QuantidadeEntrega -= item.Quantidade;
				    }

				    var pedidoCompras = domain.ConferenciaEntradaMaterial.OrdemEntradaItemMateriais
                        .Select(i => i.PedidoCompraItem.PedidoCompra.Id.GetValueOrDefault()).Distinct();
                    foreach (var pedidoCompraId in pedidoCompras)
				    {
                        var pedidoCompra = _pedidoCompraRepository.Get(pedidoCompraId);

                        if (pedidoCompra.PedidoCompraItens.All(p => p.SituacaoCompra == SituacaoCompra.AtendidoTotal))
                            pedidoCompra.SituacaoCompra = SituacaoCompra.AtendidoTotal;
                        else
                            pedidoCompra.SituacaoCompra = SituacaoCompra.AtendidoParcial;
				    }

					_ordemEntradaCompraRepository.Delete(domain);
                    _ordemEntradaMaterialRepository.Delete(domain.ConferenciaEntradaMaterial);

                    this.AddSuccessMessage("Recebimento de compra excluído com sucesso");
					return RedirectToAction("Index");
				}
				catch (Exception exception)
				{
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao excluir o recebimento de compra: " + exception.Message);
					_logger.Info(exception.GetMessage());
				}
			}

			return RedirectToAction("Editar", new { id });
        }
        #endregion

        #region Selecionar pedido de compra

        #region SelecionarPedidoCompra
        [ChildActionOnly, OutputCache(Duration = 3600)]
        public virtual ActionResult SelecionarPedidoCompra()
        {
            return PartialView();
        }
        #endregion

        #region SelecionarPedidoCompraFiltro
        [HttpPost, AjaxOnly]
        public virtual ActionResult SelecionarPedidoCompraFiltro(SelecionarPedidoCompraModel model)
        {
            var filters = model.Filtrar(_necessitaAutorizacao);

            var pedidos = _pedidoCompraRepository.Find(filters.ToArray()).ToList();
                
            var list = pedidos.Select(p => new GridPedidoCompraModel
            {
                Id = p.Id.GetValueOrDefault(),
                Comprador = p.Comprador.Nome,
                DataCompra = p.DataCompra,
                DataEntrega = p.DataEntrega,
                Fornecedor = p.Fornecedor.Nome,
                Numero = p.Numero,
                ValorCompra = p.ValorCompra
            }).ToList();
            
            return Json(list, "application/json", null);
        }
        #endregion

        #region SelecionarPedidoCompraNumero
        [HttpGet, AjaxOnly]
        public virtual ActionResult SelecionarPedidoCompraNumero(long? numero)
        {
            if (numero.HasValue)
            {
                var query = _pedidoCompraRepository.Find(p => p.Numero == numero.Value);

                if (_necessitaAutorizacao)
                    query = query.Where(p => p.Autorizado);

                var pedidoCompra = query.FirstOrDefault();

                if (pedidoCompra != null)
                    return Json(new { pedidoCompra.Id, pedidoCompra.Numero }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { erro = "Nenhum pedido de compra encontrado encontrado." }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion

        #region PesquisarItemPedidoCompra
        [HttpGet, AjaxOnly]
        public virtual ActionResult PesquisarItemPedidoCompra(long numero)
        {
            var query = _pedidoCompraRepository.Find(p => p.Numero == numero);

            if (_necessitaAutorizacao)
                query = query.Where(p => p.Autorizado);

            var pedidoCompra = query.FirstOrDefault();

            if (pedidoCompra != null)
            {
                try
                {
                    var itens = from p in pedidoCompra.PedidoCompraItens
                                select new
                                    {
                                        PedidoId = p.PedidoCompra.Id,
                                        Id = p.Id,
                                        Numero = numero,
                                        Data = p.PedidoCompra.DataCompra,
                                        Material = p.Material.Id,
                                        Referencia = p.Material.Referencia,
                                        Descricao = p.Material.Descricao,
                                        UnidadeMedidaId = p.UnidadeMedida.Id,
                                        UnidadeMedida = p.UnidadeMedida.Sigla,
                                        Quantidade = Math.Max(p.Quantidade - p.QuantidadeEntrega, 0),
                                        ValorUnitario = p.ValorUnitario,
                                        ValorTotal = p.ValorUnitario*p.Quantidade
                                    };
                    return Json(itens, JsonRequestBehavior.AllowGet);
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao adicionar pedido de compra: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return Json(new { erro = "Nenhum pedido de compra encontrado." }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        
        #endregion

		#region Métodos
		
        #region PopulateViewData
        protected void PopulateViewData(OrdemEntradaCompraModel model)
        {
            // UnidadeEstocadora
            var unidades = _pessoaRepository.Find(p => p.Unidade != null && p.Unidade.Ativo).Select(s => new { s.Id, Descricao = s.Unidade.Codigo + " - " + s.NomeFantasia}).ToList();
            ViewBag.UnidadeEstocadora = new SelectList(unidades, "Id", "Descricao", model.UnidadeEstocadora);

            // Catálogo de materiais
            var materiais = _materialRepository.Find();
            ViewBag.MaterialReferenciasDicionario = materiais.Select(c => new { Id = c.Id.GetValueOrDefault(), c.Referencia })
                                                               .ToDictionary(k => k.Id, v => v.Referencia);
            ViewBag.MaterialDescricoesDicionario = materiais.Select(c => new { Id = c.Id.GetValueOrDefault(), c.Descricao })
                                                               .ToDictionary(k => k.Id, v => v.Descricao);
            // Pedido de compras
            var pedidoCompras = _pedidoCompraRepository.Find();
            ViewBag.PedidoComprasDicionario = pedidoCompras.Select(c => new { Id = c.Id.GetValueOrDefault(), c.Numero })
                                                               .ToDictionary(k => k.Id, v => v.Numero);

            // Unidade de medida
            var unidadeMedidas = _unidadeMedidaRepository.Find().OrderBy(p => p.Sigla).ToList();
            ViewBag.UnidadeMedidasDicionario = unidadeMedidas.Select(c => new { Id = c.Id.GetValueOrDefault(), c.Sigla }).ToDictionary(k => k.Id, v => v.Sigla);
        }
        #endregion

        #region PopulateViewDataPesquisa
        protected void PopulateViewDataPesquisa(PesquisaOrdemEntradaCompraModel model)
        {
            // UnidadeEstocadora
            var unidades = _pessoaRepository.Find(p => p.Unidade != null && p.Unidade.Ativo).ToList();
            ViewBag.UnidadeEstocadora = unidades.ToSelectList("NomeFantasia", model.UnidadeEstocadora);
        }
        #endregion

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var ordemEntradaCompra = model as OrdemEntradaCompraModel;
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
        }
        #endregion

        #region ProximoOrdemEntradaCompraNumero
        private long ProximoOrdemEntradaCompraNumero()
        {
            try
            {
                return _ordemEntradaCompraRepository.Find().Max(p => p.Numero) + 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }
        #endregion

        #region ProximoOrdemEntradaMaterialNumero
        private long ProximoOrdemEntradaMaterialNumero()
        {
            try
            {
                return _ordemEntradaMaterialRepository.Find().Max(p => p.Numero) + 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }
        #endregion

        #endregion
    }
}