using System;
using Fashion.ERP.Domain.Almoxarifado;

namespace Fashion.ERP.Domain.Compras
{
    public class PedidoCompraItem : DomainBase<PedidoCompraItem>
    {
        public virtual double Quantidade { get; set; }
        public virtual double ValorUnitario { get; set; }
        public virtual DateTime? PrevisaoEntrega { get; set; }
        public virtual double QuantidadeEntrega { get; set; }
        public virtual DateTime? DataEntrega { get; set; }
        public virtual SituacaoCompra SituacaoCompra { get; set; }

        public virtual PedidoCompra PedidoCompra { get; set; }
        public virtual Material Material { get; set; }
        public virtual UnidadeMedida UnidadeMedida { get; set; }

        public virtual PedidoCompraItemCancelado PedidoCompraItemCancelado { get; set; }

        public virtual double ObtenhaDiferenca()
        {
            return Quantidade - QuantidadeEntrega;
        }

        public virtual double ObtenhaValorTotal()
        {
            return ValorUnitario * Quantidade;
        }

        public virtual void AtualizeSituacao()
        {
            var quantidadeCancelada = PedidoCompraItemCancelado != null ? PedidoCompraItemCancelado.QuantidadeCancelada : 0;
            var quantidadeTotal = QuantidadeEntrega + quantidadeCancelada;
            
            if (quantidadeTotal == 0)
                SituacaoCompra = SituacaoCompra.NaoAtendido;
            else if (Quantidade.Equals(quantidadeCancelada))
                SituacaoCompra = SituacaoCompra.Cancelado;
            else if (Quantidade <= quantidadeTotal)
                SituacaoCompra = SituacaoCompra.AtendidoTotal;
            else if (Quantidade > quantidadeTotal)
                SituacaoCompra = SituacaoCompra.AtendidoParcial;
        }
    }
}
