using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class ClassificacaoDificuldadeModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Informe a descrição")]
        [StringLength(50, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Descricao { get; set; }

        [Display(Name = "Criação")]
        public bool Criacao { get; set; }

        [Display(Name = "Produção")]
        public bool Producao { get; set; }
    }
}