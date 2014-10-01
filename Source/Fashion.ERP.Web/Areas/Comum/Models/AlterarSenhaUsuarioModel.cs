using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class AlterarSenhaUsuarioModel
    {
        public long Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Senha atual")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "{0} deve conter entre {1} e {2} caracteres")]
        public string SenhaAtual { get; set; }

        [Display(Name = "Nova senha")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "{0} deve conter entre {1} e {2} caracteres")]
        public string Senha { get; set; }

        [Display(Name = "Confirmar")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        [Compare("Senha", ErrorMessage = "As senhas não coincidem.")]
        public string ConfirmarSenha { get; set; }
    }
}