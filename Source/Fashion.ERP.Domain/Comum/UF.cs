namespace Fashion.ERP.Domain.Comum
{
    public class UF : DomainBase<UF>
    {
        public virtual string Nome { get; set; }
        public virtual string Sigla { get; set; }
        public virtual int CodigoIbge { get; set; }

        public virtual Pais Pais { get; set; }
    }
}