using System;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Compras;


namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class GridPedidoCompraItemDetalhe
    {
        public long? Id { get; set; }
        public long? PedidoCompraId { get; set; }
        [Display(Name = "Material")]
        public long? MaterialId { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Display(Name = "Referência")]
        public string Referencia { get; set; }
        [Display(Name = "Unidade")]
        public long? UnidadeEstocadora { get; set; }
        [Display(Name = "Qtde")]
        public double? Quantidade { get; set; }
        [Display(Name = "Qtde Entregue")]
        public double? QuantidadeEntregue { get; set; }
        [Display(Name = "Preço")]
        public double? ValorUnitario { get; set; }
        [Display(Name = "Desconto")]
        public double? ValorDesconto { get; set; }
        [Display(Name = "Valor Total")]
        public double? ValorTotal { get; set; }
        [Display(Name = "Entrega")]
        public DateTime? DataEntrega { get; set; }
        [Display(Name = "Referência Externa")]
        public string ReferenciaExterna { get; set; }
        [Display(Name = "Diferença")]
        public double? Diferenca { get; set; }
        [Display(Name = "Situação")]
        public SituacaoCompra Situacao { get; set; }
    }
}