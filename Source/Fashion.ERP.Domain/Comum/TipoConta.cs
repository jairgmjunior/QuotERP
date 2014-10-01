using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Comum
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<TipoConta>))]
    public enum TipoConta
    {
        [Display(Name = "Corrente")]
        Corrente,
        [Display(Name = "Poupança")]
        Poupanca
    }

    public static class TipoContaExtensions
    {
        public static string EnumToString(this TipoConta tipoConta)
        {
            var display = tipoConta.GetDisplay();
            return display != null ? display.Name : tipoConta.ToString();
        }
    }
}