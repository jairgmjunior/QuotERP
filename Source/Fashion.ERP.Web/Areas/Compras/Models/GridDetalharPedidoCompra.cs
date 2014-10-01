using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class GridDetalharPedidoCompra
    {
        public long Id { get; set; }

        [Display(Name = "Número")]
        public long Numero { get; set; }

        [Display(Name = "Data")]
        public DateTime Data { get; set; }

        [Display(Name = "Referência")]
        public string Referencia { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "UND")]
        public string UnidadeMedida { get; set; }

        [Display(Name = "Qtde")]
        public double Quantidade { get; set; }

        [Display(Name = "Valor")]
        public double ValorUnitario { get; set; }

        [Display(Name = "Total")]
        public double ValorTotal { get; set; }
    }
}