namespace Fashion.ERP.Domain.Compras
{
    public class DetalhamentoRecebimentoCompraItem : DomainBase<PedidoCompraItem>
    {
        public virtual double Quantidade { get; set; }
        public virtual PedidoCompra PedidoCompra { get; set; }
        public virtual PedidoCompraItem PedidoCompraItem { get; set; }
        
        public virtual void CalculeQuantidade(ref double quantidadeEntradaDisponivel)
        {
            var retorno = quantidadeEntradaDisponivel;
            quantidadeEntradaDisponivel = 0;
            Quantidade =  retorno;
        }
    }
}