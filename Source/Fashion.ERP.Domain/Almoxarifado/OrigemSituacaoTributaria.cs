namespace Fashion.ERP.Domain.Almoxarifado
{
    public class OrigemSituacaoTributaria : DomainBase<OrigemSituacaoTributaria>
    {
        public virtual string Codigo { get; set; }
        public virtual string Descricao { get; set; }
    }
}