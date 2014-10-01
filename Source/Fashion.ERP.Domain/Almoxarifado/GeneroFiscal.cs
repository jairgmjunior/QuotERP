namespace Fashion.ERP.Domain.Almoxarifado
{
    public class GeneroFiscal : DomainBase<GeneroFiscal>
    {
        public virtual string Descricao { get; set; }
        public virtual string Codigo { get; set; }
    }
}