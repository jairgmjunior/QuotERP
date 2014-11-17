using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class RecebimentoCompraItemModel : IModel
    {
        public long? Id { get; set; }
        
        [Display(Name = "Qtd.")]
        public double Quantidade { get; set; }

        [Display(Name = "V.Unit.Ped.(R$)")]
        [DisplayFormat(DataFormatString = "{0:N4}")]
        public double ValorUnitarioPedido { get; set; }
        
        [Display(Name = "Referência")]
        public string MaterialReferencia { get; set; }

        [Display(Name = "Ref.Externa")]
        public string MaterialReferenciaExterna { get; set; }

        [Display(Name = "Descrição")]
        public string MaterialDescricao { get; set; }

        [Display(Name = "Unid.")]
        public string UnidadeMedidaSigla { get; set; }

        [Display(Name = "Qtd.Ent.")]
        public double QuantidadeEntrada { get; set; }

        [Display(Name = "Und.Ent.")]
        public string UnidadeEntrada { get; set; }
        
        [Display(Name = "V.Unit.(R$)")]
        [DisplayFormat(DataFormatString = "{0:N4}")]
        public double ValorUnitario { get; set; }

        [Display(Name = "V.Total(R$)")]
        [DisplayFormat(DataFormatString = "{0:N4}")]
        public double ValorTotal { get; set; }

        [Display(Name = "Pedido(s)")]
        public IList<long> PedidosCompra { get; set; }
        
        public IList<long?> PedidoCompraItens { get; set; }
    }
}