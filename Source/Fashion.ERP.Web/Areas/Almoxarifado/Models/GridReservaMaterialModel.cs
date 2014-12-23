using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class GridReservaMaterialModel
    {
        public long Id { get; set; }

        [Display(Name = "Número")]
        public long Numero { get; set; }

        [Display(Name = "Ref. Origem")]
        public string Referencia { get; set; }

        [Display(Name = "Coleção")]
        public string Colecao { get; set; }

        [Display(Name = "Data")]
        public DateTime Data { get; set; }

        [Display(Name = "Data da Programação")]
        public DateTime DataProgramacao { get; set; }

        [Display(Name = "Situação")]
        public string Situacao { get; set; }
    }
}