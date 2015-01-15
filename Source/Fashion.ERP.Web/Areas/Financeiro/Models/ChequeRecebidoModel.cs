using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Financeiro;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Financeiro.Models
{
    public class ChequeRecebidoModel : IModel, IChequeRecebidoDropdownModel
    {
        public ChequeRecebidoModel()
        {
            Cmc7s = new List<string>();
            Bancos = new List<string>();
            IdBancos = new List<long>();
            Agencias = new List<string>();
            Contas = new List<string>();
            Cheques = new List<string>();
            Vencimentos = new List<DateTime>();
            IdEmitentes = new List<long?>();
            Pracas = new List<string>();
            Valores = new List<double>();
        }

        public long? Id { get; set; }

        [Display(Name = "Situação")]
        [Required(ErrorMessage = "Informe a situação do cheque")]
        public ChequeSituacao Situacao { get; set; }

        [Display(Name = "Comp")]
        public int? Comp { get; set; }

        [Display(Name = "Agência")]
        [StringLength(6, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Agencia { get; set; }

        [Display(Name = "Conta")]
        [StringLength(8, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Conta { get; set; }
        
        [Display(Name = "Número cheque")]
        [StringLength(6, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string NumeroCheque { get; set; }
        
        [Display(Name = "CMC-7")]
        [StringLength(30, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Cmc7 { get; set; }
        
        [Display(Name = "Valor")]
        public double Valor { get; set; }
        
        [Display(Name = "Nominal")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Nominal { get; set; }
        
        [Display(Name = "Emissão")]
        [Required(ErrorMessage = "Informe a data de emissão")]
        public DateTime? DataEmissao { get; set; }
        
        [Display(Name = "Vencimento")]
        public DateTime? DataVencimento { get; set; }
        
        [Display(Name = "Prorrogação")]
        public DateTime? DataProrrogacao { get; set; }
        
        [Display(Name = "Praça")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Praca { get; set; }
        
        [Display(Name = "Histórico")]
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
        public long? Banco { get; set; }
        
        [Display(Name = "Emitentes")]
        public long? Emitente { get; set; }

        [Display(Name = "Unidade")]
        [Required(ErrorMessage = "Informe a unidade que recebeu o cheque.")]
        public long? Unidade { get; set; }

        // Baixas
        public IList<string> Cmc7s { get; set; }
        public IList<long> IdBancos { get; set; }
        public IList<string> Bancos { get; set; }
        public IList<string> Agencias { get; set; }
        public IList<string> Contas { get; set; }
        public IList<string> Cheques { get; set; }
        public IList<DateTime> Vencimentos { get; set; }
        public IList<long?> IdEmitentes { get; set; }
        public IList<string> Emitentes { get; set; }
        public IList<string> Pracas { get; set; }
        public IList<double> Valores { get; set; }
    }
}