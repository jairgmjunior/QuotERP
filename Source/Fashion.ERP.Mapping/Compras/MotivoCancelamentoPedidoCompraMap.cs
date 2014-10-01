using Fashion.ERP.Domain.Compras;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Compras
{
    public class MotivoCancelamentoPedidoCompraMap : FashionClassMap<MotivoCancelamentoPedidoCompra>
    {
        public MotivoCancelamentoPedidoCompraMap() :
            base("motivocancelamentopedidocompra", 0)
        {
            Map(x => x.Descricao).Not.Nullable();
            Map(x => x.Ativo).Not.Nullable();
        }
    }
}