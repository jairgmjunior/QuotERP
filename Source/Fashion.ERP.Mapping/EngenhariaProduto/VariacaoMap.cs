using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.Framework.Mapping;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping.EngenhariaProduto
{
    public class VariacaoMap : FashionClassMap<Variacao>
    {
        public VariacaoMap()
            : base("variacao", 10)
        {
            Map(x => x.Nome).Length(100).Not.Nullable();
            
            HasManyToMany(x => x.Cores)
                .Table("variacaocor")
                .ParentKeyColumn("variacao_id")
                .ChildKeyColumn("cor_id")
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}