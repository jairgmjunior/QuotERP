using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<TipoRelatorioParametro>))]
    public enum TipoRelatorioParametro
    {
        [Display(Name = "Booleano")]
        Boolean,
        [Display(Name = "Data")]
        DateTime,
        [Display(Name = "Inteiro")]
        Integer,
        [Display(Name = "Real")]
        Float,
        [Display(Name = "Texto")]
        String,
    }

    public static class TipoRelatorioParametroExtensions
    {
        public static string EnumToString(this TipoRelatorioParametro tipoRelatorioParametro)
        {
            var display = tipoRelatorioParametro.GetDisplay();
            return display != null ? display.Name : tipoRelatorioParametro.ToString();
        }
    }
}