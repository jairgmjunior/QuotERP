using Fashion.ERP.Domain.Compras;
using Fashion.Framework.Mapping;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping.Compras
{
    public class ProcedimentoModuloComprasMap : FashionClassMap<ProcedimentoModuloCompras>
    {
        public ProcedimentoModuloComprasMap()
            : base("procedimentomodulocompras", 0)
        {
            Map(x => x.Codigo).Not.Nullable();
            Map(x => x.Descricao).Length(60).Not.Nullable();

            HasManyToMany(x => x.Funcionarios)
                .Table("procedimentomodulocomprasfuncionario")
                .ParentKeyColumn("procedimentomodulocompras_id")
                .ChildKeyColumn("funcionario_id")
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}