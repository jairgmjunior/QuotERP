using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class RemessaProducaoCapacidadeProdutivaMap : EmpresaClassMap<RemessaProducaoCapacidadeProdutiva>
    {
        public RemessaProducaoCapacidadeProdutivaMap(): base("remessaproducaocapacidadeprodutiva", 0)
        {
            Map(x => x.Quantidade);
            
            References(x => x.ClassificacaoDificuldade);
        }
    }
}