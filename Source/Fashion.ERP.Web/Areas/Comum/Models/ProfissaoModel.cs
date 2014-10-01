using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class ProfissaoModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Nome")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        [Required(ErrorMessage = "Informe o nome da profissão")]
        public string Nome { get; set; }
    }
}