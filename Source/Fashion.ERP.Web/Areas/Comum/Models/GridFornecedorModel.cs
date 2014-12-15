using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class GridFornecedorModel
    {
        public long Id { get; set; }
        
        [Display(Name = "Código")]
        public long Codigo { get; set; }
        
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "CPF/CNPJ")]
        public string CpfCnpj { get; set; }

        [Display(Name = "NomeFantasia")]
        public string NomeFantasia { get; set; }
        
        [Display(Name = "Tipo")]
        public string TipoFornecedor { get; set; }
        
        [Display(Name = "Estado")]
        public string Estado { get; set; }
        
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }
        
        public bool Ativo { get; set; }
    }
}