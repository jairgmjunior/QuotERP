using Fashion.ERP.Domain.Financeiro;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Financeiro
{
    public class RecebimentoChequeRecebidoMap : FashionClassMap<RecebimentoChequeRecebido>
    {
        public RecebimentoChequeRecebidoMap()
            : base("recebimentochequerecebido", 10)
        {
            Map(x => x.Valor).Not.Nullable();
            References(x => x.BaixaChequeRecebido).Not.Nullable();
            References(x => x.MeioPagamento).Not.Nullable();
        }
    }
}