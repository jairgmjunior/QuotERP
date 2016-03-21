using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Models
{
    public class LoginModel
    {
        [Display(Name = "Usuário")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        [StringLength(50, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Usuario { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        [StringLength(50, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Senha { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "Permanecer Logado")]
        public bool PermanecerLogado { get; set; }
    }
}