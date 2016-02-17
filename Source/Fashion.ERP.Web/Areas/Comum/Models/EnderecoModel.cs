using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class EnderecoModel : IModel
    {
        public long? Id { get; set; }
        public long Pessoa { get; set; }

        [Display(Name = "Tipo do endereço")]
        [Required(ErrorMessage = "Selecione um tipo de endereço")]
        public TipoEndereco TipoEndereco { get; set; }
        
        [Display(Name = "Logradouro")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        [Required(ErrorMessage = "Informe o logradouro")]
        public string Logradouro { get; set; }
        
        [Display(Name = "Número")]
        [StringLength(10, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Numero { get; set; }
        
        [Display(Name = "Complemento")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Complemento { get; set; }
        
        [Display(Name = "Bairro")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        [Required(ErrorMessage = "Informe o bairro")]
        public string Bairro { get; set; }
        
        [Display(Name = "CEP")]
        [UIHint("Cep")]
        [Required(ErrorMessage = "Informe o CEP")]
        public string Cep { get; set; }
        
        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "Informe a Cidade")]
        public long Cidade { get; set; } 
    }
}