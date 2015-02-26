using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class OrdemProducaoMaterialMap : FashionClassMap<OrdemProducaoMaterial>
    {
        public OrdemProducaoMaterialMap()
            : base("ordemproducaomaterial", 10)
        {
            Map(x => x.Quantidade).Not.Nullable();
            Map(x => x.QuantidadeSubstituida).Not.Nullable();
            Map(x => x.Requisitado).Not.Nullable();

            References(x => x.Material).Not.Nullable();
            References(x => x.OrdemProducaoMaterialPai);
            References(x => x.DepartamentoProducao);
            
            HasMany(x => x.OrdemProducaoMateriais)
                .Inverse()
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}