using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Mapping;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado
{
    public class EstoqueMaterialMap : FashionClassMap<EstoqueMaterial>
    {
        public EstoqueMaterialMap()
            : base("estoquematerial", 0)
        {
            Map(x => x.Quantidade).Not.Nullable();
            Map(x => x.Reserva).Not.Nullable();

            References(x => x.Material).Not.Nullable();
            References(x => x.DepositoMaterial).Not.Nullable();
            
            HasMany(x => x.MovimentacaoEstoqueMateriais)
                .Not.LazyLoad()
                .Inverse()
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);
        } 
    }
}