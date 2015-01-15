using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado
{
    public class RequisicaoMaterialItemCanceladoMap : EmpresaClassMap<RequisicaoMaterialItemCancelado>
    {
        public RequisicaoMaterialItemCanceladoMap()
            : base("requisicaomaterialitemcancelado", 0)
        {
            Map(x => x.Data).Not.Nullable();
            Map(x => x.Observacao).Not.Nullable();
            Map(x => x.QuantidadeCancelada).Not.Nullable();
        }         
    }
}