using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public class PermissaoMap : FashionClassMap<Permissao>
    {
        public PermissaoMap()
        {
            Table("permissao");
            Id(x => x.Id).Column("id").GeneratedBy.Identity();
            Map(x => x.Action);
            Map(x => x.Area);
            Map(x => x.Controller);
            Map(x => x.Descricao);
            Map(x => x.ExibeNoMenu);
            Map(x => x.RequerPermissao);
            Map(x => x.Ordem).Not.Nullable();

            References(x => x.PermissaoPai).Column("permissaopai_id");
            
            HasMany(x => x.PermissoesFilhas)
                .Access.CamelCaseField(Prefix.Underscore)
                .Inverse()
                .Cascade.None()
                .Fetch.Join();
        }
    }
}
