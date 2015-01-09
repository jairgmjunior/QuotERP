using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Almoxarifado
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<SituacaoReservaMaterial>))]
    public enum SituacaoReservaMaterial
    {
        [Display(Name = "NÃO ATENDIDA")]
        NaoAtendida,
        [Display(Name = "ATENDIDA PARCIAL")]
        AtendidaParcial,
        [Display(Name = "ATENDIDA TOTAL")]
        AtendidaTotal,
        [Display(Name = "CANCELADA")]
        Cancelada
    }

    public static class SituacaoReservaMaterialExtensions
    {
        public static string EnumToString(this SituacaoReservaMaterial situacaoReservaMaterial)
        {
            var display = situacaoReservaMaterial.GetDisplay();
            return display != null ? display.Name : situacaoReservaMaterial.ToString();
        }
    }
}