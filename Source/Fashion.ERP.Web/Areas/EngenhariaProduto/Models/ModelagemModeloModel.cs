using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class ModelagemModeloModel
    {
        public long ModeloId { get; set; }

        [Display(Name = "Referência")]
        public string ModeloReferencia { get; set; }

        [Display(Name = "Descrição")]
        public string ModeloDescricao { get; set; }

        [Display(Name = "Estilista")]
        public string ModeloEstilistaNome { get; set; }

        [Display(Name = "Criação")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime ModeloDataCriacao { get; set; }

        [Display(Name = "Modelista")]
        [Required(ErrorMessage = "Informe o modelista")]
        public long? Modelista { get; set; }

        [Display(Name = "Data modelagem")]
        [Required(ErrorMessage = "Informe a data da modelagem")]
        public DateTime? DataModelagem { get; set; }

        [Display(Name = "Tecido")]
        [StringLength(60, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Tecido { get; set; }

        [Display(Name = "Cós")]
        public double? Cos { get; set; }

        [Display(Name = "Passante")]
        public double? Passante { get; set; }

        [Display(Name = "Entrepernas")]
        public double? Entrepernas { get; set; }

        [Display(Name = "Boca")]
        public double? Boca { get; set; }

        [Display(Name = "Modelagem")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Modelagem { get; set; }

        [Display(Name = "Tamanho padrão")]
        public long? Tamanho { get; set; }

        [Display(Name = "Localização")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Localizacao { get; set; }

        [Display(Name = "Etiqueta da marca")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string EtiquetaMarca { get; set; }

        [Display(Name = "Etiqueta composicao")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string EtiquetaComposicao { get; set; }

        [Display(Name = "Tag")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Tag { get; set; }

        [Display(Name = "Observação")]
        [DataType(DataType.MultilineText)]
        [StringLength(4000, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Observacao { get; set; }
    }
}