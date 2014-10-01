using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Comum
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<TipoPrestadorServico>))]
    public enum TipoPrestadorServico
    {
        [Display(Name = "Assessor")]
        Assessor,
        [Display(Name = "Representante")]
        Representante,
        [Display(Name = "Vendedor externo")]
        VendedorExterno,
        [Display(Name = "Transportador")]
        Transportador
    }

    public static class TipoPrestadorServicoExtensions
    {
        public static string EnumToString(this TipoPrestadorServico tipoPrestadorServico)
        {
            var display = tipoPrestadorServico.GetDisplay();
            return display != null ? display.Name : tipoPrestadorServico.ToString();
        }
    }
}