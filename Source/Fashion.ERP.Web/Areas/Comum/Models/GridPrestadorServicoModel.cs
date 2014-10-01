using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class GridPrestadorServicoModel
    {
        public long Id { get; set; }
        
        [Display(Name = "Código")]
        public long Codigo { get; set; }
        
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        
        [Display(Name = "CPF/CNPJ")]
        public string CpfCnpj { get; set; }

        [Display(Name = "Data de cadastro")]
        public DateTime DataCadastro { get; set; }

        public bool Ativo { get; set; }
    }
}