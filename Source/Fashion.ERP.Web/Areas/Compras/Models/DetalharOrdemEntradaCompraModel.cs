using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class DetalharOrdemEntradaCompraModel
    {
        public DetalharOrdemEntradaCompraModel()
        {
            PedidoCompras = new List<GridDetalharPedidoCompra>();
            OrdemEntradas = new List<GridDetalharOrdemEntrada>();
        }
        public long Id { get; set; }

        [Display(Name = "Número")]
        public string Numero { get; set; }

        [Display(Name = "Data da ordem")]
        public string Data { get; set; }

        [Display(Name = "Fornecedor")]
        public string Fornecedor { get; set; }

        [Display(Name = "Situação")]
        public string Situacao { get; set; }

        [Display(Name = "Comprador")]
        public string Comprador { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        public IList<GridDetalharPedidoCompra> PedidoCompras { get; set; }

        public IList<GridDetalharOrdemEntrada> OrdemEntradas { get; set; }
    }
}