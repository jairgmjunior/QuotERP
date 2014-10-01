using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Financeiro
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<TipoContaBancaria>))]
    public enum TipoContaBancaria
    {
        [Display(Name = "Corrente")]
        Corrente,
        [Display(Name = "Poupança")]
        Poupanca,
        [Display(Name = "Salário")]
        Salario
    }

    public static class TipoContaBancariaExtensions
    {
        public static string EnumToString(this TipoContaBancaria tipoConta)
        {
            var display = tipoConta.GetDisplay();
            return display != null ? display.Name : tipoConta.ToString();
        }
    }
}