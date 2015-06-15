using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class ProgramacaoProducaoMatrizCorteItemMap: EmpresaClassMap<ProgramacaoProducaoMatrizCorteItem>
    {
        public ProgramacaoProducaoMatrizCorteItemMap()
            : base("programacaoproducaomatrizcorteitem", 10)
        {
            Map(x => x.Quantidade).Not.Nullable();
            Map(x => x.QuantidadeVezes).Not.Nullable();

            References(x => x.Tamanho);
        }
    }
}
