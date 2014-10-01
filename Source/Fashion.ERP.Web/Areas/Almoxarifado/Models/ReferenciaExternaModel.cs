using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class ReferenciaExternaModel : IModel
    {
        public long? Id { get; set; }

        public long? Material { get; set; }

        [Display(Name = "Ref.")]
        [Required(ErrorMessage = "Informe a referência")]
        [StringLength(20, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Referencia { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Informe a descrição")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Descricao { get; set; }

        [StringLength(128, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string CodigoBarra { get; set; }
        
        public double? Preco { get; set; }

        [Display(Name = "Fornecedor")]
        [Required(ErrorMessage = "Informe o fornecedor")]
        public long? Fornecedor { get; set; }

        [Display(Name = "Fornecedor")]
        public string NomeFornecedor { get; set; }
    }
}