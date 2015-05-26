using System;
using System.Collections.Generic;
using System.Linq;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Web.Areas.Compras.Models;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Repository;
using Kendo.Mvc.Extensions;
using WebGrease.Css.Extensions;

namespace Fashion.ERP.Web.Areas.Compras.Controllers
{
    public class RecebimentoCompraConverter
    {
        private readonly IRepository<PedidoCompra> _pedidoCompraRepository;
        private readonly IRepository<RecebimentoCompra> _recebimentoCompraRepository;
        private readonly IRepository<Material> _materialRepository;
        private readonly IRepository<EntradaMaterial> _entradaMaterialRepository;

        public RecebimentoCompraConverter(
            IRepository<PedidoCompra> pedidoCompraRepository,
            IRepository<RecebimentoCompra> recebimentoCompraRepository,
            IRepository<Material> materialRepository,
            IRepository<EntradaMaterial> entradaMaterialRepository)
        {
            _pedidoCompraRepository = pedidoCompraRepository;
            _recebimentoCompraRepository = recebimentoCompraRepository;
            _materialRepository = materialRepository;
            _entradaMaterialRepository = entradaMaterialRepository;
        }

        #region Crie RecebimentoCompra
        public RecebimentoCompra CrieNovoRecebimentoCompra(RecebimentoCompraModel model, RecebimentoCompraController controller)
        {
            var domain = Mapper.Unflat<RecebimentoCompra>(model);
            domain.Numero = ProximoNumero();
            domain.Data = DateTime.Now;
            domain.DataAlteracao = DateTime.Now;

            CrieListaPedidoCompra(model, domain);
            CrieListaRecebimentoCompraItens(model, domain, controller);
            
            return domain;
        }

        private void CrieListaPedidoCompra(RecebimentoCompraModel recebimentoCompraModel, RecebimentoCompra recebimentoCompra)
        {
            foreach (var pedidoCompraId in from recebimentoCompraItemModel in recebimentoCompraModel.GridItens
                                           from pedidoCompraId in recebimentoCompraItemModel.PedidosCompra
                                           group pedidoCompraId by pedidoCompraId
                                           into pedidos 
                                           select pedidos.Key)
            {
                recebimentoCompra.PedidoCompras.Add(_pedidoCompraRepository.Find(x => x.Numero == pedidoCompraId.Numero).First());
            }
        }

        private void CrieListaRecebimentoCompraItens(RecebimentoCompraModel recebimentoCompraModel, RecebimentoCompra recebimentoCompra, RecebimentoCompraController controller)
        {
            recebimentoCompra.RecebimentoCompraItens = recebimentoCompraModel.GridItens.Select(x => CrieNovoRecebimentoCompraItem(recebimentoCompra, x, controller)).ToList();
        }
        
        private RecebimentoCompraItem CrieNovoRecebimentoCompraItem(RecebimentoCompra recebimentoCompra, RecebimentoCompraItemModel recebimentoCompraItemModel, RecebimentoCompraController controller)
        {
            var item = Mapper.Unflat<RecebimentoCompraItem>(recebimentoCompraItemModel);
            item.Material = _materialRepository.Find(p => p.Referencia == recebimentoCompraItemModel.MaterialReferencia).First();
            var quantidadeEntradaDisponível = recebimentoCompraItemModel.QuantidadeEntrada;

            var pedidosCompraOrdenados = recebimentoCompraItemModel.PedidosCompra.OrderBy(x => x.Numero);
            var ultimoPedidoCompraId = pedidosCompraOrdenados.Last().Id;
            
            foreach (var pedidoCompraId in pedidosCompraOrdenados)
            {
                if (quantidadeEntradaDisponível <= 0)
                {
                    controller.AddInfoMessage("Não foi possível receber o material " + item.Material.Referencia + " do pedido de compra " + pedidoCompraId.Numero 
                        + " pois a quantidade recebida não é suficiente para atender esse item.");
                    continue;
                }

                var pedidoCompra = recebimentoCompra.PedidoCompras.First(p => p.Id == pedidoCompraId.Id);
                var pedidoCompraItem = pedidoCompra.ObtenhaPedidoCompraItem(item.Material.Referencia);
                
                if (pedidoCompraItem == null)
                    throw new Exception("Não foi possível localizar no banco o pedidoCompraItem referente ao material: " +
                                        recebimentoCompraItemModel.MaterialReferencia);
                
                var ehUltimo = ultimoPedidoCompraId == pedidoCompraId.Id;
                var quantidadeDetalhamento = ObtenhaQuantidadeDetalhamento(recebimentoCompra, pedidoCompraItem, ehUltimo, ref quantidadeEntradaDisponível);

                recebimentoCompra.CrieDetalhamentoRecebimentoCompraItem(pedidoCompra, pedidoCompraItem, quantidadeDetalhamento, item);
            }

            return item;
        }

        private double ObtenhaQuantidadeDetalhamento(RecebimentoCompra recebimentoCompra, PedidoCompraItem pedidoCompraItem, bool ehUltimo, ref double quantidadeEntradaDisponível)
        {
            var quantidadeDetalhamento = recebimentoCompra.CalculeQuantidadeDetalhamento(quantidadeEntradaDisponível, pedidoCompraItem.ObtenhaDiferenca(), ehUltimo);
            quantidadeEntradaDisponível = quantidadeEntradaDisponível - quantidadeDetalhamento;
            return quantidadeDetalhamento;
        }

        private long ProximoNumero()
        {
            try
            {
                return _recebimentoCompraRepository.Find().Max(p => p.Numero) + 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }
        #endregion

        #region Atualize RecebimentoCompra
        public RecebimentoCompra AtualizeRecebimentoCompra(RecebimentoCompra domain, RecebimentoCompraModel model, RecebimentoCompraController controller)
        {
            domain = Mapper.Unflat(model, domain);
            domain.DataAlteracao = DateTime.Now;

            AtualizeListaPedidoCompra(model, domain);
            ExcluaRecebimentoCompraItem(model, domain);
            AtualizeListaRecebimentoCompraItens(model, domain, controller);
            domain.AtualizePedidosCompra(_recebimentoCompraRepository, _pedidoCompraRepository);
            
            return domain;
        }

        private void ExcluaRecebimentoCompraItem(RecebimentoCompraModel recebimentoCompraModel,
            RecebimentoCompra recebimentoCompra)
        {
            var modelItensId = recebimentoCompraModel.GridItens.Select(p => p.Id);
            var domainItensId = recebimentoCompra.RecebimentoCompraItens.Select(p => p.Id);

            var idsExcluir = domainItensId.Except(modelItensId).ToList();

            var pedidosCompraAtualizar = new List<PedidoCompra>();
            var recebimentoItensExcluir = recebimentoCompra.RecebimentoCompraItens.
                Where(recebimentoCompraItem => idsExcluir.Contains(recebimentoCompraItem.Id)).ToList();

            foreach (var recebimentoCompraItem in recebimentoItensExcluir)
            {
                recebimentoCompraItem.DetalhamentoRecebimentoCompraItens.ForEach(detalhamento =>
                {
                    recebimentoCompra.DetalhamentoRecebimentoCompraItens.Remove(detalhamento);
                    detalhamento.PedidoCompraItem.QuantidadeEntrega -= detalhamento.Quantidade;
                    detalhamento.PedidoCompraItem.AtualizeSituacao();
                    detalhamento.PedidoCompra.AtualizeSituacao();
                    if (!pedidosCompraAtualizar.Contains(detalhamento.PedidoCompra))
                    {
                        pedidosCompraAtualizar.Add(detalhamento.PedidoCompra);
                    }   
                });
            }

            recebimentoItensExcluir.ForEach(recebimentoCompraItem =>
            {
                recebimentoCompra.RecebimentoCompraItens.Remove
                    (recebimentoCompraItem);
                recebimentoCompraItem.ExcluaEntradaItemMaterial(recebimentoCompra.EntradaMaterial, _entradaMaterialRepository);
            });

            pedidosCompraAtualizar.ForEach(pedido => _pedidoCompraRepository.Update(pedido));
            _recebimentoCompraRepository.Update(recebimentoCompra);
        }

        private void AtualizeListaPedidoCompra(RecebimentoCompraModel recebimentoCompraModel, RecebimentoCompra recebimentoCompra)
        {
            foreach (var pedidoCompraId in from recebimentoCompraItemModel in recebimentoCompraModel.GridItens
                                           from pedidoCompraId in recebimentoCompraItemModel.PedidosCompra
                                           let pedidoExistenteNaLista = recebimentoCompra.PedidoCompras.FirstOrDefault(p => p.Numero == pedidoCompraId.Numero)
                                           where pedidoExistenteNaLista == null
                                           select pedidoCompraId)
            {
                recebimentoCompra.PedidoCompras.Add(_pedidoCompraRepository.Find(x => x.Numero == pedidoCompraId.Numero).First());
            }
        }

        private void AtualizeListaRecebimentoCompraItens(RecebimentoCompraModel recebimentoCompraModel,
            RecebimentoCompra recebimentoCompra, RecebimentoCompraController controller)
        {
            var novosItens = new List<RecebimentoCompraItem>();

            foreach (var recebimentoCompraItemModel in recebimentoCompraModel.GridItens)
            {
                var itemSalvo = recebimentoCompra.RecebimentoCompraItens.FirstOrDefault(p => p.Id == recebimentoCompraItemModel.Id);
                if (itemSalvo == null)
                {
                    novosItens.Add(CrieNovoRecebimentoCompraItem(recebimentoCompra, recebimentoCompraItemModel, controller));
                    continue;
                }

                var pedidosCompraOrdenados = recebimentoCompraItemModel.PedidosCompra.OrderBy(x => x.Numero);
                var ultimoPedidoCompraId = pedidosCompraOrdenados.Last().Id;

                var item = Mapper.Unflat(recebimentoCompraItemModel, itemSalvo);
                
                var quantidadeEntradaDisponível = recebimentoCompraItemModel.QuantidadeEntrada;

                foreach (var pedidoCompraId in pedidosCompraOrdenados)
                {
                    var pedidoCompra = recebimentoCompra.PedidoCompras.First(p => p.Id == pedidoCompraId.Id);

                    var pedidoCompraItem = pedidoCompra.PedidoCompraItens.SingleOrDefault(
                        s => s.Material.Referencia == recebimentoCompraItemModel.MaterialReferencia);

                    if (pedidoCompraItem == null)
                        throw new Exception(
                            "Não foi possível localizar no banco o pedidoCompraItem referente ao material: " +
                            recebimentoCompraItemModel.MaterialReferencia);

                    var detalhamento = item.ObtenhaDetalhamentoRecebimentoCompraItem(pedidoCompra.Id) ??
                        new DetalhamentoRecebimentoCompraItem();

                    if (quantidadeEntradaDisponível <= 0)
                    {
                        controller.AddInfoMessage("Não foi possível receber o material " + item.Material.Referencia + " do pedido de compra " + pedidoCompraId.Numero
                        + " pois a quantidade recebida não é suficiente para atender esse item.");

                        if (detalhamento.PedidoCompra != null)
                        {
                            item.DetalhamentoRecebimentoCompraItens.Remove(detalhamento);
                            recebimentoCompra.DetalhamentoRecebimentoCompraItens.Remove(detalhamento);
                        }

                        continue;
                    }

                    var ehUltimo = ultimoPedidoCompraId == pedidoCompraId.Id;
                    var quantidadeDetalhamento = ObtenhaQuantidadeDetalhamento(recebimentoCompra, pedidoCompraItem, ehUltimo, ref quantidadeEntradaDisponível);

                    detalhamento.PedidoCompra = pedidoCompra;
                    detalhamento.PedidoCompraItem = pedidoCompraItem;
                    detalhamento.Quantidade = quantidadeDetalhamento;
                    
                    if (detalhamento.Id == null)
                    {
                        item.DetalhamentoRecebimentoCompraItens.Add(detalhamento);
                        recebimentoCompra.DetalhamentoRecebimentoCompraItens.Add(detalhamento);
                    }
                }
            }

            recebimentoCompra.RecebimentoCompraItens.AddRange(novosItens);
        }
        #endregion

        #region Crie PedidoCompraRecebimentoModel

        public PedidoCompraRecebimentoModel CriePedidoCompraRecebimentoModel(PedidoCompra domain)
        {
            var model = Mapper.Flat<PedidoCompraRecebimentoModel>(domain);
            model.NumeroPedido = domain.Numero;

            model.Grid = new List<PedidoCompraItemRecebimentoModel>();

            foreach (var item in domain.PedidoCompraItens.OrderBy(x => x.Material.Referencia))
            {
                if (item.SituacaoCompra == SituacaoCompra.NaoAtendido ||
                    item.SituacaoCompra == SituacaoCompra.AtendidoParcial)
                {
                    var modelItem = Mapper.Flat<PedidoCompraItemRecebimentoModel>(item);
                    modelItem.MaterialDescricaoPedido = item.Material.Descricao;
                    modelItem.MaterialReferenciaPedido = item.Material.Referencia;
                    modelItem.QuantidadePedido = item.Quantidade - item.QuantidadeEntrega;
                    modelItem.UnidadeMedidaSiglaPedido = item.UnidadeMedida.Sigla;
                    modelItem.ValorUnitarioPedido = item.ValorUnitario;

                    var referenciaExterna =
                        item.Material.ReferenciaExternas.FirstOrDefault(rf => rf.Fornecedor.Id == domain.Fornecedor.Id);

                    if (referenciaExterna != null)
                    {
                        modelItem.MaterialReferenciaExternaPedido = referenciaExterna.Referencia;
                    }

                    model.Grid.Add(modelItem);
                }
            }
            return model;
        }

        #endregion

        #region Crie RecebimentoCompraItemModel

        public RecebimentoCompraItemModel CrieRecebimentoCompraItemModel(PedidoCompraItemRecebimentoModel pedidoCompraItemRecebimentoModel, PedidoCompra pedidoCompra)
        {
            var pedidoCompraItem = pedidoCompra.PedidoCompraItens.First(p => p.Id == pedidoCompraItemRecebimentoModel.Id);

            var retorno = new RecebimentoCompraItemModel
            {
                MaterialDescricao = pedidoCompraItem.Material.Descricao,
                MaterialReferencia = pedidoCompraItem.Material.Referencia,
                MaterialReferenciaExterna = pedidoCompraItemRecebimentoModel.MaterialReferenciaExternaPedido,
                Quantidade = pedidoCompraItem.Quantidade,
                UnidadeMedidaSigla = pedidoCompraItem.UnidadeMedida.Sigla,
                UnidadeMedida = pedidoCompraItem.UnidadeMedida.Sigla,
                ValorUnitario = ObtenhaValorUnitario(pedidoCompraItem),
                ValorUnitarioPedido = pedidoCompraItem.ValorUnitario,
                QuantidadeEntrada = ObtenhaQuantidadeDeEntrada(pedidoCompraItem),
                PedidosCompra = new List<IdentificadorPedidoCompra> { new IdentificadorPedidoCompra{ Id = pedidoCompraItemRecebimentoModel.PedidoCompra, Numero = pedidoCompra.Numero }},
                PedidoCompraItens = new List<long?> { pedidoCompraItemRecebimentoModel.Id },
                ValorTotal = ObtenhaTotalItem(pedidoCompraItem)
            };

            return retorno;
        }

        private double ObtenhaValorUnitario(PedidoCompraItem pedidoCompraItem)
        {
            return pedidoCompraItem.ValorUnitario / pedidoCompraItem.UnidadeMedida.ObtenhaFatorMultiplicativoParaEntrada();
        }

        private double ObtenhaTotalItem(PedidoCompraItem pedidoCompraItem)
        {
            return pedidoCompraItem.ValorUnitario * ObtenhaQuantidadeDeEntrada(pedidoCompraItem);
        }

        private double ObtenhaQuantidadeDeEntrada(PedidoCompraItem pedidoCompraItem)
        {
            return pedidoCompraItem.UnidadeMedida.ConvertaQuantidadeParaEntrada(pedidoCompraItem.Quantidade - pedidoCompraItem.QuantidadeEntrega);
        }

        #endregion

        #region Atualize RecebimentoCompraItemModel
        
        public RecebimentoCompraItemModel AtualizeRecebimentoCompraItemModel(RecebimentoCompraItemModel recebimentoCompraItemModel,
            PedidoCompraItemRecebimentoModel pedidoCompraItemRecebimentoModel, PedidoCompra pedidoCompra)
        {
            var numeroPedido = pedidoCompraItemRecebimentoModel.PedidoCompra;
            if (recebimentoCompraItemModel.PedidosCompra.Any(x => x.Id == numeroPedido) &&
                recebimentoCompraItemModel.PedidoCompraItens.Contains(pedidoCompraItemRecebimentoModel.Id))
                return null;

            var recebimentoNovo = CrieRecebimentoCompraItemModel(pedidoCompraItemRecebimentoModel, pedidoCompra);

            if (!recebimentoNovo.UnidadeMedidaSigla.Equals(recebimentoCompraItemModel.UnidadeMedidaSigla))
                throw new Exception("Não é possível receber pedidos de compra itens com unidades de medida diferentes. Material de referência: "
                    + recebimentoNovo.MaterialReferencia);

            recebimentoNovo.Id = recebimentoCompraItemModel.Id;
            recebimentoNovo.Quantidade = recebimentoNovo.Quantidade + recebimentoCompraItemModel.Quantidade;
            recebimentoNovo.ValorUnitario = (recebimentoNovo.ValorUnitario + recebimentoCompraItemModel.ValorUnitario) / 2;
            recebimentoNovo.ValorUnitarioPedido = (recebimentoNovo.ValorUnitarioPedido + recebimentoCompraItemModel.ValorUnitarioPedido) / 2;
            recebimentoNovo.QuantidadeEntrada = recebimentoNovo.QuantidadeEntrada + recebimentoCompraItemModel.QuantidadeEntrada;
            recebimentoNovo.ValorTotal = recebimentoNovo.ValorTotal + recebimentoNovo.ValorTotal;
            recebimentoNovo.PedidosCompra.AddRange(recebimentoCompraItemModel.PedidosCompra);
            recebimentoNovo.PedidoCompraItens.AddRange(recebimentoCompraItemModel.PedidoCompraItens);

            IList<IdentificadorPedidoCompra> list = recebimentoNovo.PedidosCompra;
            IEnumerable<IdentificadorPedidoCompra> sortedEnum = list.OrderBy(f => f.Numero);
            recebimentoNovo.PedidosCompra = sortedEnum.ToList();

            return recebimentoNovo;
        }
        #endregion
    }
}