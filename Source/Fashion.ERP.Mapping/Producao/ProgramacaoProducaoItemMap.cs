using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class ProgramacaoProducaoItemMap : EmpresaClassMap<ProgramacaoProducaoItem>
    {
        public ProgramacaoProducaoItemMap()
            : base("programacaoproducaoitem", 0)
        {
            Map(x => x.Quantidade);

            References(x => x.FichaTecnica);
            References(x => x.ProgramacaoProducaoMatrizCorte).Cascade.All();
        }
    }
}