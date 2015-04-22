using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.EngenhariaProduto
{
    public class ModeloReprovacaoMap : EmpresaClassMap<ModeloReprovacao>
    {
        public ModeloReprovacaoMap()
            : base("modeloreprovacao", 0)
        {
            Map(x => x.Motivo).Not.Nullable();
        }
    }
}