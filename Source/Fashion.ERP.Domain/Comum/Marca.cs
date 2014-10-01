namespace Fashion.ERP.Domain.Comum
{
    public class Marca : DomainBase<Marca>
    {
        public virtual string Nome { get; set; }
        public virtual bool Ativo { get; set; }
    }
}