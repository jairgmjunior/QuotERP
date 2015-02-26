using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class OrdemProducaoItemMap : FashionClassMap<OrdemProducaoItem>
    {
        public OrdemProducaoItemMap()
            : base("ordemproducaoitem", 10)
        {
            Map(x => x.QuantidadeSolicitada).Not.Nullable();
            Map(x => x.QuantidadeAdicional).Not.Nullable();
            Map(x => x.QuantidadeCancelada).Not.Nullable();
            Map(x => x.SituacaoOrdemProducao).Not.Nullable();

            References(x => x.FichaTecnicaVariacaoMatriz).Not.Nullable();
            References(x => x.Tamanho).Not.Nullable();
            References(x => x.OrdemProducao).Not.Nullable();

            HasMany(x => x.OrdemProducaoItemFechamentos)
                .Inverse()
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}