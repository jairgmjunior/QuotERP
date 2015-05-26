using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.EngenhariaProduto
{
    public class ModeloMaterialConsumoMap : EmpresaClassMap<ModeloMaterialConsumo>
    {
        public ModeloMaterialConsumoMap()
            : base("modelomaterialconsumo", 10)
        {
            Map(x => x.Quantidade).Not.Nullable();

            References(x => x.UnidadeMedida).Not.Nullable();
            References(x => x.Material).Not.Nullable();
            References(x => x.DepartamentoProducao).Not.Nullable();
        }
    }
}