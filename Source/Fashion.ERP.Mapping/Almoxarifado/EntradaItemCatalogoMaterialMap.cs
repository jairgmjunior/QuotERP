using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado
{
    public class EntradaItemMaterialMap : FashionClassMap<EntradaItemMaterial>
    {
        public EntradaItemMaterialMap()
            : base("entradaitemmaterial", 10)
        {
            Map(x => x.QuantidadeCompra).Not.Nullable();
            Map(x => x.FatorMultiplicativo).Not.Nullable();
            Map(x => x.Quantidade).Not.Nullable();

            References(x => x.EntradaMaterial).Not.Nullable();
            References(x => x.Material).Not.Nullable();
            References(x => x.UnidadeMedida).Not.Nullable();
        } 
    }
}