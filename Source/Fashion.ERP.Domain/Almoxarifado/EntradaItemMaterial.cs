namespace Fashion.ERP.Domain.Almoxarifado
{
    public class EntradaItemMaterial : DomainBase<EntradaItemMaterial>
    {
        public virtual EntradaMaterial EntradaMaterial { get; set; }

        public virtual double QuantidadeCompra { get; set; }
        public virtual Material Material { get; set; }
        public virtual UnidadeMedida UnidadeMedidaCompra { get; set; }
        public virtual MovimentacaoEstoqueMaterial MovimentacaoEstoqueMaterial { get; set; }
    }
}