using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public sealed class CidadeMap : FashionClassMap<Cidade>
    {
        public CidadeMap()
            : base("cidade", 0)
        {
            Map(x => x.Nome).Not.Nullable().Length(100);
            Map(x => x.CodigoIbge).Not.Nullable();

            References(x => x.UF).Not.Nullable();
        }
    }
}