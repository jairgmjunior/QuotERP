using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Financeiro
{
    public class RecebimentoChequeRecebido : DomainBase<RecebimentoChequeRecebido>
    {
        public virtual double Valor { get; set; }
        public virtual BaixaChequeRecebido BaixaChequeRecebido { get; set; }
        public virtual MeioPagamento MeioPagamento { get; set; }
    }
}