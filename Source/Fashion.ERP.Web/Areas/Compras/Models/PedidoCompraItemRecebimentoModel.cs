using System;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class PedidoCompraItemRecebimentoModel : IModel
    {
        public long? Id { get; set; }
        
        [Display(Name = "Referência")]
        public string MaterialReferenciaPedido { get; set; }

        [Display(Name = "Ref.Externa")]
        public string MaterialReferenciaExternaPedido { get; set; }

        [Display(Name = "Descrição")]
        public string MaterialDescricaoPedido { get; set; }

        [Display(Name = "Und.")]
        public string UnidadeMedidaSiglaPedido { get; set; }
        
        [Display(Name = "Val. Unitário")]
        public double ValorUnitarioPedido { get; set; }

        [Display(Name = "Qtd.Receber")]
        public double QuantidadePedido { get; set; }

        public long PedidoCompra { get; set; }

        public bool Marcado;
    }
}