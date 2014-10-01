using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Financeiro
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<TipoLancamento>))]
    public enum TipoLancamento
    {
        [Display(Name = "Débito")]
        Debito,
        [Display(Name = "Crédito")]
        Credito
    }

    public static class TipoLancamentoExtensions
    {
        public static string EnumToString(this TipoLancamento tipoLancamento)
        {
            var display = tipoLancamento.GetDisplay();
            return display != null ? display.Name : tipoLancamento.ToString();
        }
    }
}