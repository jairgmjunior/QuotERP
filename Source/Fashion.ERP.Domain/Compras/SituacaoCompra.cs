using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Compras
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<SituacaoCompra>))]
    public enum SituacaoCompra
    {
        [Display(Name = "NÃO ATENDIDO")]
        NaoAtendido,
        [Display(Name = "ATENDIDO PARCIAL")]
        AtendidoParcial,
        [Display(Name = "ATENDIDO TOTAL")]
        AtendidoTotal,
        [Display(Name = "CANCELADO")]
        Cancelado,
    }

    public static class SituacaoCompraExtensions
    {
        public static string EnumToString(this SituacaoCompra situacaoCompra)
        {
            var display = situacaoCompra.GetDisplay();
            return display != null ? display.Name : situacaoCompra.ToString();
        }
    }
}