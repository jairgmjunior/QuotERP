using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado
{
    public class ReservaMaterialItemCanceladoMap : EmpresaClassMap<ReservaMaterialItemCancelado>
    {
        public ReservaMaterialItemCanceladoMap()
            : base("reservamaterialitemcancelado", 0)
        {
            Map(x => x.Data).Not.Nullable();
            Map(x => x.Observacao).Not.Nullable();
            Map(x => x.QuantidadeCancelada).Not.Nullable();
        }         
    }
}