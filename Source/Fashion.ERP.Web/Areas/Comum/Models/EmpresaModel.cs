using System;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Validators;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class EmpresaModel : IModel
    {
        //Id de pessoa
        public long? Id { get; set; }

        [Display(Name = "Pessoa")]
        [Required(ErrorMessage = "Selecione o tipo de pessoa")]
        public TipoPessoa TipoPessoa { get; set; }
        
        [Display(Name = "CNPJ")]
        [UIHint("Cnpj")]
        [StringLength(18, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        [CpfCnpj(ErrorMessage = "CNPJ inválido.")]
        [Required(ErrorMessage = "Informe o CNPJ")]
        public string Cnpj { get; set; }

        [Display(Name = "Nome/Razão Social")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        [Required(ErrorMessage = "Informe o nome/razão social da empresa")]
        public string Nome { get; set; }

        [Display(Name = "Nome Fantasia")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        [Required(ErrorMessage = "Informe o nome fantasia da empresa")]
        public string NomeFantasia { get; set; }

        [Display(Name = "Data de Fundação")]
        public DateTime? DataFundacao { get; set; }

        [Display(Name = "Inscrição Estadual")]
        [StringLength(20, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        [Required(ErrorMessage = "Informe a inscrição estadual da empresa")]
        public string InscricaoEstadual { get; set; }

        [Display(Name = "Inscrição Municipal")]
        [StringLength(20, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        [Required(ErrorMessage = "Informe a inscrição municipal da empresa")]
        public string InscricaoMunicipal { get; set; }
    }
}