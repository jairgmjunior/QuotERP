using System;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Validators;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class ClienteModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Pessoa")]
        [Required(ErrorMessage = "Selecione o tipo de pessoa")]
        public TipoPessoa TipoPessoa { get; set; }

        [Display(Name = "CPF")]
        [UIHint("Cpf")]
        [StringLength(14, ErrorMessage = "{0} n�o deve ser maior que {1} caracteres")]
        [CpfCnpj(ErrorMessage = "CPF inv�lido.")]
        [RequiredIf("TipoPessoa", TipoPessoa.Fisica, ErrorMessage = "CPF � obrigat�rio.")]
        public string Cpf { get; set; }

        [Display(Name = "CNPJ")]
        [UIHint("Cnpj")]
        [StringLength(18, ErrorMessage = "{0} n�o deve ser maior que {1} caracteres")]
        [CpfCnpj(ErrorMessage = "CNPJ inv�lido.")]
        [RequiredIf("TipoPessoa", TipoPessoa.Juridica, ErrorMessage = "CNPJ � obrigat�rio.")]
        public string Cnpj { get; set; }

        [Display(Name = "Nome")]
        [StringLength(100, ErrorMessage = "{0} n�o deve ser maior que {1} caracteres")]
        [Required(ErrorMessage = "Informe o nome do cliente")]
        public string Nome { get; set; }

        [Display(Name = "Nome Fantasia")]
        [StringLength(100, ErrorMessage = "{0} n�o deve ser maior que {1} caracteres")]
        public string NomeFantasia { get; set; }

        [Display(Name = "Identidade")]
        [StringLength(20, ErrorMessage = "{0} n�o deve ser maior que {1} caracteres")]
        public string DocumentoIdentidade { get; set; }

        [Display(Name = "�rg�o Expedidor")]
        [StringLength(20, ErrorMessage = "{0} n�o deve ser maior que {1} caracteres")]
        public string OrgaoExpedidor { get; set; }

        [Display(Name = "Inscri��o Estadual")]
        [StringLength(20, ErrorMessage = "{0} n�o deve ser maior que {1} caracteres")]
        public string InscricaoEstadual { get; set; }

        [Display(Name = "Inscri��o Municipal")]
        [StringLength(20, ErrorMessage = "{0} n�o deve ser maior que {1} caracteres")]
        public string InscricaoMunicipal { get; set; }

        [Display(Name = "Inscri��o Suframa")]
        [StringLength(9, ErrorMessage = "{0} n�o deve ser maior que {1} caracteres")]
        public string InscricaoSuframa { get; set; }

        [Display(Name = "Data de Nascimento")]
        public DateTime? DataNascimento { get; set; }

        [Display(Name = "Site")]
        [DataType(DataType.Url)]
        [StringLength(100, ErrorMessage = "{0} n�o deve ser maior que {1} caracteres")]
        public string Site { get; set; }

        [Display(Name = "Foto")]
        public long? FotoId { get; set; }

        [Display(Name = "Foto")]
        public string FotoNome { get; set; }

        [Display(Name = "Sexo")]
        public Sexo ClienteSexo { get; set; }

        [Display(Name = "Estado Civil")]
        public EstadoCivil ClienteEstadoCivil { get; set; }

        [Display(Name = "Nome da M�e")]
        [StringLength(100, ErrorMessage = "{0} n�o deve ser maior que {1} caracteres")]
        public string ClienteNomeMae { get; set; }

        [Display(Name = "Data de Validade")]
        public DateTime? ClienteDataValidade { get; set; }

        [Display(Name = "Observa��o")]
        [DataType(DataType.MultilineText)]
        [StringLength(4000, ErrorMessage = "{0} n�o deve ser maior que {1} caracteres")]
        public string ClienteObservacao { get; set; }

        [Display(Name = "Profiss�o")]
        public long? ClienteProfissao { get; set; }

        [Display(Name = "�rea de Interesse")]
        public long? ClienteAreaInteresse { get; set; }
    }
}