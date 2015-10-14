using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class ModeloAvaliacaoModel : IModel
    {
        public long? Id { get; set; }

        public long? IdAvaliacao { get; set; }

        public long? IdModelo { get; set; }

        public long SequenciaTag { get; set; }

        [Display(Name = "Referência")]
        public string Referencia { get; set; }

        [Display(Name = "Coleção")]
        public string Colecao { get; set; }

        [Display(Name = "Estilista")]
        public string Estilista { get; set; }
        
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Tecido")]
        public string Tecido { get; set; }

        [Display(Name = "Forro")]
        public string Forro { get; set; }
        
        [Display(Name = "Motivo")]
        [Required(ErrorMessage = "Informe o motivo")]
        public string Motivo { get; set; }
        
        [Display(Name = "Tag/Ano")]
        [Required(ErrorMessage = "Informe a tag")]
        public string Tag { get; set; }

        [Required(ErrorMessage = "Informe o ano")]
        public int? Ano { get; set; }

        [Display(Name = "Coleção Aprovada")]
        public long? ColecaoAprovada { get; set; }
        
        [Display(Name = "Complemento")]
        [DataType(DataType.MultilineText)]
        [StringLength(200, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Complemento { get; set; }

        [Display(Name = "Catálogo")]
        public Boolean? Catalogo { get; set; }

        [Display(Name = "Dificuldade")]
        public long? ClassificacaoDificuldade { get; set; }

        [Display(Name = "Quantidade Total")]
        public double? QuantidadeTotal { get; set; }
        
        public Boolean AprovadoReprovado { get; set; }
        
        [Display(Name = "Produto Base")]
        public long? ProdutoBase { get; set; }
        
        [Display(Name = "Comprimento")]
        public long? Comprimento { get; set; }

        [Display(Name = "Barra")]
        public long? Barra { get; set; }

        [Display(Name = "Grade")]
        public long? Grade { get; set; }
        
        public IList<ModeloAprovacaoModel> GridItens { get; set; }
    }
}