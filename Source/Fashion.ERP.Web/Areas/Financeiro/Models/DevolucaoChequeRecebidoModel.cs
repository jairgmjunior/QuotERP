using System;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Financeiro;

namespace Fashion.ERP.Web.Areas.Financeiro.Models
{
    public class DevolucaoChequeRecebidoModel
    {
        public long? Id { get; set; }

        [Display(Name = "Unidade")]
        public string Unidade { get; set; }

        [Display(Name = "Cliente")]
        public string Cliente { get; set; }

        [Display(Name = "Banco")]
        public string Banco { get; set; }

        [Display(Name = "Agência")]
        public string Agencia { get; set; }

        [Display(Name = "Conta")]
        public string Conta { get; set; }

        [Display(Name = "Nº cheque")]
        public string NumeroCheque { get; set; }

        [Display(Name = "Situação cheque")]
        public string SituacaoCheque { get; set; }

        [Display(Name = "Emitente")]
        public string Emitente { get; set; }

        [Display(Name = "Data emissão")]
        public DateTime DataEmissao { get; set; }

        [Display(Name = "Data vencimento")]
        public DateTime DataVencimento { get; set; }

        [Display(Name = "Data prorrogação")]
        public DateTime? DataProrrogacao { get; set; }

        [Display(Name = "Valor")]
        public double Valor { get; set; }

        [Display(Name = "Data devolução")]
        public DateTime? DataDevolucao { get; set; }

        [Display(Name = "Motivo devolução")]
        public long? CompensacaoCheque { get; set; }

        [Display(Name = "Nova situação cheque")]
        public ChequeSituacao SituacaoChequeRecebido { get; set; }

        [Display(Name = "Observação")]
        [StringLength(4000, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Observacao { get; set; }
    }
}