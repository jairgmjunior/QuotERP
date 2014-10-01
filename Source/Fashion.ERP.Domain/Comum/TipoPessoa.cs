using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Comum
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<TipoPessoa>))]
    public enum TipoPessoa
    {
        [Display(Name = "Física")]
        Fisica,
        [Display(Name = "Jurídica")]
        Juridica,
        [Display(Name = "Exterior")]
        Exterior
    }

    public static class TipoPessoaExtensions
    {
        public static string EnumToString(this TipoPessoa tipoPessoa)
        {
            var display = tipoPessoa.GetDisplay();
            return display != null ? display.Name : tipoPessoa.ToString();
        }
    }
}