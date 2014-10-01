using Fashion.ERP.Domain.Compras;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Compras
{
    public class RecebimentoPedidoCompraItemMap : FashionClassMap<RecebimentoPedidoCompraItem>
    {
        public RecebimentoPedidoCompraItemMap()
            : base("recebimentopedidocompraitem", 0)
        {
            Map(x => x.Quantidade).Not.Nullable();

            References(x => x.PedidoCompraItem).Not.Nullable();
        }
    }
}