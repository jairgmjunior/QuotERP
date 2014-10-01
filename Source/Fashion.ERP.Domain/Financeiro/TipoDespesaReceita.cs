using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Financeiro
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<TipoDespesaReceita>))]
    public enum TipoDespesaReceita
    {
        [Display(Name = "Recebimentos")]
        Recebimentos,
        [Display(Name = "Despesas fixas")]
        DespesaFixas,
        [Display(Name = "Despesas variáveis")]
        DespesasVariaveis,
        [Display(Name = "Pessoas")]
        Pessoas,
        [Display(Name = "Impostos")]
        Impostos
    }

    public static class TipoDespesaReceitaExtensions
    {
        public static string EnunToString(this TipoDespesaReceita tipoDespesaReceita)
        {
            var display = tipoDespesaReceita.GetDisplay();
            return display != null ? display.Name : tipoDespesaReceita.ToString();
        }
    }
}