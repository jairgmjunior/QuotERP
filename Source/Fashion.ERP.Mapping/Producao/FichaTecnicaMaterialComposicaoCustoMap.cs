using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class FichaTecnicaMaterialComposicaoCustoMap : EmpresaClassMap<FichaTecnicaMaterialComposicaoCusto>
    {
        public FichaTecnicaMaterialComposicaoCustoMap()
            : base("fichatecnicamaterialcomposicaocusto", 1)
        {
            Map(x => x.Custo);

            References(x => x.Material);
        }
    }
}