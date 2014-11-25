using Fashion.ERP.Domain.Compras;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Compras
{
    public class ParametroModuloCompraMap : FashionClassMap<ParametroModuloCompra>
    {
        public ParametroModuloCompraMap()
            : base("parametromodulocompra", 0)
        {
            Map(x => x.ValidaRecebimentoPedido).Not.Nullable();
            Map(x => x.PercentualCriacaoPedidoAutorizadoRecebimento).Not.Nullable();
        }
    }
}