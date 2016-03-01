using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Financeiro;

namespace Fashion.ERP.Web.Areas.Financeiro.Models
{
    public class DepositoChequeRecebidoModel
    {
        public DepositoChequeRecebidoModel()
        {
            Situacao = new List<ChequeSituacao>();
        }
        [Display(Name = "Unidade Est")]
        public long? Unidade { get; set; }

        [Display(Name = "Data Vencimento")]
        public DateTime? VencimentoDe { get; set; }

        [Display(Name = "Até")]
        public DateTime? VencimentoAte { get; set; }

        [Display(Name = "Situação")]
        public List<ChequeSituacao> Situacao { get; set; }

        [Display(Name = "Banco")]
        [Required(ErrorMessage = "Informe o banco")]
        public long? Banco { get; set; }

        [Display(Name = "Agência/Conta")]
        [Required(ErrorMessage = "Informe o banco")]
        public long? ContaBancaria { get; set; }

        [Display(Name = "Data de Depósito")]
        [Required(ErrorMessage = "Informe a data de depósito")]
        public DateTime? DataDeposito { get; set; }

        public List<long> Cheques { get; set; }
    }
}