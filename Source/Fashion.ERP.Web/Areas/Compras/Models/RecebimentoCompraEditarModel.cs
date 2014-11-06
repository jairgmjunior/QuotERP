using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class RecebimentoCompraEditarModel : RecebimentoCompraModel
    {
        //elementos html disabled não podem ser required pois os mesmos não são passados para o controller.
        [Display(Name = "Unidade")]
        public override long? Unidade { get; set; }
    }
}