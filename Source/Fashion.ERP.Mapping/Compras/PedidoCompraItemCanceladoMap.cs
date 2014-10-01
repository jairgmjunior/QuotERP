using Fashion.ERP.Domain.Compras;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Compras
{
    public class PedidoCompraItemCanceladoMap : FashionClassMap<PedidoCompraItemCancelado>
    {
        public PedidoCompraItemCanceladoMap()
            : base("PedidoCompraItemCancelado", 10)
        {
            Map(x => x.Data).Not.Nullable();
            Map(x => x.QuantidadeCancelada).Not.Nullable();
            Map(x => x.Observacao);
            
            References(x => x.MotivoCancelamentoPedidoCompra).Not.Nullable();           
        }
    }
}