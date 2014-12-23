using System;

namespace Fashion.ERP.Domain.Almoxarifado
{
    public class ReservaMaterialItemCancelado : DomainEmpresaBase<ReservaMaterialItem>
    {
        public virtual DateTime Data { get; set; }
        public virtual String Observacao { get; set; }
        public virtual Double QuantidadeCancelada { get; set; }
    }
}