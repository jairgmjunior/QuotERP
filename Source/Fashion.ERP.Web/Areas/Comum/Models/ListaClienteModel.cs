using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class ListaClienteModel
    {
        [Display(Name = "Pessoa")]
        public TipoPessoa? TipoPessoa { get; set; }

        [Display(Name = "CPF")]
        [UIHint("Cpf")]
        [StringLength(14, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Cpf { get; set; }

        [Display(Name = "CNPJ")]
        [UIHint("Cnpj")]
        [StringLength(18, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Cnpj { get; set; }

        [Display(Name = "Nome")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Nome { get; set; }

        [Display(Name = "Código")]
        public long? Codigo { get; set; }
    }
}