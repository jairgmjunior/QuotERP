namespace Fashion.ERP.Domain.Almoxarifado
{
    public class EntradaItemMaterial : DomainBase<EntradaItemMaterial>
    {
        public virtual EntradaMaterial EntradaMaterial { get; set; }

        public virtual double QuantidadeCompra { get; set; }
        public virtual double FatorMultiplicativo { get; set; }
        public virtual double Quantidade { get; set; }

        public virtual Material Material { get; set; }
        public virtual UnidadeMedida UnidadeMedida { get; set; }
    }
}