using System.ComponentModel;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Comum
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<TipoContato>))]
    public enum TipoContato
    {
        Residencial,
        Comercial,
        Celular
    }

    public static class TipoContatoExtensions
    {
        public static string EnumToString(this TipoContato tipoContato)
        {
            var display = tipoContato.GetDisplay();
            return display != null ? display.Name : tipoContato.ToString();
        }
    }
}