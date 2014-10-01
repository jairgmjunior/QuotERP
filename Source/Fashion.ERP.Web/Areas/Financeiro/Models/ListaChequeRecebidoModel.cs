using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Financeiro.Models
{
    public class ListaChequeRecebidoModel
    {
        [Display(Name = "Agência")]
        public string Agencia { get; set; }

        [Display(Name = "Conta")]
        public string Conta { get; set; }

        [Display(Name = "Número")]
        public string NumeroCheque { get; set; }

        [Display(Name = "Valor")]
        public double? Valor { get; set; }

        [Display(Name = "Nominal")]
        public string Nominal { get; set; }

        [Display(Name = "Emissão")]
        public DateTime? DataEmissaoInicio { get; set; }

        [Display(Name = "")]
        public DateTime? DataEmissaoFim { get; set; }

        [Display(Name = "Vencimento")]
        public DateTime? DataVencimentoInicio { get; set; }

        [Display(Name = "")]
        public DateTime? DataVencimentoFim { get; set; }

        [Display(Name = "Saldo")]
        public double? Saldo { get; set; }

        [Display(Name = "Compensado")]
        public bool? Compensado { get; set; }

        [Display(Name = "Cliente")]
        public long? Cliente { get; set; }

        [Display(Name = "Banco")]
        public long? Banco { get; set; }

        [Display(Name = "Emitente")]
        public string Emitente { get; set; }

        [Display(Name = "Unidade")]
        public long? Unidade { get; set; }

        [Display(Name = "Agrupar por")]
        public string Group { get; set; }

        [Display(Name = "Ordenar por")]
        public string Sort { get; set; }

        [Display(Name = "")]
        public string SortDirection { get; set; }
    }
}