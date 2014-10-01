using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class GridDetalharOrdemEntrada
    {
        public long Id { get; set; }

        [Display(Name = "Data")]
        public DateTime Data { get; set; }

        [Display(Name = "Quantidade")]
        public int Quantidade { get; set; }

        [Display(Name = "Situação")]
        public string Situacao { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; } 
    }
}