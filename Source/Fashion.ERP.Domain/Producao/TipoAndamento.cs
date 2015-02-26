using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Producao
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<TipoOrdemProducao>))]
    public enum TipoAndamento
    {
        [Display(Name = "Entrada")]
        Entrada,
        [Display(Name = "Saída")]
        Saida,
    }
}