using System.ComponentModel;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Almoxarifado
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<SituacaoConferencia>))]
    public enum SituacaoConferencia
    {
        Aberta,
        Conferida,
        Divergente,
        Cancelada
    }

    public static class SituacaoConferenciaExtensions
    {
        public static string EnumToString(this SituacaoConferencia situacaoConferencia)
        {
            var display = situacaoConferencia.GetDisplay();
            return display != null ? display.Name : situacaoConferencia.ToString();
        }
    }
}