using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Producao
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<TipoSinistroFechamentoOrdemProducao>))]
    public enum TipoSinistroFechamentoOrdemProducao
    {
        [Display(Name = "Defeito")]
        Defeito,
        [Display(Name = "Conserto")]
        Conserto,
    }

    public static class TipoSinistroFechamentoOrdemProducaoExtensions
    {
        public static string EnumToString(this TipoSinistroFechamentoOrdemProducao tipoSinistroFechamentoOrdemProducao)
        {
            var display = tipoSinistroFechamentoOrdemProducao.GetDisplay();
            return display != null ? display.Name : tipoSinistroFechamentoOrdemProducao.ToString();
        }
    }
}