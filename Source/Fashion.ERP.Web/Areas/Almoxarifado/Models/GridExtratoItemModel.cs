using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class GridExtratoItemModel
    {
        [Display(Name = "Data")]
        public DateTime Data { get; set; }

        [Display(Name = "Entrada")]
        public double? Entrada { get; set; }

        [Display(Name = "Saída")]
        public double? Saida { get; set; }
        
        [Display(Name = "Origem/Destino")]
        public String OrigemDestino { get; set; }
    }
}