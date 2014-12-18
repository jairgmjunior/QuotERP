using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Almoxarifado
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<SituacaoReservaMaterialItem>))]
    public enum SituacaoReservaMaterialItem
    {
        [Display(Name = "Solicitada")]
        Solicitada,
        [Display(Name = "Substituída")]
        Substituida,
        [Display(Name = "Finalizada")]
        Finalizada,
        [Display(Name = "Cancelada")]
        Cancelada
    }

    public static class SituacaoReservaMaterialItemExtensions
    {
        public static string EnumToString(this SituacaoReservaMaterialItem situacaoReservaMaterialItem)
        {
            var display = situacaoReservaMaterialItem.GetDisplay();
            return display != null ? display.Name : situacaoReservaMaterialItem.ToString();
        }
    }
}