namespace Fashion.ERP.Domain.Compras
{
    public class ParametroModuloCompra : DomainBase<ParametroModuloCompra>
    {
        public virtual bool ValidaRecebimentoPedido { get; set; }
        public virtual double PercentualCriacaoPedidoAutorizadoRecebimento { get; set; }
    }
}