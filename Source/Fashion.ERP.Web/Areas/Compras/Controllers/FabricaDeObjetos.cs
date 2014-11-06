using System;
using System.Collections.Generic;
using System.Linq;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Web.Areas.Compras.Models;
using Fashion.ERP.Web.Helpers;
using Fashion.Framework.Repository;
using FluentNHibernate.Conventions;
using Kendo.Mvc.Extensions;

namespace Fashion.ERP.Web.Areas.Compras.Controllers
{
    public class FabricaDeObjetos
    {
        private readonly IRepository<PedidoCompra> _pedidoCompraRepository;
        private readonly IRepository<RecebimentoCompra> _recebimentoCompraRepository;
        private readonly IRepository<Material> _materialRepository;

        public FabricaDeObjetos(
            IRepository<PedidoCompra> pedidoCompraRepository,
            IRepository<RecebimentoCompra> recebimentoCompraRepository,
            IRepository<Material> materialRepository)
        {
            _pedidoCompraRepository = pedidoCompraRepository;
            _recebimentoCompraRepository = recebimentoCompraRepository;
            _materialRepository = materialRepository;
        }

        #region Crie RecebimentoCompra
        public RecebimentoCompra CrieNovoRecebimentoCompra(RecebimentoCompraModel model)
        {
            var domain = Mapper.Unflat<RecebimentoCompra>(model);
            domain.Numero = ProximoNumero();
            domain.Data = DateTime.Now;
            domain.DataAlteracao = DateTime.Now;

            CrieListaPedidoCompra(model, domain);
            CrieListaRecebimentoCompraItens(model, domain);
            
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
                recebimentoCompra.PedidoCompras.Add(_pedidoCompraRepository.Get(pedidoCompraId));
            }
        }

        private void CrieListaRecebimentoCompraItens(RecebimentoCompraModel recebimentoCompraModel, RecebimentoCompra recebimentoCompra)
        {
            recebimentoCompra.RecebimentoCompraItens = recebimentoCompraModel.GridItens.Select(x => CrieNovoRecebimentoCompraItem(recebimentoCompra, x)).ToList();
        }

        public void AtualizePedidosCompra(RecebimentoCompra recebimentoCompra)
        {
            Framework.UnitOfWork.Session.Current.Flush();

            foreach (var pedidoCompra in recebimentoCompra.PedidoCompras)
            {
                var recebimentos = _recebimentoCompraRepository.Find().Where(p => p.PedidoCompras.Any(i => i.Id == pedidoCompra.Id)).ToList();

                foreach (var pedidoCompraItem in pedidoCompra.PedidoCompraItens)
                {
                    var detalhamentos = recebimentos.SelectMany(r => r.DetalhamentoRecebimentoCompraItens);
                    var detalhamentosDoPedidoCompraItem = detalhamentos.Where(d => d.PedidoCompraItem.Id == pedidoCompraItem.Id).ToList();
                    
                    double quantidadeEntregue = 0;

                    if (!detalhamentosDoPedidoCompraItem.IsEmpty())
                        quantidadeEntregue = detalhamentosDoPedidoCompraItem.Select(p => p.Quantidade)
                            .Aggregate((quantidadeTotal, quantidade) => quantidadeTotal + quantidade);

                    pedidoCompraItem.QuantidadeEntrega = pedidoCompraItem.UnidadeMedida.ConvertaQuantidadeParaEntrada(quantidadeEntregue);
                    pedidoCompraItem.AtualizeSituacao();
                }

                pedidoCompra.AtualizeSituacao();
                _pedidoCompraRepository.Update(pedidoCompra);
            }
        }

        public void AtualizePedidosCompraAoExcluir(RecebimentoCompra recebimentoCompra)
        {
            foreach (var pedidoCompra in recebimentoCompra.PedidoCompras)
            {
                var recebimentos = _recebimentoCompraRepository.Find().Where(p => p.PedidoCompras.Any(i => i.Id == pedidoCompra.Id)
                    && p.Id != recebimentoCompra.Id).ToList();

                foreach (var pedidoCompraItem in pedidoCompra.PedidoCompraItens)
                {
                    var detalhamentos = recebimentos.SelectMany(r => r.DetalhamentoRecebimentoCompraItens);
                    var detalhamentosDoPedidoCompraItem = detalhamentos.Where(d => d.PedidoCompraItem.Id == pedidoCompraItem.Id).ToList();

                    double quantidadeEntregue = 0;

                    if(!detalhamentosDoPedidoCompraItem.IsEmpty())
                        quantidadeEntregue = detalhamentosDoPedidoCompraItem.Select(p => p.Quantidade)
                            .Aggregate((quantidadeTotal, quantidade) => quantidadeTotal + quantidade);
                    
                    pedidoCompraItem.QuantidadeEntrega = pedidoCompraItem.UnidadeMedida.ConvertaQuantidadeParaEntrada(quantidadeEntregue);
                    pedidoCompraItem.AtualizeSituacao();
                }

                pedidoCompra.AtualizeSituacao();
                _pedidoCompraRepository.Update(pedidoCompra);
            }
        }

        private RecebimentoCompraItem CrieNovoRecebimentoCompraItem(RecebimentoCompra recebimentoCompra, RecebimentoCompraItemModel recebimentoCompraItemModel)
        {
            var item = Mapper.Unflat<RecebimentoCompraItem>(recebimentoCompraItemModel);
            item.Material = _materialRepository.Find(p => p.Referencia == recebimentoCompraItemModel.MaterialReferencia).FirstOrDefault();
            var quantidadeEntradaDisponível = recebimentoCompraItemModel.QuantidadeEntrada;

            foreach (var pedidoCompraId in recebimentoCompraItemModel.PedidosCompra)
            {
                var pedidoCompra = recebimentoCompra.PedidoCompras.First(p => p.Id == pedidoCompraId);
                var pedidoCompraItem = pedidoCompra.PedidoCompraItens.SingleOrDefault(
                    s => s.Material.Referencia == recebimentoCompraItemModel.MaterialReferencia);

                if (pedidoCompraItem == null)
                    throw new Exception("Não foi possível localizar no banco o pedidoCompraItem referente ao material: " +
                                        recebimentoCompraItemModel.MaterialReferencia);
                
                var detalhamento = new DetalhamentoRecebimentoCompraItem
                {
                    PedidoCompra = pedidoCompra,
                    PedidoCompraItem = pedidoCompraItem,
                    Quantidade = ObtenhaQuantidadeDetalhamento(ref quantidadeEntradaDisponível, pedidoCompraItem.Quantidade)
                };
                
                recebimentoCompra.DetalhamentoRecebimentoCompraItens.Add(detalhamento);
                item.DetalhamentoRecebimentoCompraItens.Add(detalhamento);
            }

            return item;
        }

        private double ObtenhaQuantidadeDetalhamento(ref double quantidadeEntradaDisponivel, double quantidadePedidoItem)
        {
            if (quantidadeEntradaDisponivel >= quantidadePedidoItem)
            {
                quantidadeEntradaDisponivel = quantidadeEntradaDisponivel - quantidadePedidoItem;
                return quantidadePedidoItem;
            }

            var retorno = quantidadeEntradaDisponivel;
            quantidadeEntradaDisponivel = 0;
            return retorno;
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
        public RecebimentoCompra AtualizeRecebimentoCompra(RecebimentoCompra domain, RecebimentoCompraModel model)
        {
            domain = Mapper.Unflat(model, domain);
            domain.DataAlteracao = DateTime.Now;

            AtualizeListaPedidoCompra(model, domain);
            AtualizeListaRecebimentoCompraItens(model, domain);
            AtualizePedidosCompra(domain);
            
            return domain;
        }

        private void AtualizeListaPedidoCompra(RecebimentoCompraModel recebimentoCompraModel, RecebimentoCompra recebimentoCompra)
        {
            foreach (var pedidoCompraId in from recebimentoCompraItemModel in recebimentoCompraModel.GridItens
                                           from pedidoCompraId in recebimentoCompraItemModel.PedidosCompra
                                           let pedidoExistenteNaLista = recebimentoCompra.PedidoCompras.FirstOrDefault(p => p.Id == pedidoCompraId)
                                           where pedidoExistenteNaLista == null
                                           select pedidoCompraId)
            {
                recebimentoCompra.PedidoCompras.Add(_pedidoCompraRepository.Get(pedidoCompraId));
            }
        }

        private void AtualizeListaRecebimentoCompraItens(RecebimentoCompraModel recebimentoCompraModel,
            RecebimentoCompra recebimentoCompra)
        {
            var novosItens = new List<RecebimentoCompraItem>();

            foreach (var recebimentoCompraItemModel in recebimentoCompraModel.GridItens)
            {
                var itemSalvo = recebimentoCompra.RecebimentoCompraItens.FirstOrDefault(p => p.Id == recebimentoCompraItemModel.Id);
                if (itemSalvo == null)
                {
                    novosItens.Add(CrieNovoRecebimentoCompraItem(recebimentoCompra, recebimentoCompraItemModel));
                    continue;
                }

                var item = Mapper.Unflat(recebimentoCompraItemModel, itemSalvo);
                item.Quantidade = recebimentoCompraItemModel.QuantidadeEntrada;
                var quantidadeEntradaDisponível = recebimentoCompraItemModel.QuantidadeEntrada;

                foreach (var pedidoCompraId in recebimentoCompraItemModel.PedidosCompra)
                {
                    var pedidoCompra = recebimentoCompra.PedidoCompras.First(p => p.Id == pedidoCompraId);

                    var pedidoCompraItem = pedidoCompra.PedidoCompraItens.SingleOrDefault(
                        s => s.Material.Referencia == recebimentoCompraItemModel.MaterialReferencia);

                    if (pedidoCompraItem == null)
                        throw new Exception(
                            "Não foi possível localizar no banco o pedidoCompraItem referente ao material: " +
                            recebimentoCompraItemModel.MaterialReferencia);
                    
                    var detalhamento =
                        item.DetalhamentoRecebimentoCompraItens.SingleOrDefault(
                            d => d.PedidoCompra.Id == pedidoCompra.Id) ??
                        new DetalhamentoRecebimentoCompraItem();

                    detalhamento.PedidoCompra = pedidoCompra;
                    detalhamento.PedidoCompraItem = pedidoCompraItem;
                    detalhamento.Quantidade = ObtenhaQuantidadeDetalhamento(ref quantidadeEntradaDisponível,
                        pedidoCompraItem.Quantidade);
                    
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

            foreach (var item in domain.PedidoCompraItens)
            {
                if (item.SituacaoCompra == SituacaoCompra.NaoAtendido ||
                    item.SituacaoCompra == SituacaoCompra.AtendidoParcial)
                {
                    var modelItem = Mapper.Flat<PedidoCompraItemRecebimentoModel>(item);
                    modelItem.MaterialDescricaoPedido = item.Material.Descricao;
                    modelItem.MaterialReferenciaPedido = item.Material.Referencia;
                    modelItem.QuantidadePedido = item.Quantidade;
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
                UnidadeEntrada = pedidoCompraItem.UnidadeMedida.Sigla,
                ValorUnitario = ObtenhaValorUnitario(pedidoCompraItem),
                ValorUnitarioPedido = pedidoCompraItem.ValorUnitario,
                QuantidadeEntrada = ObtenhaQuantidadeDeEntrada(pedidoCompraItem),
                PedidosCompra = new List<long> { pedidoCompraItemRecebimentoModel.PedidoCompra },
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
            return pedidoCompraItem.UnidadeMedida.ConvertaQuantidadeParaEntrada(pedidoCompraItem.Quantidade);
        }

        #endregion

        #region Atualize RecebimentoCompraItemModel
        
        public RecebimentoCompraItemModel AtualizeRecebimentoCompraItemModel(RecebimentoCompraItemModel recebimentoCompraItemModel,
            PedidoCompraItemRecebimentoModel pedidoCompraItemRecebimentoModel, PedidoCompra pedidoCompra)
        {
            var numeroPedido = pedidoCompraItemRecebimentoModel.PedidoCompra;
            if (recebimentoCompraItemModel.PedidosCompra.Contains(numeroPedido) &&
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

            IList<long> list = recebimentoNovo.PedidosCompra;
            IEnumerable<long> sortedEnum = list.OrderBy(f => f);
            recebimentoNovo.PedidosCompra = sortedEnum.ToList();

            return recebimentoNovo;
        }
        #endregion
    }
}