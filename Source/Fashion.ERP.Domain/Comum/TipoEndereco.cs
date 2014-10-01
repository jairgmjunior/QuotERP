using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Comum
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<TipoEndereco>))]
    public enum TipoEndereco
    {
        [Display(Name = "Residencial")]
        Residencial,
        [Display(Name = "Fatura")]
        Fatura,
        [Display(Name = "Cobrança")]
        Cobranca,
        [Display(Name = "Entrega")]
        Entrega
    }

    public static class TipoEnderecoExtensions
    {
        public static string EnumToString(this TipoEndereco tipoEndereco)
        {
            var display = tipoEndereco.GetDisplay();
            return display != null ? display.Name : tipoEndereco.ToString();
        }
    }
}