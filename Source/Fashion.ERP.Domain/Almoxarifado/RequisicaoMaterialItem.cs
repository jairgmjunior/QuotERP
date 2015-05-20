using System;

namespace Fashion.ERP.Domain.Almoxarifado
{
    public class RequisicaoMaterialItem : DomainEmpresaBase<RequisicaoMaterialItem>
    {
        public virtual Double QuantidadeSolicitada { get; set; }     
        public virtual Double QuantidadeAtendida { get; set; }
        public virtual SituacaoRequisicaoMaterial SituacaoRequisicaoMaterial { get; set; }
        public virtual Material Material { get; set; }
        public virtual RequisicaoMaterialItemCancelado RequisicaoMaterialItemCancelado { get; set; }

        public virtual Double QuantidadePendente
        {
            get
            {
                var quantidadeCancelada = RequisicaoMaterialItemCancelado == null
                    ? 0
                    : RequisicaoMaterialItemCancelado.QuantidadeCancelada;

                return QuantidadeSolicitada - (QuantidadeAtendida + quantidadeCancelada);
            }
        }


        public virtual Double QuantidadeCancelada
        {
            get
            {
                return RequisicaoMaterialItemCancelado == null ? 0 : RequisicaoMaterialItemCancelado.QuantidadeCancelada;
            }
        }

        public virtual void AtualizeSituacao()
        {
            var quantidadeCancelada = RequisicaoMaterialItemCancelado != null ? RequisicaoMaterialItemCancelado.QuantidadeCancelada : 0;
            var quantidadeTotal = QuantidadeAtendida + quantidadeCancelada;

            if (quantidadeTotal == 0)
                SituacaoRequisicaoMaterial = SituacaoRequisicaoMaterial.NaoAtendido;
            else if (QuantidadeSolicitada.Equals(quantidadeCancelada))
                SituacaoRequisicaoMaterial = SituacaoRequisicaoMaterial.Cancelado;
            else if (QuantidadeSolicitada <= quantidadeTotal)
                SituacaoRequisicaoMaterial = SituacaoRequisicaoMaterial.AtendidoTotal;
            else if (QuantidadeSolicitada > quantidadeTotal)
                SituacaoRequisicaoMaterial = SituacaoRequisicaoMaterial.AtendidoParcial;
        }
    }
}