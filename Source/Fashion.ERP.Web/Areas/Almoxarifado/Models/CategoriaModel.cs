using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class CategoriaModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe o nome")]
        [StringLength(60, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Nome { get; set; }

        [Display(Name = "Código NCM")]
        [Required(ErrorMessage = "Informe o Código NCM")]
        [StringLength(8, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string CodigoNcm { get; set; }

        [Display(Name = "Gênero")]
        [Required(ErrorMessage = "Informe o Gênero da categoria")]
        public GeneroCategoria GeneroCategoria { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "Informe o Tipo da categoria")]
        public TipoCategoria TipoCategoria { get; set; }
    }
}