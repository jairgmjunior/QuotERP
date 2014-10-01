namespace Fashion.ERP.Domain.Almoxarifado
{
    public class ConferenciaEntradaMaterialItem : DomainBase<ConferenciaEntradaMaterialItem>
    {
        public virtual double Quantidade { get; set; }
        public virtual double QuantidadeConferida { get; set; }
        public virtual SituacaoConferencia SituacaoConferencia { get; set; }
        public virtual Material Material { get; set; }
        public virtual UnidadeMedida UnidadeMedida { get; set; }
    }
}