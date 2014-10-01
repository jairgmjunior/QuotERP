using System;

namespace Fashion.ERP.Domain.Compras
{
    public class PedidoCompraItemCancelado : DomainBase<PedidoCompraItemCancelado>
    {          
        public virtual DateTime? Data { get; set; }
        public virtual double QuantidadeCancelada { get; set; }
        public virtual string Observacao { get; set; }
        public virtual MotivoCancelamentoPedidoCompra MotivoCancelamentoPedidoCompra { get; set; }

        public virtual void CalculeQuantidade(PedidoCompraItem pedidoCompraItem)
        {
            QuantidadeCancelada = pedidoCompraItem.Quantidade - pedidoCompraItem.QuantidadeEntrega;
        }
    }
}
