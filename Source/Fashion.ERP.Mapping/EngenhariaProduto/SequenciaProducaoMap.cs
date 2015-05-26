using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.EngenhariaProduto
{
    public class SequenciaProducaoMap : FashionClassMap<SequenciaProducao>
    {
        public SequenciaProducaoMap()
            : base("sequenciaproducao", 10)
        {
            //Map(x => x.Ordem).Not.Nullable();
            Map(x => x.DataEntrada);
            Map(x => x.DataSaida);

            References(x => x.DepartamentoProducao).Not.Nullable();
            References(x => x.SetorProducao);
        }
    }
}