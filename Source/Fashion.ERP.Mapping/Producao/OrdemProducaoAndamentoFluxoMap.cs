using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class OrdemProducaoAndamentoFluxoMap : FashionClassMap<OrdemProducaoAndamentoFluxo>
    {
        public OrdemProducaoAndamentoFluxoMap()
            : base("ordemproducaoandamentofluxo", 10)
        {
            Map(x => x.Data).Not.Nullable();
            Map(x => x.TipoAndamento).Not.Nullable();
            Map(x => x.Quantidade).Not.Nullable();

            References(x => x.OrdemProducao).Not.Nullable();
        } 
    }
}