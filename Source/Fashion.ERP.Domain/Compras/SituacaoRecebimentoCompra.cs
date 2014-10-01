using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Compras
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<SituacaoRecebimentoCompra>))]
    public enum SituacaoRecebimentoCompra
    {
        [Display(Name = "Aguardando")]
        Aguardando,
        [Display(Name = "Divergente")]
        Divergente,
        [Display(Name = "Finalizada")]
        Finalizada,
        [Display(Name = "Cancelada")]
        Cancelada
    }

    public static class SituacaoRecebimentoCompraExtensions
    {
        public static string EnumToString(this SituacaoRecebimentoCompra situacaoRecebimentoCompra)
        {
            var display = situacaoRecebimentoCompra.GetDisplay();
            return display != null ? display.Name : situacaoRecebimentoCompra.ToString();
        }
    }
}