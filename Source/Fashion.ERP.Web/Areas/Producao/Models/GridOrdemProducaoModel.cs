using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class GridOrdemProducaoModel
    {
        public long Id { get; set; }

        [Display(Name = "Número")]
        public long Numero { get; set; }

        [Display(Name = "Tag")]
        public string Tag { get; set; }

        [Display(Name = "Ano")]
        public long Ano { get; set; }

        [Display(Name = "Data")]
        public DateTime Data { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Situação")]
        public string Situacao { get; set; }

        [Display(Name = "Foto")]
        public string Foto { get; set; }
    }
}