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

            References(x => x.EntradaMaterial).Not.Nullable();
            References(x => x.Material).Not.Nullable();
            References(x => x.UnidadeMedidaCompra).Not.Nullable();
            References(x => x.MovimentacaoEstoqueMaterial).Not.Nullable().Cascade.All();
        } 
    }
}