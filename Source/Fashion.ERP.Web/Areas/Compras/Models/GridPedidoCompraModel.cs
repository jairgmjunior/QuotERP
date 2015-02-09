using System;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Compras;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class GridPedidoCompraModel
    {
        public long Id { get; set; }

        public bool Autorizado { get; set; }

        [Display(Name = "Número")]
        public long Numero { get; set; }

        [Display(Name = "Fornecedor")]
        public string Fornecedor { get; set; }

        [Display(Name = "Comprador")]
        public string Comprador { get; set; }

        [Display(Name = "Data compra")]
        public DateTime DataCompra { get; set; }

        [Display(Name = "Data entrega")]
        public DateTime? DataEntrega { get; set; }

        [Display(Name = "Valor da compra")]
        public double ValorCompra { get; set; }

        [Display(Name = "Situação")]
        public SituacaoCompra SituacaoCompra { get; set; }
    }
}