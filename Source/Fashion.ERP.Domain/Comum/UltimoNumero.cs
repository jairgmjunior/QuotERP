namespace Fashion.ERP.Domain.Comum
{
    public class UltimoNumero : DomainEmpresaBase<UltimoNumero>
    {
        public virtual long Numero { get; set; }
        public virtual string  NomeTabela { get; set; }
    }
}