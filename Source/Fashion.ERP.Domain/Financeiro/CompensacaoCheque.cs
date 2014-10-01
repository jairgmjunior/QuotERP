namespace Fashion.ERP.Domain.Financeiro
{
    public class CompensacaoCheque : DomainBase<CompensacaoCheque>
    {
        public virtual int Codigo { get; set; }
        public virtual string Descricao { get; set; }
    }
}