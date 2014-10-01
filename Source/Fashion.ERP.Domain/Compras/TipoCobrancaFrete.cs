using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Compras
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<TipoCobrancaFrete>))]
    public enum TipoCobrancaFrete
    {
        [Display(Name = "0 - Emitente")]
        Emitente = 0,
        [Display(Name = "1 - Destinatário")]
        Destinatario = 1,
        [Display(Name = "2 - Terceiros")]
        Terceiros = 2,
        [Display(Name = "9 - Sem cobrança")]
        SemCobranca = 9,
    }

    public static class TipoCobrancaFreteExtensions
    {
        public static string EnumToString(this TipoCobrancaFrete tipoCobrancaFrete)
        {
            var display = tipoCobrancaFrete.GetDisplay();
            return display != null ? display.Name : tipoCobrancaFrete.ToString();
        }
    }
}