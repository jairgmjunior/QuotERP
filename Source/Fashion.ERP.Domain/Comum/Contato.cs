namespace Fashion.ERP.Domain.Comum
{
    public class Contato : DomainBase<Contato>
    {
        public virtual TipoContato TipoContato { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Telefone { get; set; }
        public virtual string Operadora { get; set; }
        public virtual string Email { get; set; }

        public virtual Pessoa Pessoa { get; set; }
    }
}