using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class ProducaoMatrizCorteItemMap: EmpresaClassMap<ProducaoMatrizCorteItem>
    {
        public ProducaoMatrizCorteItemMap()
            : base("producaomatrizcorteitem", 10)
        {
            Map(x => x.QuantidadeProgramada).Not.Nullable();
            Map(x => x.QuantidadeAdicional).Not.Nullable();
            Map(x => x.QuantidadeCorte).Not.Nullable();
            Map(x => x.QuantidadeProducao).Not.Nullable();
            Map(x => x.QuantidadeVezes).Not.Nullable();

            References(x => x.Tamanho);
        }
    }
}
