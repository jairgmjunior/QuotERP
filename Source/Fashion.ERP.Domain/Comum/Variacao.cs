namespace Fashion.ERP.Domain.Comum
{
    public class Variacao : DomainBase<Variacao>
    {
        public virtual string Nome { get; set; }
        public virtual bool Ativo { get; set; }
    }
}