using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public sealed class BancoMap : FashionClassMap<Banco>
    {
        public BancoMap()
            : base("banco", 0)
        {
            Map(x => x.Codigo).Not.Nullable();
            Map(x => x.Nome).Not.Nullable().Length(100);
        }
    }
}