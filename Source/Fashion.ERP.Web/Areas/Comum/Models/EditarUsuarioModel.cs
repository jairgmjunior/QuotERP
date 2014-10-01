using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class EditarUsuarioModel : UsuarioModel
    {
        [Display(Name = "Resetar senha")]
        public bool ResetarSenha { get; set; }

        [Display(Name = "Nova senha")]
        public string NovaSenha { get; set; }
    }
}