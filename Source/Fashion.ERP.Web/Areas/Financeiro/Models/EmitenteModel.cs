using System;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Financeiro.Models
{
    public class EmitenteModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Agência")]
        [StringLength(6, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Agencia { get; set; }

        [Display(Name = "Conta")]
        [StringLength(8, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Conta { get; set; }

        [Display(Name = "Nome")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Nome1 { get; set; }

        [UIHint("Cpf")]
        [Display(Name = "CPF")]
        [StringLength(14, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Cpf1 { get; set; }

        [UIHint("Cnpj")]
        [Display(Name = "CNPJ")]
        [StringLength(18, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Cnpj1 { get; set; }

        [Display(Name = "Documento")]
        [StringLength(20, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Documento1 { get; set; }

        [Display(Name = "Órgão Expedidor")]
        [StringLength(20, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string OrgaoExpedidor1 { get; set; }

        [Display(Name = "Nome")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Nome2 { get; set; }

        [UIHint("Cpf")]
        [Display(Name = "CPF")]
        [StringLength(14, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Cpf2 { get; set; }

        [UIHint("Cnpj")]
        [Display(Name = "CNPJ")]
        [StringLength(18, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Cnpj2 { get; set; }

        [Display(Name = "Documento")]
        [StringLength(20, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Documento2 { get; set; }

        [Display(Name = "Órgão Expedidor")]
        [StringLength(20, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string OrgaoExpedidor2 { get; set; }

        [Display(Name = "Cliente Desde")]
        public DateTime? ClienteDesde { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        [Display(Name = "Banco")]
        public long? Banco { get; set; }
    }
}