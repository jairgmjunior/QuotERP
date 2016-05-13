using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Producao
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<TipoProducao>))]
    public enum TipoProducao
    {
        [Display(Name = "PRODUÇÃO")]
        Producao,
        [Display(Name = "CONSUMO")]
        Consumo,
        [Display(Name = "DOAÇÃO")]
        Doacao
    }

    public static class TipoProducaoExtensions
    {
        public static string EnumToString(this TipoProducao tipoProducao)
        {
            var display = tipoProducao.GetDisplay();
            return display != null ? display.Name : tipoProducao.ToString();
        }
    }
}