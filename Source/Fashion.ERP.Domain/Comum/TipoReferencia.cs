using System.ComponentModel;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Comum
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<TipoReferencia>))]
    public enum TipoReferencia
    {
        Pessoal,
        Comercial,
    }

    public static class TipoTipoReferenciaExtensions
    {
        public static string EnumToString(this TipoReferencia tipoReferencia)
        {
            var display = tipoReferencia.GetDisplay();
            return display != null ? display.Name : tipoReferencia.ToString();
        }
    }
}