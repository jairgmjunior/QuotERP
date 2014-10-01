using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public sealed class PaisMap : FashionClassMap<Pais>
    {
        public PaisMap()
            : base("pais", 0)
        {
            Map(x => x.Nome).Not.Nullable().Length(100);
            Map(x => x.CodigoBacen).Not.Nullable();
        }
    }
}