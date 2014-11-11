namespace Fashion.ERP.Domain.Almoxarifado
{
    public class SaidaItemMaterial : DomainBase<SaidaItemMaterial>
    {
        public virtual SaidaMaterial SaidaMaterial { get; set; }
        public virtual Material Material { get; set; }
        public virtual MovimentacaoEstoqueMaterial MovimentacaoEstoqueMaterial { get; set; }
    }
}