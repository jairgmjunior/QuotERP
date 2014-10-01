namespace Fashion.ERP.Domain.Compras
{
    public class MotivoCancelamentoPedidoCompra : DomainBase<MotivoCancelamentoPedidoCompra>
    {
        public virtual string Descricao { get; set; }
        public virtual bool Ativo { get; set; }
    }
}