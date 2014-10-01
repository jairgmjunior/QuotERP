using Fashion.ERP.Domain.Compras;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Compras
{
    public class DetalhamentoRecebimentoCompraItemMap: FashionClassMap<DetalhamentoRecebimentoCompraItem>
    {
        public DetalhamentoRecebimentoCompraItemMap() :
            base("detalhamentorecebimentocompraitem", 0)
        {
            Map(x => x.Quantidade).Not.Nullable();

            References(x => x.PedidoCompra).Not.Nullable();
            References(x => x.PedidoCompraItem).Not.Nullable();
        }
    }
}