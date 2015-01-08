using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;
using System;

namespace Fashion.ERP.Web.Areas.Financeiro.Models
{
    public class BaixaChequeRecebidoModel : IModel
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

        [Display(Name = "Data vencimento")]
        public DateTime? DataVencimento { get; set; }

        [Display(Name = "Data prorrogação")]
        public DateTime? DataProrrogacao { get; set; }

        [Display(Name = "Valor")]
        public double ValorCheque { get; set; }

        [Display(Name = "Saldo")]
        public double SaldoCheque { get; set; }

        [Display(Name = "Data da baixa")]
        [Required(ErrorMessage = "Informe a data da baixa")]
        public DateTime? Data { get; set; }
        
        [Display(Name = "Valor do juros")]
        public double ValorJuros { get; set; }

        [Display(Name = "Despesas")]
        public double Despesa { get; set; }
        
        [Display(Name = "Desconto")]
        public double ValorDesconto { get; set; }
        
        [Display(Name = "Valor da baixa")]
        [Required(ErrorMessage = "Informe o valor")]
        [Range(0d, double.PositiveInfinity)]
        public double Valor { get; set; }

        [Display(Name = "Valor total")]
        public double ValorTotal { get; set; }

        [Display(Name = "Histórico")]
        [StringLength(4000, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Historico { get; set; }
        
        [Display(Name = "Observação")]
        [StringLength(4000, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Observacao { get; set; }

        [Display(Name = "Cheque")]
        public long ChequeRecebido { get; set; }

        public List<ListaBaixaChequeRecebidoModel> BaixasRealizadas { get; set; }
    }
}