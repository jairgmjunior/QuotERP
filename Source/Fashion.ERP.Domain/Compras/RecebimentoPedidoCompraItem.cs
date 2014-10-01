namespace Fashion.ERP.Domain.Compras
{
    public class RecebimentoPedidoCompraItem : DomainBase<RecebimentoCompraItem>
    {
        public virtual double Quantidade { get; set; }
        public virtual PedidoCompraItem PedidoCompraItem { get; set; } 
    }
}