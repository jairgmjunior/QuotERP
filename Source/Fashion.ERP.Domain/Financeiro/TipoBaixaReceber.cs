using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Financeiro
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<TipoBaixaReceber>))]
    public enum TipoBaixaReceber
    {
        [Display(Name = "Normal")]
        Normal,
        [Display(Name = "Quitação")]
        Quitacao,
        [Display(Name = "Sinistro")]
        Sinistro,
        [Display(Name = "Roubo")]
        Roubo,
        [Display(Name = "Devolução")]
        Devolucao,
    }

    public static class TipoBaixaReceberExtensions
    {
        public static string EnumToString(this TipoBaixaReceber tipoBaixaReceber)
        {
            var display = tipoBaixaReceber.GetDisplay();
            return display != null ? display.Name : tipoBaixaReceber.ToString();
        }
    }
}