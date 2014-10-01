using System;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class FichaFuncionarioModel
    {
        [Display(Name = "Pessoa")]
        public TipoPessoa? TipoPessoa { get; set; }

        [Display(Name = "CPF")]
        [UIHint("Cpf")]
        [StringLength(14, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Cpf { get; set; }

        [Display(Name = "Nome")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Nome { get; set; }

        [Display(Name = "Identidade")]
        [StringLength(20, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string DocumentoIdentidade { get; set; }

        [Display(Name = "Órgão Exp.")]
        [StringLength(20, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string OrgaoExpedidor { get; set; }

        [Display(Name = "Insc Estadual")]
        [StringLength(20, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string InscricaoEstadual { get; set; }

        [Display(Name = "Nascimento")]
        public DateTime? DataNascimento { get; set; }

        [Display(Name = "Código")]
        public long? Codigo { get; set; }

        [Display(Name = "Função")]
        public FuncaoFuncionario? FuncaoFuncionario { get; set; }
    }
}