using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.EngenhariaProduto
{
    public class NaturezaMap : FashionClassMap<Natureza>
    {
        public NaturezaMap()
            : base("natureza", 0)
        {
            Map(x => x.Descricao).Length(60).Not.Nullable();
            Map(x => x.Ativo).Not.Nullable();

            HasMany(x => x.SequenciasOperacionais)
                .Not.KeyNullable()
                .Cascade.AllDeleteOrphan();
        }
    }
}