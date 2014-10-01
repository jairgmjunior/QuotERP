using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class GridItemPedidoCompraModel
    {
        public long Id { get; set; }

        public bool Autorizado { get; set; }

        [Display(Name = "Número")]
        public long Referencia { get; set; }

        [Display(Name = "Fornecedor")]
        public string Descricao { get; set; }

        [Display(Name = "Comprador")]
        public string MeioPagamento { get; set; }

        [Display(Name = "Data compra")]
        public DateTime DataCompra { get; set; }

        [Display(Name = "Data entrega")]
        public DateTime? DataEntrega { get; set; }

        [Display(Name = "Valor compra")]
        public double ValorCompra { get; set; }
    }
}