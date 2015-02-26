using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Producao
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<TipoOrdemProducao>))]
    public enum TipoOrdemProducao
    {
        [Display(Name = "Normal")]
        Normal,
        [Display(Name = "Remessa")]
        Remessa,
        [Display(Name = "Consumo")]
        Consumo,
        [Display(Name = "Doação")]
        Doacao,
    }

    public static class TipoOrdemProducaoExtensions
    {
        public static string EnumToString(this TipoOrdemProducao tipoOrdemProducao)
        {
            var display = tipoOrdemProducao.GetDisplay();
            return display != null ? display.Name : tipoOrdemProducao.ToString();
        }
    }
}