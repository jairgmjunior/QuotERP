using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Financeiro.Models
{
    public class GridExtratoBancarioModel
    {
        public long Id { get; set; }
        
        [Display(Name = "Lançamento")]
        public string TipoLancamento { get; set; }

        [Display(Name = "Emissão")]
        public DateTime Emissao { get; set; }

        [Display(Name = "Compensação")]
        public DateTime? Compensacao { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Valor")]
        public double Valor { get; set; }

        [Display(Name = "Compensado")]
        public bool Compensado { get; set; }

        [Display(Name = "Conta bancária")]
        public string ContaBancaria { get; set; }
    }
}