namespace Fashion.ERP.Domain.Almoxarifado
{
    public class Tecido : DomainBase<Tecido>
    {
        public virtual string Composicao { get; set; }
        public virtual string Armacao { get; set; }
        public virtual string Gramatura { get; set; }
        public virtual string Largura { get; set; }
        public virtual string Rendimento { get; set; }
    }
}