using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class MaterialModel : IModel, IMaterialDropdownModel
    {
        public long? Id { get; set; }

        [Display(Name = "Referência")]
        [Required(ErrorMessage = "Informe a referência")]
        [StringLength(20, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Referencia { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Informe a descrição")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Descricao { get; set; }

        [Display(Name = "Detalhamento")]
        [Required(ErrorMessage = "Informe o detalhamento")]
        [StringLength(200, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        [DataType(DataType.MultilineText)]
        public string Detalhamento { get; set; }

        [Display(Name = "Código de barras")]
        [StringLength(128, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string CodigoBarra { get; set; }

        [Display(Name = "NCM")]
        [Required(ErrorMessage = "Informe o NCM")]
        [StringLength(8, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Ncm { get; set; }

        [Display(Name = "Alíquota")]
        public double Aliquota { get; set; }

        [Display(Name = "Peso bruto")]
        public double PesoBruto { get; set; }

        [Display(Name = "Peso líquido")]
        public double PesoLiquido { get; set; }

        [Display(Name = "Origem de situação tributária")]
        [Required(ErrorMessage = "Informe a origem de situação tributária")]
        public long? OrigemSituacaoTributaria { get; set; }

        [Display(Name = "Localização")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Localizacao { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        [Display(Name = "Foto")]
        public long? FotoId { get; set; }

        public string FotoNome { get; set; }

        [Display(Name = "Gênero fiscal")]
        [Required(ErrorMessage = "Informe o gênero fiscal")]
        public long? GeneroFiscal { get; set; }

        [Display(Name = "Unidade de medida")]
        [Required(ErrorMessage = "Informe a unidade de medida")]
        public long? UnidadeMedida { get; set; }

        [Display(Name = "Marca do material")]
        [Required(ErrorMessage = "Informe a marca do material")]
        public long? MarcaMaterial { get; set; }

        [Display(Name = "Família")]
        [Required(ErrorMessage = "Informe a família")]
        public long? Familia { get; set; }

        [Display(Name = "Tipo do item")]
        [Required(ErrorMessage = "Informe o tipo do item")]
        public long? TipoItem { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "Informe a categoria")]
        public long? Categoria { get; set; }

        [Display(Name = "Subcategoria")]
        [Required(ErrorMessage = "Informe a subcategoria")]
        public long? Subcategoria { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string BordadoDescricao { get; set; }

        [Display(Name = "Pontos")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string BordadoPontos { get; set; }

        [Display(Name = "Aplicação")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string BordadoAplicacao { get; set; }

        [Display(Name = "Observação")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string BordadoObservacao { get; set; }

        [Display(Name = "Composição")]
        [StringLength(200, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string TecidoComposicao { get; set; }

        [Display(Name = "Armação")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string TecidoArmacao { get; set; }

        [Display(Name = "Gramatura")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string TecidoGramatura { get; set; }

        [Display(Name = "Largura")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string TecidoLargura { get; set; }

        [Display(Name = "Rendimento")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string TecidoRendimento { get; set; }
    }
}