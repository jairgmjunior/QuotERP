using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class GridEntradaMaterialModel
    {
        public long Id { get; set; }

        [Display(Name = "Data da entrada")]
        public DateTime DataEntrada { get; set; }

        [Display(Name = "Unidade de destino")]
        public string UnidadeDestino { get; set; }

        [Display(Name = "Depósito destino")]
        public string DepositoMaterialDestino { get; set; }

        [Display(Name = "Origem/Fornecedor")]
        public string OrigemFornecedor { get; set; }
    }
}