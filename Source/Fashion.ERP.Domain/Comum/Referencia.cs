namespace Fashion.ERP.Domain.Comum
{
    public class Referencia : DomainBase<Referencia>
    {
        public virtual Cliente Cliente { get; set; }

        public virtual TipoReferencia TipoReferencia { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Telefone { get; set; }
        public virtual string Celular { get; set; }
        public virtual string Observacao { get; set; }
    }
}