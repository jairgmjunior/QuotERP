using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class MaterialComposicaoCustoMatrizMap : EmpresaClassMap<MaterialComposicaoCustoMatriz>
    {
        public MaterialComposicaoCustoMatrizMap() : base("materialcomposicaocustomatriz", 1)
        {
            Map(x => x.Custo);

            References(x => x.Material);
        }
    }
}