using System;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Validators;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class FuncionarioModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Pessoa")]
        [Required(ErrorMessage = "Selecione o tipo de pessoa")]
        public TipoPessoa TipoPessoa { get; set; }

        [Display(Name = "CPF")]
        [UIHint("Cpf")]
        [StringLength(14, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        [CpfCnpj(ErrorMessage = "CPF inválido.")]
        [Required(ErrorMessage = "Informe o CPF")]
        public string Cpf { get; set; }
        
        [Display(Name = "Nome")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        [Required(ErrorMessage = "Informe o nome do funcionário")]
        public string Nome { get; set; }

        [Display(Name = "Identidade")]
        [StringLength(20, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string DocumentoIdentidade { get; set; }

        [Display(Name = "Órgão Expeditor")]
        [StringLength(20, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string OrgaoExpedidor { get; set; }

        [Display(Name = "Inscrição Estadual")]
        [StringLength(20, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        [Required(ErrorMessage = "Informe o número da incrição estadual, caso não exista, informar ISENTO")]
        public string InscricaoEstadual { get; set; }

        [Display(Name = "Data de Nascimento")]
        public DateTime? DataNascimento { get; set; }

        [Display(Name = "Site")]
        [DataType(DataType.Url)]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Site { get; set; }

        [Display(Name = "Comissão (%)")]
        [Required(ErrorMessage = "Informe o percentual de comissão")]
        public double FuncionarioPercentualComissao { get; set; }

        [Display(Name = "Função do funcionário")]
        [Required(ErrorMessage = "Selecione a função do funcionário")]
        public FuncaoFuncionario FuncionarioFuncaoFuncionario { get; set; }
    }
}