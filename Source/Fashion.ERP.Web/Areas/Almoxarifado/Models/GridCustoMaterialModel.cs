using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class GridCustoMaterialModel
    {
        [Display(Name = "Data")]
        public DateTime Data { get; set; }

        [Display(Name = "Fornecedor")]
        public string Fornecedor { get; set; }

        [Display(Name = "Custo")]
        public double Custo { get; set; }
    }
}