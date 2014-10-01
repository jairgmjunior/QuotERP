namespace Fashion.ERP.Domain.Comum
{
    public class Cidade : DomainBase<Cidade>
    {
        public virtual string Nome { get; set; }
        public virtual int CodigoIbge { get; set; }

        public virtual UF UF { get; set; }
    }
}