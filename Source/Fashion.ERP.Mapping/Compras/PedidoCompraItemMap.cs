using Fashion.ERP.Domain.Compras;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Compras
{
    public class PedidoCompraItemMap : FashionClassMap<PedidoCompraItem>
    {
        public PedidoCompraItemMap()
            : base("pedidocompraitem", 10)
        {
            Map(x => x.Quantidade).Not.Nullable();
            Map(x => x.ValorUnitario).Not.Nullable();
            Map(x => x.PrevisaoEntrega);
            Map(x => x.QuantidadeEntrega).Not.Nullable();
            Map(x => x.DataEntrega);
            Map(x => x.SituacaoCompra).Not.Nullable();

            References(x => x.PedidoCompra).Not.Nullable();
            References(x => x.Material).Not.Nullable();
            References(x => x.UnidadeMedida);
            References(x => x.PedidoCompraItemCancelado).Unique().Cascade.All();
        }
    }
}