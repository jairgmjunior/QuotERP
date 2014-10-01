using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public class CentroCustoMap : FashionClassMap<CentroCusto>
    {
        public CentroCustoMap()
            : base("centrocusto", 0)
        {
            Map(x => x.Codigo).Not.Nullable();
            Map(x => x.Nome).Length(60).Not.Nullable();
            Map(x => x.Ativo).Not.Nullable();
        }
    }
}