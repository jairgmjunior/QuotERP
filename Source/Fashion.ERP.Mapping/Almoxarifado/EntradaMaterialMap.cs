using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Mapping;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado
{
    public class EntradaMaterialMap : FashionClassMap<EntradaMaterial>
    {
        public EntradaMaterialMap()
            : base("entradamaterial", 0)
        {
            Map(x => x.DataEntrada).Not.Nullable();
            Map(x => x.DataAlteracao).Not.Nullable();

            References(x => x.DepositoMaterialDestino).Not.Nullable();
            References(x => x.DepositoMaterialOrigem);
            References(x => x.Fornecedor);
            
            HasMany(x => x.EntradaItemMateriais)
                .Not.LazyLoad()
                .Inverse()
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);
        } 
    }
}