using System;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class PedidoCompraItemRecebimentoModel : IModel
    {
        public long? Id { get; set; }
        
        [Display(Name = "Referência")]
        public string MaterialReferencia { get; set; }

        [Display(Name = "Ref.Externa")]
        public string MaterialReferenciaExterna { get; set; }

        [Display(Name = "Descrição")]
        public string MaterialDescricao { get; set; }

        [Display(Name = "Und.")]
        public string UnidadeMedidaSigla { get; set; }
        
        [Display(Name = "Val. Unitário")]
        public double ValorUnitario { get; set; }

        [Display(Name = "Quantidade")]
        public double Quantidade { get; set; }

        public bool Marcado;
    }
}