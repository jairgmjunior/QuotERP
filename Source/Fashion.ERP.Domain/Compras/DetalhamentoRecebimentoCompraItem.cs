namespace Fashion.ERP.Domain.Compras
{
    public class DetalhamentoRecebimentoCompraItem : DomainBase<PedidoCompraItem>
    {
        public virtual double Quantidade { get; set; }
        public virtual PedidoCompra PedidoCompra { get; set; }
        public virtual PedidoCompraItem PedidoCompraItem { get; set; }
        
        //public virtual void CalculeQuantidade(ref double quantidadeEntradaDisponivel)
        //{
        //    var retorno = quantidadeEntradaDisponivel;
        //    quantidadeEntradaDisponivel = 0;
        //    Quantidade =  retorno;
        //}

        //public virtual void CalculeQuantidade(ref double quantidadeEntradaDisponivel)
        //{
        //    if (quantidadeEntradaDisponivel >= PedidoCompraItem.Quantidade)
        //    {
        //        quantidadeEntradaDisponivel = quantidadeEntradaDisponivel - PedidoCompraItem.Quantidade;
        //        Quantidade = PedidoCompraItem.Quantidade;
        //        return;
        //    }

        //    var retorno = quantidadeEntradaDisponivel;
        //    quantidadeEntradaDisponivel = 0;
        //    Quantidade = retorno;
        //}
    }
}