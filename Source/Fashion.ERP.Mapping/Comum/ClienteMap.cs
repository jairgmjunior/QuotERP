using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public class ClienteMap : FashionClassMap<Cliente>
    {
        public ClienteMap()
            : base("cliente", 10)
        {
            Map(x => x.Codigo).Not.Nullable();
            Map(x => x.Sexo);
            Map(x => x.EstadoCivil);
            Map(x => x.NomeMae).Length(100);
            Map(x => x.DataValidade);
            Map(x => x.Observacao).Length(4000);
            Map(x => x.DataCadastro).Not.Nullable();

            References(x => x.Profissao);
            References(x => x.AreaInteresse);
            
            HasMany(x => x.Dependentes)
                //.KeyColumn("cliente_id")
                .Not.Inverse()
                .Not.KeyNullable()
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);

            HasMany(x => x.Referencias)
                .Inverse()
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}