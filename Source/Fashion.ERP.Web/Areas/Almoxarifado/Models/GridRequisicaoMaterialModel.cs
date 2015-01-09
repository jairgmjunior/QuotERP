using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class GridRequisicaoMaterialModel
    {
        public long Id { get; set; }

        [Display(Name = "Número")]
        public long Numero { get; set; }

        [Display(Name = "Origem")]
        public string Origem { get; set; }

        [Display(Name = "Unidade")]
        public string UnidadeRequerente { get; set; }

        [Display(Name = "Requerente")]
        public string Requerente { get; set; }

        [Display(Name = "Tipo de Material")]
        public string TipoMaterial { get; set; }

        [Display(Name = "Data")]
        public DateTime Data { get; set; }

        [Display(Name = "Situação")]
        public string Situacao { get; set; }
    }
}