using System;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Financeiro.Models
{
    public class ChequeRecebidoModel : IModel, IChequeRecebidoDropdownModel
    {
        public long? Id { get; set; }

        [Display(Name = "Comp")]
        public int? Comp { get; set; }

        [Display(Name = "Agência")]
        [Required(ErrorMessage = "Informe o número da agência")]
        [StringLength(6, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Agencia { get; set; }

        [Display(Name = "Conta")]
        [Required(ErrorMessage = "Informe o número da conta")]
        [StringLength(8, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Conta { get; set; }
        
        [Display(Name = "Número cheque")]
        [Required(ErrorMessage = "Informe o número do cheque")]
        [StringLength(6, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string NumeroCheque { get; set; }
        
        [Display(Name = "CMC-7")]
        [StringLength(30, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Cmc7 { get; set; }
        
        [Display(Name = "Valor")]
        [Required(ErrorMessage = "Informe o valor do cheque")]
        public double Valor { get; set; }
        
        [Display(Name = "Nominal")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Nominal { get; set; }
        
        [Display(Name = "Emissão")]
        [Required(ErrorMessage = "Informe a data de emissão")]
        public DateTime? DataEmissao { get; set; }
        
        [Display(Name = "Vencimento")]
        [Required(ErrorMessage = "Informe a data de vencimento")]
        public DateTime? DataVencimento { get; set; }
        
        [Display(Name = "Prorrogação")]
        public DateTime? DataProrrogacao { get; set; }
        
        [Display(Name = "Praça")]
        [Required(ErrorMessage = "Informe a praça")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Praca { get; set; }
        
        [Display(Name = "Histórico")]
        [DataType(DataType.MultilineText)]
        [StringLength(4000, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Historico { get; set; }
        
        [Display(Name = "Observação")]
        [DataType(DataType.MultilineText)]
        [StringLength(4000, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Observacao { get; set; }
        
        [Display(Name = "Saldo")]
        public double? Saldo { get; set; }
        
        [Display(Name = "Compensado")]
        public bool Compensado { get; set; }
        
        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Informe o cliente")]
        public long? Cliente { get; set; }

        [Display(Name = "Banco")]
        [Required(ErrorMessage = "Informe o banco")]
        public long? Banco { get; set; }
        
        [Display(Name = "Emitentes")]
        [Required(ErrorMessage = "Informe o(s) emitente(s) do cheque.")]
        public long? Emitente { get; set; }

        [Display(Name = "Unidade")]
        [Required(ErrorMessage = "Informe a unidade que recebeu o cheque.")]
        public long? Unidade { get; set; }
    }
}