using System;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class ModeloModel : IModel, IModeloDropdownModel
    {
        public long? Id { get; set; }

        [Display(Name = "Referência")]
        [Required(ErrorMessage = "Informe a referência")]
        [StringLength(50, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Referencia { get; set; }

        [Display(Name = "Coleção")]
        [Required(ErrorMessage = "Informe a coleção")]
        public long? Colecao { get; set; }

        [Required(ErrorMessage = "Informe o estilista")]
        [Display(Name = "Estilista")]
        public long? Estilista { get; set; }
        
        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Informe a descrição")]
        [DataType(DataType.MultilineText)]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Descricao { get; set; }

        [Display(Name = "Complemento")]
        [StringLength(50, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "Informe a natureza")]
        public long? Natureza { get; set; }

        [Display(Name = "Classificação")]
        [Required(ErrorMessage = "Informe a classificação")]
        public long? Classificacao { get; set; }


        [Required(ErrorMessage = "Informe o artigo")]
        public long? Artigo { get; set; }

        [Display(Name = "Base")]
        public long? ProdutoBase { get; set; }

        public long? Comprimento { get; set; }

        public long? Barra { get; set; }

        [Required(ErrorMessage = "Informe o detalhamento")]
        [DataType(DataType.MultilineText)]
        [StringLength(200, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Detalhamento { get; set; }

        [Required(ErrorMessage = "Informe a grade")]
        public long? Grade { get; set; }

        [Display(Name = "Tamanho padrão")]
        [StringLength(10, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string TamanhoPadrao { get; set; }

        [Required(ErrorMessage = "Informe a marca")]
        public long? Marca { get; set; }

        public long? Segmento { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(200, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Lavada { get; set; }

        [Display(Name = "Linha casa")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string LinhaCasa { get; set; }

        [Display(Name = "Tecido")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Tecido { get; set; }

        [Display(Name = "Forro")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Forro { get; set; }

        [Display(Name = "Tecido complementar")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string TecidoComplementar { get; set; }

        [Display(Name = "Ziper braguilha")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string ZiperBraguilha { get; set; }

        [Display(Name = "Ziper detalhe")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string ZiperDetalhe { get; set; }

        [Display(Name = "Previsão de envio")]
        public DateTime? DataPrevisaoEnvio { get; set; }
    }
}