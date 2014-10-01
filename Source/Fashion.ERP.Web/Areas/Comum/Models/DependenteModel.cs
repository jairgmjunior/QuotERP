using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Validators;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class DependenteModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Cliente")]
        public long Cliente { get; set; }
        
        [Display(Name = "Grau de dependência")]
        [Required(ErrorMessage = "Informe o grau de dependência")]
        public long? GrauDependencia { get; set; }
        
        [Display(Name = "Nome")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        [Required(ErrorMessage = "Informe o nome do dependente")]
        public string Nome { get; set; }
        
        [Display(Name = "CPF")]
        [UIHint("Cpf")]
        [StringLength(14, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        [Required(ErrorMessage = "Informe o número do CPF")]
        [CpfCnpj(ErrorMessage = "CPF inválido.")]
        public string Cpf { get; set; }
        
        [Display(Name = "RG")]
        [StringLength(20, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        [Required(ErrorMessage = "Informe o RG")]
        public string Rg { get; set; }
        
        [Display(Name = "Órgão expedidor")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        [Required(ErrorMessage = "Informe o órgão expedidor")]
        public string OrgaoExpedidor { get; set; }
    }
}