using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class OrdemProducaoItemFechamentoSinistroMap : FashionClassMap<OrdemProducaoItemFechamentoSinistro>
    {
        public OrdemProducaoItemFechamentoSinistroMap()
            : base("ordemproducaoitemfechamentosinistro", 1)
        {
            Map(x => x.Quantidade).Not.Nullable();
            Map(x => x.TipoSinistroFechamentoOrdemProducao).Not.Nullable();

            References(x => x.DefeitoProducao).Not.Nullable();
            References(x => x.OrdemProducaoItemFechamento).Not.Nullable();
        }
    }
}