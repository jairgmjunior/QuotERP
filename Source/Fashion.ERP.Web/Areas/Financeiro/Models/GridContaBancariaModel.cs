using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Financeiro.Models
{
    public class GridContaBancariaModel
    {
        public long Id { get; set; }

        [Display(Name = "Agência")]
        public string Agencia { get; set; }

        [Display(Name = "Nome da agência")]
        public string NomeAgencia { get; set; }

        [Display(Name = "Conta")]
        public string Conta { get; set; }

        [Display(Name = "Tipo conta")]
        public string TipoContaBancaria { get; set; }

        [Display(Name = "Gerente")]
        public string Gerente { get; set; }

        [Display(Name = "Abertura")]
        public DateTime? Abertura { get; set; }

        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        [Display(Name = "Banco")]
        public string Banco { get; set; }
    }
}