using System;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Financeiro;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Financeiro.Models
{
    public class TituloReceberModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Unidade")]
        [Required(ErrorMessage = "Informe a unidade")]
        public long? Unidade { get; set; }

        [Display(Name = "Situação título")]
        public SituacaoTitulo SituacaoTitulo { get; set; }

        [Display(Name = "Número")]
        [Required(ErrorMessage = "Informe o número do título")]
        [StringLength(30, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Numero { get; set; }

        [Display(Name = "Parcela")]
        [Required(ErrorMessage = "Informe a parcela")]
        public int? Parcela { get; set; }

        [Display(Name = "Plano")]
        [Required(ErrorMessage = "Informe o plano")]
        public int Plano { get; set; }

        [Display(Name = "Emissão")]
        [Required(ErrorMessage = "Informe a emissão")]
        public DateTime? Emissao { get; set; }

        [Display(Name = "Vencimento")]
        [Required(ErrorMessage = "Informe o vencimento")]
        public DateTime? Vencimento { get; set; }

        [Display(Name = "Prorrogação")]
        [Required(ErrorMessage = "Informe a data de prorrogação")]
        public DateTime? Prorrogacao { get; set; }

        [Display(Name = "Valor título")]
        [Required(ErrorMessage = "Informe o valor do título")]
        public double? Valor { get; set; }

        [Display(Name = "Saldo título")]
        [Required(ErrorMessage = "Informe o saldo do título")]
        public double? SaldoDevedor { get; set; }

        [Display(Name = "Valor despesas")]
        [Required(ErrorMessage = "Informe o valor das despesas")]
        public double? ValorDespesas { get; set; }

        [Display(Name = "Valor total")]
        public double? ValorTotal { get; set; }

        [Display(Name = "Histórico")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Historico { get; set; }

        [Display(Name = "Observação")]
        [StringLength(4000, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        [DataType(DataType.MultilineText)]
        public string Observacao { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Informe o cliente")]
        public long? Cliente { get; set; }

        [Display(Name = "Funcionário")]
        [Required(ErrorMessage = "Informe o funcionário")]
        public long? Funcionario { get; set; }

        [Display(Name = "Banco")]
        [Required(ErrorMessage = "Informe o banco")]
        public long? Banco { get; set; }
    }
}