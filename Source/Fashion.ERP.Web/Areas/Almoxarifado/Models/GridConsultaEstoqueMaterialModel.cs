using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class GridConsultaEstoqueMaterialModel
    {
        public long Id { get; set; }

        [Display(Name = "Unidade")]
        public string Unidade { get; set; }

        [Display(Name = "Depósito")]
        public string DepositoMaterial { get; set; }

        [Display(Name = "Categoria")]
        public string Categoria { get; set; }

        [Display(Name = "Subcategoria")]
        public string Subcategoria { get; set; }

        [Display(Name = "Família")]
        public string Familia { get; set; }

        [Display(Name = "Marca")]
        public string Marca { get; set; }

        [Display(Name = "Referência")]
        public string Referencia { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Und.")]
        public string UnidadeMedida { get; set; }
        
        [Display(Name = "Foto")]
        public string Foto { get; set; }

        [Display(Name = "Qtde. Estoque")]
        public double Saldo { get; set; }

        [Display(Name = "Qtde. Reservada")]
        public double QtdeReservada { get; set; }

        [Display(Name = "Qtde. Compras")]
        public double QuantidadeCompras { get; set; }
        
        [Display(Name = "Qtde. Disp. Compras")]
        public double QtdeDisponivelCompras
        {
            get { return QtdeDisponivel + QuantidadeCompras; }
        }

        [Display(Name = "Qtde. Disponível")]
        public double QtdeDisponivel
        {
            get { return Saldo - QtdeReservada; }
        }

        public long? MaterialId { get; set; }
        public long? UnidadeId { get; set; }
    }
}