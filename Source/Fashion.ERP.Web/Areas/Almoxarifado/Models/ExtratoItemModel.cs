using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class ExtratoItemModel
    {
        public long Id { get; set; }

        [Display(Name = "Unidade")]
        public string Unidade { get; set; }

        [Display(Name = "Depósito")]
        public string Deposito { get; set; }

        [Display(Name = "Referência")]
        public string Material { get; set; }

        [Display(Name = "Período")]
        public DateTime DataInicial { get; set; }

        [Display(Name = "Até")]
        public DateTime DataFinal { get; set; }

        [Display(Name = "Saldo em data inicial")]
        public double SaldoInicial { get; set; }

        [Display(Name = "Saldo em data final")]
        public double SaldoFinal { get; set; }

        [Display(Name = "Unidade medida")]
        public string UnidadeMedida { get; set; }
        
        public List<GridExtratoItemModel> Grid { get; set; }
    }
}