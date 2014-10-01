namespace Fashion.ERP.Domain.Comum
{
    public class CentroCusto : DomainBase<CentroCusto>
    {
        public virtual long Codigo { get; set; }
        public virtual string Nome { get; set; }
        public virtual bool Ativo { get; set; }
    }
}