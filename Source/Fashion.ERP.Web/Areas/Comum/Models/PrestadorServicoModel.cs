using System;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Validators;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class PrestadorServicoModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Pessoa")]
        [Required(ErrorMessage = "Selecione o tipo de pessoa")]
        public TipoPessoa TipoPessoa { get; set; }

        [Display(Name = "CPF")]
        [UIHint("Cpf")]
        [StringLength(14, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        [CpfCnpj(ErrorMessage = "CPF inválido.")]
        public string Cpf { get; set; }

        [Display(Name = "CNPJ")]
        [UIHint("Cnpj")]
        [StringLength(18, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        [CpfCnpj(ErrorMessage = "CNPJ inválido.")]
        public string Cnpj { get; set; }

        [Display(Name = "Nome")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        [Required(ErrorMessage = "Informe o nome do prestador de serviço")]
        public string Nome { get; set; }

        [Display(Name = "Nome Fantasia")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string NomeFantasia { get; set; }

        [Display(Name = "Identidade")]
        [StringLength(20, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string DocumentoIdentidade { get; set; }

        [Display(Name = "Órgão Expeditor")]
        [StringLength(20, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string OrgaoExpedidor { get; set; }

        [Display(Name = "Inscrição Estadual")]
        [StringLength(20, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string InscricaoEstadual { get; set; }

        [Display(Name = "Inscrição Municipal")]
        [StringLength(20, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string InscricaoMunicipal { get; set; }

        [Display(Name = "Inscrição Suframa")]
        [StringLength(9, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string InscricaoSuframa { get; set; }

        [Display(Name = "Data de Nascimento")]
        public DateTime? DataNascimento { get; set; }

        [Display(Name = "Site")]
        [DataType(DataType.Url)]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Site { get; set; }

        [Display(Name = "Comissão (%)")]
        [Required(ErrorMessage = "Informe o valor da comissão")]
        public double PrestadorServicoComissao { get; set; }

        [Display(Name = "Funções")]
        public TipoPrestadorServico[] PrestadorServicoTipoPrestadorServicos { get; set; }

        [Display(Name = "Unidade")]
        [Required(ErrorMessage = "Informe a unidade do prestador de serviço")]
        public long PrestadorServicoUnidade { get; set; }
    }
}