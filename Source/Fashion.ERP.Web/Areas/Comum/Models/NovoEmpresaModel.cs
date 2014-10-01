using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class NovoEmpresaModel : EmpresaModel
    {
        public long? IdEndereco { get; set; }

        [Display(Name = "Tipo Endereço")]
        [Required(ErrorMessage = "Informe o tipo de endereço")]
        public TipoEndereco EnderecoTipoEndereco { get; set; }

        [Display(Name = "Logradouro")]
        [Required(ErrorMessage = "Informe o logradouro")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string EnderecoLogradouro { get; set; }

        [Display(Name = "Número")]
        public string EnderecoNumero { get; set; }

        [Display(Name = "Complemento")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string EnderecoComplemento { get; set; }

        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "Informe o bairro")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string EnderecoBairro { get; set; }

        [Display(Name = "CEP")]
        [UIHint("Cep")]
        [Required(ErrorMessage = "Informe o CEP")]
        [StringLength(9, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string EnderecoCep { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "Informe a cidade")]
        public long EnderecoCidade { get; set; }

        [Display(Name = "Tipo Contato")]
        [Required(ErrorMessage = "Informe o tipo de contato")]
        public TipoContato ContatoTipoContato { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe o nome do contato")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string ContatoNome { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "Informe o telefone")]
        [StringLength(14, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string ContatoTelefone { get; set; }

        [Display(Name = "Operadora")]
        [StringLength(20, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string ContatoOperadora { get; set; }

        [Display(Name = "Email")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string ContatoEmail { get; set; } 
    }
}