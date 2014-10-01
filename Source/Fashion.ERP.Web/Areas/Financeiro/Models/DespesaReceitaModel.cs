using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Financeiro;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Financeiro.Models
{
    public class DespesaReceitaModel: IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Informe a descrição")]
        [StringLength(60, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Descricao { get; set; }

        [Display(Name = "Ativo")]
        [Required(ErrorMessage = "Infome a opção ativo/inativo")]
        public bool Ativo { get; set; }

        [Display(Name = "Tipo Despesa/Receita")]
        [Required(ErrorMessage = "Informe o tipo da despesa/receita")]
        public TipoDespesaReceita TipoDespesaReceita { get; set; }
    }
}