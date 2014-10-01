namespace Fashion.ERP.Domain.Almoxarifado
{
    public class TipoItem : DomainBase<TipoItem>
    {
        public virtual string Descricao { get; set; }
        public virtual string Codigo { get; set; }
    }
}