using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public sealed class UfMap : FashionClassMap<UF>
    {
        public UfMap()
            : base("uf", 0)
        {
            Map(x => x.Nome).Not.Nullable().Length(100);
            Map(x => x.Sigla).Not.Nullable().Length(2);
            Map(x => x.CodigoIbge).Not.Nullable();

            References(x => x.Pais).Not.Nullable();
        }
    }
}