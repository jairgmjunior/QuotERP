using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class GridConsultaEstoqueMaterialModel
    {
        public long Id { get; set; }

        [Display(Name = "U.E.")]
        public long Unidade { get; set; }
        
        [Display(Name = "Depósito")]
        public string DepositoMaterial { get; set; }

        [Display(Name = "Referência")]
        public string Referencia { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Un. medida")]
        public string UnidadeMedida { get; set; }

        [Display(Name = "Qtde. Estoque")]
        public double Saldo { get; set; }

        [Display(Name = "Qtde. Reservada")]
        public double QtdeReservada { get; set; }

        [Display(Name = "Qtde. Disponível")]
        public double QtdeDisponivel
        {
            get { return  Saldo - QtdeReservada; }
        }

        public long? MaterialId { get; set; }
        public long? UnidadeId { get; set; }
    }
}