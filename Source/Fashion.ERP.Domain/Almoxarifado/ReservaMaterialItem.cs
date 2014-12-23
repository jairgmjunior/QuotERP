using System;

namespace Fashion.ERP.Domain.Almoxarifado
{
    public class ReservaMaterialItem : DomainEmpresaBase<ReservaMaterialItem>
    {
        public virtual Double QuantidadeReserva { get; set; }
        public virtual Double QuantidadeAtendida { get; set; }
        public virtual Material Material { get; set; }
        public virtual SituacaoReservaMaterial SituacaoReservaMaterial { get; set; }

        public virtual ReservaMaterialItemCancelado ReservaMaterialItemCancelado { get; set; }
        
        public virtual void AtualizeSituacao()
        {
            var quantidadeCancelada = ReservaMaterialItemCancelado != null ? ReservaMaterialItemCancelado.QuantidadeCancelada : 0;
            var quantidadeTotal = QuantidadeAtendida + quantidadeCancelada;

            if (quantidadeTotal == 0)
                SituacaoReservaMaterial = SituacaoReservaMaterial.NaoAtendida;
            else if (QuantidadeReserva.Equals(quantidadeCancelada))
                SituacaoReservaMaterial = SituacaoReservaMaterial.Cancelada;
            else if (QuantidadeReserva <= quantidadeTotal)
                SituacaoReservaMaterial = SituacaoReservaMaterial.AtendidaTotal;
            else if (QuantidadeReserva > quantidadeTotal)
                SituacaoReservaMaterial = SituacaoReservaMaterial.AtendidaParcial;
        }
    }
}