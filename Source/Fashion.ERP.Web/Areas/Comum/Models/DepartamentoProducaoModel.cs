using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class DepartamentoProducaoModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe o nome")]
        [StringLength(50, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Nome { get; set; }

        [Display(Name = "Criação")]
        public virtual bool Criacao { get; set; }

        [Display(Name = "Produção")]
        public virtual bool Producao { get; set; }
    }
}