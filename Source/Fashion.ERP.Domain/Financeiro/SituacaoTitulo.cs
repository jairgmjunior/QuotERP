using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Financeiro
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<SituacaoTitulo>))]
    public enum SituacaoTitulo
    {
        [Display(Name = "Não liquidado")]
        NaoLiquidado,
        [Display(Name = "Liquidado parcial")]
        LiquidadoParcial,
        [Display(Name = "Liquidado")]
        Liquidado,
        [Display(Name = "Cancelado")]
        Cancelado
    }

    public static class SituacaoTituloExtensions
    {
        public static string EnumToString(this SituacaoTitulo situacaoTitulo)
        {
            var display = situacaoTitulo.GetDisplay();
            return display != null ? display.Name : situacaoTitulo.ToString();
        }
    }
}