using System;

namespace Fashion.ERP.Domain.Almoxarifado
{
    public class RequisicaoMaterialItemCancelado : DomainEmpresaBase<RequisicaoMaterialItem>
    {
        public virtual DateTime Data { get; set; }
        public virtual String Observacao { get; set; }
        public virtual Double QuantidadeCancelada { get; set; }
    }
}