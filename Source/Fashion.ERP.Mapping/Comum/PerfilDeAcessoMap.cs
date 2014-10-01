using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public class PerfilDeAcessoMap : FashionClassMap<PerfilDeAcesso>
    {
        public PerfilDeAcessoMap()
            : base("perfildeacesso", 10)
        {
            Map(x => x.Nome);
            
            HasManyToMany(x => x.Permissoes)
                .Access.CamelCaseField(Prefix.Underscore)
                .Table("permissaotoperfildeacesso")
                .ParentKeyColumn("perfildeacesso_id")
                .ChildKeyColumn("permissao_id")
                .Fetch.Join();
        }
    }
}
