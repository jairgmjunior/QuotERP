using System;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class InformacaoBancariaModel : IModel
    {
        public long? Id { get; set; }

        public long Pessoa { get; set; }

        [Display(Name = "Banco")]
        [Required(ErrorMessage = "Informe o banco")]
        public long Banco { get; set; }

        [Display(Name = "Agência")]
        [StringLength(6, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        [Required(ErrorMessage = "Informe o número da agência")]
        public string Agencia { get; set; }

        [Display(Name = "Conta")]
        [StringLength(20, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        [Required(ErrorMessage = "Informe o número da conta")]
        public string Conta { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "Informe o tipo de conta")]
        public TipoConta? TipoConta { get; set; }

        [Display(Name = "Data de abertura")]
        public DateTime? DataAbertura { get; set; }

        [Display(Name = "Titular")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Titular { get; set; }

        [Display(Name = "Telefone")]
        [StringLength(14, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Telefone { get; set; }
    }
}