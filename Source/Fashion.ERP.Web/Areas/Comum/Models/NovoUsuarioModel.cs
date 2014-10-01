using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class NovoUsuarioModel : UsuarioModel
    {
        [Display(Name = "Senha")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        [StringLength(8, MinimumLength = 4,ErrorMessage = "{0} deve conter entre {1} e {2} caracteres")]
        public string Senha { get; set; }

        [Display(Name = "Confirmar")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        [Compare("Senha", ErrorMessage = "As senhas não coincidem.")]
        public string ConfirmarSenha { get; set; }

        [Display(Name = "Funcionário")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        public long Funcionario { get; set; }
    }
}