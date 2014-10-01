using System;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Financeiro;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Financeiro.Models
{
    public class ContaBancariaModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Agência")]
        [Required(ErrorMessage = "Informe o número da agência")]
        [StringLength(6, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Agencia { get; set; }

        [Display(Name = "Nome da agência")]
        [StringLength(50, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string NomeAgencia { get; set; }

        [Display(Name = "Conta")]
        [Required(ErrorMessage = "Informe o número da conta")]
        [StringLength(8, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Conta { get; set; }

        [Display(Name = "Tipo conta")]
        [Required(ErrorMessage = "Informe o tipo da conta bancária")]
        public TipoContaBancaria? TipoContaBancaria { get; set; }

        [Display(Name = "Gerente")]
        [StringLength(50, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Gerente { get; set; }

        [Display(Name = "Abertura")]
        public DateTime? Abertura { get; set; }

        [Display(Name = "Telefone")]
        [StringLength(20, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Telefone { get; set; }

        [Display(Name = "Banco")]
        [Required(ErrorMessage = "Informe o banco")]
        public long? Banco { get; set; }
    }
}