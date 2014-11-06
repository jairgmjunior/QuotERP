using Fashion.ERP.Domain.Financeiro;
using Fashion.Framework.Mapping;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping.Financeiro
{
    public class DespesaTituloReceberMap : FashionClassMap<DespesaTituloReceber>
    {
        public DespesaTituloReceberMap()
            : base("despesatituloreceber", 10)
        {
            Map(x => x.Data).Not.Nullable();
            Map(x => x.Historico).Length(100);
            Map(x => x.Valor).Not.Nullable();
            Map(x => x.AgregarValorTitulo).Not.Nullable();

            References(x => x.TituloReceber).Not.Nullable();

            HasManyToMany(x => x.DespesaReceitas)
                .Table("despesatituloreceberdespesareceita")
                .ParentKeyColumn("despesatituloreceber_id")
                .ChildKeyColumn("despesareceita_id")
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}