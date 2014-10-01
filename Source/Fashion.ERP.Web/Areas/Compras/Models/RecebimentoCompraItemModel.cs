using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class RecebimentoCompraItemModel : IModel
    {
        public long? Id { get; set; }
        
        [Display(Name = "Quantidade")]
        public double Quantidade { get; set; }

        [Display(Name = "Val.Unit.Pedido")]
        public double ValorUnitarioPedido { get; set; }
        
        [Display(Name = "Referência")]
        public string MaterialReferencia { get; set; }

        [Display(Name = "Referência Externa")]
        public string MaterialReferenciaExterna { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Unidade")]
        public string UnidadeMedidaSigla { get; set; }

        [Display(Name = "Qtd.Entrada")]
        public string QuantidadeEntrada { get; set; }

        [Display(Name = "Und.Entrada")]
        public string UnidadeEntrada { get; set; }
        
        [Display(Name = "Val. Unitário")]
        public double ValorUnitario { get; set; }

        [Display(Name = "Total Item")]
        public double TotalItem { get; set; }

        [Display(Name = "Pedido de Compra")]
        public IList<long> PedidosCompra { get; set; }
    }
}