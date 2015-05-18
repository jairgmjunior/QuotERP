using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.EngenhariaProduto
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<TipoEnfestoTecido>))]
    public enum TipoEnfestoTecido
    {
        [Display(Name = "PARES")]
        Pares,
        [Display(Name = "FOLHA")]
        Folha
    }

    public static class TipoEnfestoTecidoExtensions
    {
        public static string EnumToString(this TipoEnfestoTecido situacaoCompra)
        {
            var display = situacaoCompra.GetDisplay();
            return display != null ? display.Name : situacaoCompra.ToString();
        }
    }
}