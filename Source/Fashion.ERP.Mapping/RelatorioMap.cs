using Fashion.ERP.Domain;
using Fashion.Framework.Mapping;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping
{
    public class RelatorioMap : FashionClassMap<Relatorio>
    {
        public RelatorioMap()
            : base("relatorio", 0)
        {
            Map(x => x.Nome).Not.Nullable().Length(100);
            
            References(x => x.Arquivo).Not.Nullable();

            HasMany(x => x.RelatorioParametros)
                .Inverse()
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}