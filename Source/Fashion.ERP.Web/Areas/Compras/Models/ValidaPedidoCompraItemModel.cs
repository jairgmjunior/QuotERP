using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class ValidaPedidoCompraItemModel
    {
        [Display(Name = "Referência")]
        public string Referencia { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Unidade de Medida")]
        public string UnidadeMedida { get; set; }

        [Display(Name = "Quantidade")]
        public double Quantidade { get; set; }

        [Display(Name = "Valor Unitário")]
        public double ValorUnitario { get; set; }

        [Display(Name = "Valor Total")]
        public double ValorTotal { get; set; }
    }
}