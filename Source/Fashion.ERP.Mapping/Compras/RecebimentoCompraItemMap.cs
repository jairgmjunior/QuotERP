using Fashion.ERP.Domain.Compras;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Compras
{
    public class RecebimentoCompraItemMap : FashionClassMap<RecebimentoCompraItem>
    {
        public RecebimentoCompraItemMap()
            : base("recebimentocompraitem", 0)
        {
            Map(x => x.Quantidade).Not.Nullable();
            Map(x => x.Custo).Not.Nullable();
            Map(x => x.ValorTotal).Not.Nullable();
            
            References(x => x.Material).Not.Nullable();

            HasMany(x => x.DetalhamentoRecebimentoCompraItens)
                .Cascade.AllDeleteOrphan();
        }
    }
}