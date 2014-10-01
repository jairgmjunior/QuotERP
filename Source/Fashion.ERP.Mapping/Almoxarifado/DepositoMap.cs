using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Mapping;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado
{
    public class DepositoMaterialMap : FashionClassMap<DepositoMaterial>
    {
        public DepositoMaterialMap()
            : base("depositomaterial", 0)
        {
            Map(x => x.Nome).Length(60).Not.Nullable();
            Map(x => x.DataAbertura).Not.Nullable();
            Map(x => x.Ativo).Not.Nullable();

            References(x => x.Unidade).Not.Nullable();
            HasManyToMany(x => x.Funcionarios)
                .Table("depositomaterialfuncionario")
                .ParentKeyColumn("depositomaterial_id")
                .ChildKeyColumn("funcionario_id")
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}