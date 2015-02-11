using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public class VariacaoModeloMap : FashionClassMap<VariacaoModelo>
    {
        public VariacaoModeloMap()
            : base("variacaomodelo", 0)
        {
            References(x => x.Variacao).Not.Nullable();

            HasManyToMany(x => x.Cores)
                .Table("variacaomodelocor")
                .ParentKeyColumn("variacaomodelo_id")
                .ChildKeyColumn("cor_id")
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}