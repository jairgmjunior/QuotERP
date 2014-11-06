using Fashion.ERP.Domain.Financeiro;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Financeiro
{
    public class RateioDespesaReceitaMap : EmpresaClassMap<RateioDespesaReceita>
    {
        public RateioDespesaReceitaMap()
            : base("rateiodespesareceita", 10)
        {
            Map(x => x.Valor).Not.Nullable();

            References(x => x.TituloPagar);
            References(x => x.DespesaReceita);
        }
    }
}