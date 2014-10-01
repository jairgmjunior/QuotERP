using Fashion.ERP.Domain.Financeiro;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Financeiro
{
    public class CompensacaoChequeMap : FashionClassMap<CompensacaoCheque>
    {
        public CompensacaoChequeMap()
            : base("compensacaocheque", 0)
        {
            Map(x => x.Codigo).Not.Nullable();
            Map(x => x.Descricao).Not.Nullable().Length(256);
        }
    }
}