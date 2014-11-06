using System;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.Compras.Models;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Compras.Controllers
{
    public partial class PedidoCompraCancelamentoController : BaseController
    {
        #region Variaveis

        private readonly IRepository<PedidoCompra> _pedidoCompraRepository;
        private readonly IRepository<PedidoCompraItem> _pedidoCompraItemRepository;
        private readonly IRepository<Material> _materialRepository;
        private readonly IRepository<UnidadeMedida> _unidadeMedidaRepository;
        private readonly IRepository<MotivoCancelamentoPedidoCompra> _motivoCancelamentoPedidoCompraRepository;
        private readonly ILogger _logger;             

        #endregion

        #region Construtores

        public PedidoCompraCancelamentoController(ILogger logger, IRepository<PedidoCompra> pedidoCompraRepository,
                                      IRepository<Material> materialRepository,
                                      IRepository<UnidadeMedida> unidadeMedidaRepository,
                                      IRepository<PedidoCompraItem> pedidoCompraItemRepository,
                                      IRepository<MotivoCancelamentoPedidoCompra> motivoCancelamentoPedidoCompraRepository)
        {
            _pedidoCompraRepository = pedidoCompraRepository;
            _pedidoCompraItemRepository = pedidoCompraItemRepository;
            _materialRepository = materialRepository;
            _unidadeMedidaRepository = unidadeMedidaRepository;
            _motivoCancelamentoPedidoCompraRepository = motivoCancelamentoPedidoCompraRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region CancelamentoPedido

        [ImportModelStateFromTempData, PopulateViewData("PopulateViewDataItemCancelado")]
        public virtual ActionResult CancelamentoPedido(long id)
        {
            var domain = _pedidoCompraRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<PedidoCompraCancelamentoModel>(domain);
                model.SituacaoCompraDescricao = domain.SituacaoCompra.EnumToString();
                model.ValorCompra = domain.ValorCompra;

                model.GridItemCancelado = domain.PedidoCompraItens.Select(p => new GridPedidoCompraItemCanceladoModel()
                {
                    Id = p.Id.GetValueOrDefault(),
                    Referencia = p.Material.Referencia,
                    Descricao = p.Material.Descricao,
                    UND = p.UnidadeMedida.Sigla,
                    Qtde = p.Quantidade,
                    Entregue = p.QuantidadeEntrega,
                    Diferenca = p.ObtenhaDiferenca(),
                    Preco = p.ValorUnitario,
                    Desconto = 0,
                    ValorTotal = p.ObtenhaValorTotal(),
                    SituacaoCompraDescricao = p.SituacaoCompra.EnumToString()
                }).ToList();

                return View("CancelamentoPedido", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o pedido de compra.");
            return RedirectToAction("CancelamentoPedido");
        }

        #endregion

        #region IndexCancelarItem
        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewDataItemCancelado")]
        public virtual ActionResult CancelamentoPedido(PedidoCompraCancelamentoModel model)
        {
           if (ModelState.IsValid)
           {
                try
                {
                    
                    foreach (var modelGrid in model.GridItemCancelado)
                    {
                        var pedidoCompraItemCancelado = new PedidoCompraItemCancelado();

                        if (modelGrid.Check)
                        {
                            var domain = _pedidoCompraItemRepository.Get(modelGrid.Id);
                            
                            pedidoCompraItemCancelado.Observacao = model.ObservacaoCancelamento;

                            pedidoCompraItemCancelado.Data = DateTime.Now;
                            pedidoCompraItemCancelado.MotivoCancelamentoPedidoCompra =
                                _motivoCancelamentoPedidoCompraRepository.Load(model.MotivoCancelamento);
                          
                            pedidoCompraItemCancelado.CalculeQuantidade(domain);

                            domain.AtualizeSituacao();

                            domain.PedidoCompraItemCancelado = pedidoCompraItemCancelado;
                            _pedidoCompraItemRepository.Update(domain);
                        }  
                    }

                    var pedidocompra = _pedidoCompraRepository.Get(model.Id);
                    pedidocompra.AtualizeSituacao();
                    _pedidoCompraRepository.SaveOrUpdate(pedidocompra);
                    
                    this.AddSuccessMessage("Item cancelados com sucesso.");
                    return RedirectToAction("Index", "PedidoCompra");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty,
                        "Não é possível salvar o motivo de cancelamento do pedido. Confira se os dados foram informados corretamente: " +
                        exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }
           
            return View("CancelamentoPedido", model);
        }

        #endregion

        #endregion

        #region PopulateViewData
        protected void PopulateViewDataItemCancelado(PedidoCompraCancelamentoModel model)
        {
            //Motivo Cancelamento
            var motivoCancelamento = _motivoCancelamentoPedidoCompraRepository.Find(p => p.Ativo).OrderBy(p => p.Descricao).ToList();
            ViewBag.MotivoCancelamento = motivoCancelamento.ToSelectList("Descricao", model.MotivoCancelamento);
            
            // Catálogo de materiais
            var catalogoMateriais = _materialRepository.Find();
            ViewBag.CatalogoReferenciasDicionario = catalogoMateriais.Select(c => new { Id = c.Id.GetValueOrDefault(), c.Referencia })
                                                               .ToDictionary(k => k.Id, v => v.Referencia);
            ViewBag.CatalogoDescricaoDicionario = catalogoMateriais.Select(c => new { Id = c.Id.GetValueOrDefault(), c.Descricao })
                                                               .ToDictionary(k => k.Id, v => v.Descricao);

            // Unidade de medida
            var unidadeMedidas = _unidadeMedidaRepository.Find().OrderBy(p => p.Sigla).ToList();
            var unidadeMediasAtivo = unidadeMedidas.Where(p => p.Ativo).ToList();
            ViewData["UnidadeMedida"] = unidadeMediasAtivo.ToSelectList("Sigla");

            var unidadeMedidasSigla = unidadeMedidas.Select(c => new { Id = c.Id.GetValueOrDefault(), c.Sigla }).ToDictionary(k => k.Id, v => v.Sigla);
            ViewBag.UnidadeMedidasDicionario = unidadeMedidasSigla;

            var quantidadeEntrega = _pedidoCompraItemRepository.Find();
            ViewBag.QuantidadeEntregaDicionario = quantidadeEntrega.Select(c => new { Id = c.Id.GetValueOrDefault(), c.QuantidadeEntrega })
                                                              .ToDictionary(k => k.Id, v => v.QuantidadeEntrega);

            // Situacao
            ViewBag.SituacoesDicionario = ((SituacaoCompra[])typeof(SituacaoCompra).GetEnumValues()).ToDictionary(k => k, e => e.EnumToString());
        }
        #endregion
    }
}


