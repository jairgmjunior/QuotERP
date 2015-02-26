using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class OrdemProducaoItemFechamentoMap : FashionClassMap<OrdemProducaoItemFechamento>
    {
        public OrdemProducaoItemFechamentoMap()
            : base("ordemproducaoitemfechamento", 1)
        {
            Map(x => x.Data).Not.Nullable();
            Map(x => x.QuantidadeProduzida).Not.Nullable();

            References(x => x.OrdemProducaoItem).Not.Nullable();

            HasMany(x => x.OrdemProducaoItemFechamentoSinistros)
                .Not.LazyLoad()
                .Inverse()
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}