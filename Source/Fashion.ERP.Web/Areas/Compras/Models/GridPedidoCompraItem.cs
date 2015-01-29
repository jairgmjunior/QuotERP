using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fashion.ERP.Domain.Comum;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Compras;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class GridPedidoCompraItem
    {

        [Display(Name = "Id")]
        public long? Id { get; set; }
        [Display(Name = "Pedido de Compra")]
        public long? PedidoCompraId { get; set; }
        [Display(Name = "Material")]
        public long? MaterialId { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Display(Name = "Referência")]
        public string Referencia { get; set; }
        [Display(Name = "Unidade")]
        public string UnidadeMedida { get; set; }
        [Display(Name = "Qtde")]
        public double? Quantidade { get; set; }
        [Display(Name = "Preço(R$)")]
        public double? ValorUnitario { get; set; }
        [Display(Name = "Desconto(R$)")]
        public double? ValorDesconto { get; set; }
        [Display(Name = "Valor Total(R$)")]
        public double? ValorTotal
        {
            get { return ValorUnitario * Quantidade; }
        }

        //[Display(Name = "Valor Total")]
        //public double? ValorTotal { get; set; }

        [Display(Name = "Prev. Entrega")]
        public DateTime? PrevisaoEntrega { get; set; }
        [Display(Name = "Prev. Entrega")]
        public String PrevisaoEntregaString { get; set; }

        [Display(Name = "Referência Externa")]
        public string ReferenciaExterna { get; set; }

        [Display(Name = "Diferença")]
        public double? Diferenca { get; set; }
        [Display(Name = "Situação")]
        public String Situacao { get; set; }

        [Display(Name = "Qtde Entregue")]
        public double? QuantidadeEntregue { get; set; }

        [Display(Name = "Entrega")]
        public DateTime? DataEntrega { get; set; }


    }
} 