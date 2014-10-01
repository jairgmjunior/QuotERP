using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class PedidoCompraReduzidoModel: IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Número")]
        public long Numero { get; set; }
        
        [Display(Name = "Data")]
        public string Data { get; set; }

        [Display(Name = "Valor")]
        public double Valor { get; set; } 
    }
}