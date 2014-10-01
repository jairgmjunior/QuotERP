using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class GridClienteModel
    {
        public long Id { get; set; }

        [Display(Name = "Código")]
        public long Codigo { get; set; }
        
        [Display(Name = "CPF/CNPJ")]
        public string CpfCnpj { get; set; }
        
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        
        [Display(Name = "Cadastrado em")]
        public DateTime DataCadastro { get; set; }
    }
}