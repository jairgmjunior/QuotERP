namespace Fashion.ERP.Domain.Comum
{
    public class Pais : DomainBase<Pais>
    {
        public virtual string Nome { get; set; }
        public virtual int CodigoBacen { get; set; }
    }
}