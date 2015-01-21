using System;

namespace Fashion.ERP.Domain.Almoxarifado
{
    public class RequisicaoMaterialItemCancelado : DomainEmpresaBase<RequisicaoMaterialItem>
    {
        public virtual DateTime? Data { get; set; }
        public virtual double QuantidadeCancelada { get; set; }
        public virtual string Observacao { get; set; }

        public virtual void CalculeQuantidade(RequisicaoMaterialItem requisicaoMaterialItem)
        {
            QuantidadeCancelada = requisicaoMaterialItem.QuantidadeSolicitada - requisicaoMaterialItem.QuantidadeAtendida;
        }
    }
}