using System;

using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Web.Areas.Comum.Models;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class GridPedidoCompraItemCanceladoModel 
    {
        public long Id { get; set; }

        [Display(Name = "Referência")]
        public string Referencia { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "UND")]
        public string UND { get; set; }

        [Display(Name = "Qtde")]
        public double Qtde { get; set; }

        [Display(Name = "Entregue")]
        public double Entregue { get; set; }

        [Display(Name = "Diferença")]
        public double Diferenca { get; set; }

        [Display(Name = "Preço")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Preco { get; set; }

        [Display(Name = "Desconto")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Desconto { get; set; }

        [Display(Name = "Valor Total")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double ValorTotal { get; set; }

        [Display(Name = "Situação")]
        public string SituacaoCompraDescricao { get; set; }

        public bool Check { get; set; }
    }
}