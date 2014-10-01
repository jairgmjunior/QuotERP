namespace Fashion.ERP.Domain.Comum
{
    public class Banco : DomainBase<Banco>
    {
        public virtual int Codigo { get; set; }
        public virtual string Nome { get; set; }
    }
}