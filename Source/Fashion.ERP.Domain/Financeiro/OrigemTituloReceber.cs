using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Financeiro
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<OrigemTituloReceber>))]
    public enum OrigemTituloReceber
    {
        [Display(Name = "Venda")]
        Venda,
        [Display(Name = "Cadastro")]
        Cadastro,
    }

    public static class OrigemTituloReceberExtensions
    {
        public static string EnumToString(this OrigemTituloReceber origemTituloReceber)
        {
            var display = origemTituloReceber.GetDisplay();
            return display != null ? display.Name : origemTituloReceber.ToString();
        }
    }
}