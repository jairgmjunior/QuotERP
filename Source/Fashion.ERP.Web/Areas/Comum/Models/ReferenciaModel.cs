using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class ReferenciaModel : IModel
    {
        public long? Id { get; set; }
        public long Cliente { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "Selecione o tipo da referência")]
        public TipoReferencia TipoReferencia { get; set; }

        [Display(Name = "Nome")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        [Required(ErrorMessage = "Informe o nome da referência")]
        public string Nome { get; set; }

        [Display(Name = "Telefone")]
        [StringLength(20, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Telefone { get; set; }

        [Display(Name = "Celular")]
        [StringLength(20, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Celular { get; set; }

        [Display(Name = "Observação")]
        [StringLength(4000, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        [DataType(DataType.MultilineText)]
        public string Observacao { get; set; }
    }
}