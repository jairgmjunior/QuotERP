using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class GridInformacaoBancariaModel
    {
        public long Id { get; set; }

        [Display(Name = "Banco")]
        public string Banco { get; set; }
        
        [Display(Name = "Agência")]
        public string Agencia { get; set; }
        
        [Display(Name = "Conta")]
        public string Conta { get; set; }
        
        [Display(Name = "Tipo")]
        public string TipoConta { get; set; }
        
        [Display(Name = "Data de abertura")]
        public string DataAbertura { get; set; }
        
        [Display(Name = "Titular")]
        public string Titular { get; set; }
        
        [Display(Name = "Telefone")]
        public string Telefone { get; set; } 
    }
}