namespace Fashion.ERP.Domain.Comum
{
    public class Cor : DomainBase<Cor>
    {
        public virtual string Nome { get; set; }
        public virtual bool Ativo { get; set; }
    }
}