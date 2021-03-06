﻿using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class ParametroModuloCompraModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Validar o recebimento do pedido de compra")]
        public bool ValidaRecebimentoPedido { get; set; }

        [Display(Name = "Percentual para criação do Pedido de compra autorizado no Recebimento de compra")]
        public double PercentualCriacaoPedidoAutorizadoRecebimento { get; set; }
    }
}