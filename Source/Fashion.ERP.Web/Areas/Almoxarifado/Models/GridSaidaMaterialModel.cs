using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class GridSaidaMaterialModel
    {
        public long Id { get; set; }

        [Display(Name = "Data da saída")]
        public DateTime DataSaida { get; set; }

        [Display(Name = "Centro de custo")]
        public string CentroCusto { get; set; }

        [Display(Name = "Unidade retirada")]
        public string UnidadeOrigem { get; set; }

        [Display(Name = "Depósito retirada")]
        public string DepositoMaterialOrigem { get; set; }

        [Display(Name = "Unidade de destino")]
        public string UnidadeDestino { get; set; }

        [Display(Name = "Depósito destino")]
        public string DepositoMaterialDestino { get; set; }
    }
}