using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class GridSequenciaProducaoModel
    {
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Entrada")]
        public DateTime? Entrada { get; set; }

        [Display(Name = "Saída")]
        public DateTime? Saida { get; set; }
    }
}