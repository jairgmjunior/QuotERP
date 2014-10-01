using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class GridOrdemEntradaCompraModel
    {
        public long Id { get; set; }

        [Display(Name = "Número")]
        public long Numero { get; set; }

        [Display(Name = "Data")]
        public DateTime Data { get; set; }

        [Display(Name = "Fornecedor")]
        public string Fornecedor { get; set; }

        [Display(Name = "Situação")]
        public string Situacao { get; set; }
    }
}