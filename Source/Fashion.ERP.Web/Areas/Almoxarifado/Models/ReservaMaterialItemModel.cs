using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Almoxarifado;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class ReservaMaterialItemModel
    {
        public long? Id { get; set; }

        [Display(Name = "Referência")]
        public string Referencia { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Unid.")]
        public string UnidadeMedida { get; set; }

        [Display(Name = "Qtd. Solicitada")]
        [Range(0, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
        public double QuantidadeSolicitada { get; set; }

        [Display(Name = "Qtd. Atendida")]
        public double QuantidadeAtendida { get; set; }

        [Display(Name = "Qtd. Cancelada")]
        public double QuantidadeCancelada { get; set; }

        [Display(Name = "Situação")]
        public SituacaoReservaMaterial SituacaoReservaMaterial { get; set; } 
    }
}