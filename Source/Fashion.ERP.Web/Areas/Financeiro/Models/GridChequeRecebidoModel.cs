using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Financeiro.Models
{
    public class GridChequeRecebidoModel
    {
        public long Id { get; set; }

        [Display(Name = "Emitente")]
        public string Emitente { get; set; }

        [Display(Name = "Número")]
        public string NumeroCheque { get; set; }

        [Display(Name = "Valor")]
        public double Valor { get; set; }

        [Display(Name = "Saldo")]
        public double Saldo { get; set; }

        [Display(Name = "Vencimento")]
        public DateTime Vencimento { get; set; }
    }
}