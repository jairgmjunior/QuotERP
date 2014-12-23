using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado
{
    public class ReservaMaterialItemMap: EmpresaClassMap<ReservaMaterialItem>
    {
        public ReservaMaterialItemMap() : base("reservamaterialitem", 0)
        {
            Map(x => x.QuantidadeAtendida).Not.Nullable();
            Map(x => x.QuantidadeReserva).Not.Nullable();
            Map(x => x.SituacaoReservaMaterial).Not.Nullable();

            References(x => x.Material).Not.Nullable();
            References(x => x.ReservaMaterialItemCancelado).Cascade.All();
        }
    }
}