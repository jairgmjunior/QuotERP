using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Mapping;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado
{
    public class SaidaMaterialMap : FashionClassMap<SaidaMaterial>
    {
        public SaidaMaterialMap()
            : base("saidamaterial", 0)
        {
            Map(x => x.DataSaida).Not.Nullable();

            References(x => x.DepositoMaterialDestino);
            References(x => x.DepositoMaterialOrigem).Not.Nullable();
            References(x => x.CentroCusto);

            HasMany(x => x.SaidaItemMateriais)
                .Not.LazyLoad()
                .Inverse()
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);
        } 
    }
}