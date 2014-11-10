using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado
{
    public class SaidaItemMaterialMap : FashionClassMap<SaidaItemMaterial>
    {
        public SaidaItemMaterialMap()
            : base("saidaitemmaterial", 0)
        {
            Map(x => x.Quantidade).Not.Nullable();

            References(x => x.SaidaMaterial).Not.Nullable();
            References(x => x.Material).Not.Nullable();
        } 
    }
}