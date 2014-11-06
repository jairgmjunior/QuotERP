using Fashion.ERP.Domain.Financeiro;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Financeiro
{
    public class RateioCentroCustoMap : EmpresaClassMap<RateioCentroCusto>
    {
        public RateioCentroCustoMap()
            : base("rateiocentrocusto", 10)
        {
            Map(x => x.Valor).Not.Nullable();

            References(x => x.TituloPagar);
            References(x => x.CentroCusto);
        }
    }
}