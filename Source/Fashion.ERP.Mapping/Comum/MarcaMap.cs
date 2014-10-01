using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public class MarcaMap : FashionClassMap<Marca>
    {
        public MarcaMap()
            : base("marca", 0)
        {
            Map(x => x.Nome).Length(100).Not.Nullable();
            Map(x => x.Ativo).Not.Nullable();
        }
    }
}