using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fashion.ERP.Domain.Comum;
using System.ComponentModel.DataAnnotations;

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
        public long? UnidadeEstocadora { get; set; }
        [Display(Name = "Qtde")]
        public double? Quantidade { get; set; }
        [Display(Name = "Preço")]
        public double? ValorUnitario { get; set; }
        [Display(Name = "Desconto")]
        public double? ValorDesconto { get; set; }
        [Display(Name = "Valor Total")]
        public double? ValorTotal { get; set; }
        [Display(Name = "Prev. Entrega")]
        public DateTime? PrevisaoEntrega { get; set; }
        [Display(Name = "Referência Externa")]
        public string ReferenciaExterna { get; set; }
    }
} 