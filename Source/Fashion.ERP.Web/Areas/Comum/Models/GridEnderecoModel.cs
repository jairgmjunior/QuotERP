using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class GridEnderecoModel
    {
        public long Id { get; set; }

        [Display(Name = "Tipo")]
        public string TipoEndereco { get; set; }

        [Display(Name = "Logradouro")]
        public string Logradouro { get; set; }
        
        [Display(Name = "Número")]
        public string Numero { get; set; }

        [Display(Name = "Complemento")]
        public string Complemento { get; set; }

        [Display(Name = "Bairro")]
        public string Bairro { get; set; }
        
        [Display(Name = "CEP")]
        public string Cep { get; set; }

        [Display(Name = "Cidade")]
        public string Cidade { get; set; } 
    }
}