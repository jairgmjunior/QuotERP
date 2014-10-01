using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class ContatoModel : IModel
    {
        public long? Id { get; set; }
        public long Pessoa { get; set; }

        [Display(Name = "Tipo Contato")]
        public TipoContato TipoContato { get; set; }

        [Display(Name = "Nome")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        [Required(ErrorMessage = "Informe o nome do contato")]
        public string Nome { get; set; }

        [Display(Name = "Telefone")]
        [StringLength(14, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Telefone { get; set; }

        [Display(Name = "Operadora")]
        [StringLength(20, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Operadora { get; set; }

        [Display(Name = "Email")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Email { get; set; }
    }
}