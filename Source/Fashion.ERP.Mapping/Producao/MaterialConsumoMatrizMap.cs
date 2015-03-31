using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class MaterialConsumoMatrizMap : EmpresaClassMap<MaterialConsumoMatriz>
    {
        public MaterialConsumoMatrizMap() : base("materialconsumomatriz", 1)
        {
            Map(x => x.Custo);
            Map(x => x.Quantidade);

            References(x => x.Material);
            References(x => x.DepartamentoProducao);
        }
    }
}