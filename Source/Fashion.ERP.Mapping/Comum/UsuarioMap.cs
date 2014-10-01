using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public class UsuarioMap : FashionClassMap<Usuario>
    {
        public UsuarioMap() 
            : base("usuario", 10)
        {
            Map(x => x.Login);
            Map(x => x.Nome);
            Map(x => x.Senha);

            References(x => x.Funcionario).Not.Nullable();

            HasManyToMany(x => x.PerfisDeAcesso)
                .Access.CamelCaseField(Prefix.Underscore)
                .Table("perfildeacessotousuario")
                .ParentKeyColumn("usuario_id")
                .ChildKeyColumn("perfildeacesso_id");

            HasManyToMany(x => x.Permissoes)
                .Access.CamelCaseField(Prefix.Underscore)
                .Table("permissaotousuario")
                .ParentKeyColumn("usuario_id")
                .ChildKeyColumn("permissao_id");
        }
    }
}
