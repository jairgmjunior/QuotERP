using System.Collections.ObjectModel;
using System.Linq;
using Fashion.ERP.Domain.Comum;
using System;
using System.Collections.Generic;

namespace Fashion.ERP.Domain.Compras
{
    public class PedidoCompra : DomainBase<PedidoCompra>
    {
        private readonly IList<PedidoCompraItem> _pedidoCompraItens;

        public PedidoCompra()
        {
            _pedidoCompraItens = new List<PedidoCompraItem>();
        }

        public virtual long Numero { get; set; }
        public virtual DateTime DataCompra { get; set; }
        public virtual DateTime PrevisaoFaturamento { get; set; }
        public virtual DateTime PrevisaoEntrega { get; set; }
        public virtual TipoCobrancaFrete TipoCobrancaFrete { get; set; }
        public virtual double ValorFrete { get; set; }
        public virtual double ValorDesconto { get; set; }
        public virtual double ValorCompra { get; set; }
        public virtual string Observacao { get; set; }
        public virtual bool Autorizado { get; set; }
        public virtual DateTime? DataAutorizacao { get; set; }
        public virtual string ObservacaoAutorizacao { get; set; }
        public virtual SituacaoCompra SituacaoCompra { get; set; }
        public virtual string Contato { get; set; }
        public virtual Pessoa Comprador { get; set; }
        public virtual Pessoa Fornecedor { get; set; }
        public virtual Pessoa UnidadeEstocadora { get; set; }
        public virtual Prazo Prazo { get; set; }
        public virtual MeioPagamento MeioPagamento { get; set; }

        #region pedidoCompraItem

        public virtual IReadOnlyCollection<PedidoCompraItem> PedidoCompraItens
        {
            get { return new ReadOnlyCollection<PedidoCompraItem>(_pedidoCompraItens); }
        }

        public virtual void AddPedidoCompraItem(params PedidoCompraItem[] pedidoCompraItens)
        {
            foreach (var pedidoCompraItem in pedidoCompraItens)
            {
                pedidoCompraItem.PedidoCompra = this;
                _pedidoCompraItens.Add(pedidoCompraItem);
            }
        }

        public virtual void RemovePedidoCompraItem(params PedidoCompraItem[] pedidoCompraItens)
        {
            foreach (var pedidoCompraItem in pedidoCompraItens)
            {
                if (_pedidoCompraItens.Contains(pedidoCompraItem))
                    _pedidoCompraItens.Remove(pedidoCompraItem);
            }
        }

        public virtual void AtualizeSituacao()
        {
            double quantidadePedida = _pedidoCompraItens.Select(p => p.Quantidade)
                .Aggregate((quantidadeTotal, quantidade) => quantidadeTotal + quantidade);

            double quantidadeEntregue = _pedidoCompraItens.Select(p => p.QuantidadeEntrega)
                .Aggregate((quantidadeTotal, quantidade) => quantidadeTotal + quantidade);

            double quantidadeCancelada = _pedidoCompraItens.Select(
                p => p.PedidoCompraItemCancelado == null ? 0 : p.PedidoCompraItemCancelado.QuantidadeCancelada)
                .Aggregate((quantidadeTotal, quantidade) => quantidadeTotal + quantidade);

            if (quantidadePedida.Equals(quantidadeCancelada))
                SituacaoCompra = SituacaoCompra.Cancelado;
            else if (quantidadePedida.Equals(quantidadeEntregue + quantidadeCancelada))
                SituacaoCompra = SituacaoCompra.AtendidoTotal;
            else if (quantidadePedida > (quantidadeEntregue + quantidadeCancelada))
                SituacaoCompra = SituacaoCompra.AtendidoParcial;
        }

        #endregion
    }
}
