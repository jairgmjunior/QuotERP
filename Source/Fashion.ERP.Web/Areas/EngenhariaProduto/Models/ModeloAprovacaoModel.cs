using System;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class ModeloAprovacaoModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Referência")]
        public string Referencia { get; set; }
        
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        
        [Display(Name = "Quantidade")]
        [Required(ErrorMessage = "Informe a quantidade")]
        public long? Quantidade { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        [Display(Name = "Produto Base")]
        public long? ProdutoBase { get; set; }
        
        [Display(Name = "Comprimento")]
        public long? Comprimento { get; set; }

        [Display(Name = "Medida Comprimento")]
        public double? MedidaComprimento { get; set; }

        [Display(Name = "Barra")]
        public long? Barra { get; set; }

        [Display(Name = "Produto Base")]
        public double? MedidaBarra { get; set; }
    }
}