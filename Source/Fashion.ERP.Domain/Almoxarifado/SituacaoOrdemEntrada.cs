using System.ComponentModel;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Almoxarifado
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<SituacaoOrdemEntrada>))]
    public enum SituacaoOrdemEntrada
    {
        Aberta,
        Recebida,
        Devolvida,
        Cancelada
    }

    public static class SituacaoOrdemEntradaExtensions
    {
        public static string EnumToString(this SituacaoOrdemEntrada situacaoOrdemEntrada)
        {
            var display = situacaoOrdemEntrada.GetDisplay();
            return display != null ? display.Name : situacaoOrdemEntrada.ToString();
        }
    }
}