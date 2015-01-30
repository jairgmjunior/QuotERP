using System;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Financeiro;

namespace Fashion.ERP.Web.Areas.Financeiro.Models
{
    public class GridDepositoChequeRecebido
    {
        public long Id { get; set; }

        [Display(Name = "U.E.")]
        public string Unidade { get; set; }
        
        [Display(Name = "Emitente")]
        public string Emitente { get; set; }

        [Display(Name = "Número")]
        public string NumeroCheque { get; set; }

        [Display(Name = "Banco")]
        public string Banco { get; set; }

        [Display(Name = "Valor")]
        public double Valor { get; set; }

        [Display(Name = "Vencimento")]
        public DateTime DataVencimento { get; set; }

        [Display(Name = "Situação")]
        public ChequeSituacao Situacao { get; set; }

        [Display(Name = "Depositar")]
        public bool Depositar { get; set; }

        public int CodigoCompensacaoCheque { get; set; }
    }
}