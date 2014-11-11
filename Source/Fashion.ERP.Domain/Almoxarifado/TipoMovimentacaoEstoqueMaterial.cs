using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Almoxarifado
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<TipoMovimentacaoEstoqueMaterial>))]
    public enum TipoMovimentacaoEstoqueMaterial
    {
        [Display(Name = "Entrada")]
        Entrada,
        [Display(Name = "Saída")]
        Saida
    }

    public static class TipoMovimentacaoEstoqueMaterialExtensions
    {
        public static string EnumToString(this TipoMovimentacaoEstoqueMaterial tipoMovimentacaoEstoqueMaterial)
        {
            var display = tipoMovimentacaoEstoqueMaterial.GetDisplay();
            return display != null ? display.Name : tipoMovimentacaoEstoqueMaterial.ToString();
        }
    }
}