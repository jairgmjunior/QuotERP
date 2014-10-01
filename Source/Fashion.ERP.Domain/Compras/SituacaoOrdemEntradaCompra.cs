using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Compras
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<SituacaoOrdemEntradaCompra>))]
    public enum SituacaoOrdemEntradaCompra
    {
        [Display(Name = "Não recebida")]
        NaoRecebida,
        [Display(Name = "Cancelada")]
        Cancelada
    }

    public static class SituacaoOrdemEntradaCompraExtensions
    {
        public static string EnumToString(this SituacaoOrdemEntradaCompra situacaoOrdemEntradaCompra)
        {
            var display = situacaoOrdemEntradaCompra.GetDisplay();
            return display != null ? display.Name : situacaoOrdemEntradaCompra.ToString();
        }
    }
}