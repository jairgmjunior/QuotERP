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
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataInicial { get; set; }

        [Display(Name = "Até")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataFinal { get; set; }

        [Display(Name = "Saldo na data inicial")]
        public double SaldoInicial { get; set; }

        [Display(Name = "Saldo na data final")]
        public double SaldoFinal { get; set; }

        [Display(Name = "Unidade medida")]
        public string UnidadeMedida { get; set; }
        
        public List<GridExtratoItemModel> Grid { get; set; }

        [Display(Name = "Qtde. Disponível")]
        public double QtdeDisponivel
        {
            get { return SaldoFinal - QtdeReservada; }
        }

        public double QtdeReservada { get; set; }
    }
}