using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.EngenhariaProduto
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<SituacaoModelo>))]
    public enum SituacaoModelo
    {
        [Display(Name = "NÃO AVALIADO")]
        NaoAvaliado,
        [Display(Name = "APROVADO")]
        Aprovado,
        [Display(Name = "REPROVADO")]
        Reprovado
    }

    public static class SituacaoModeloExtensions
    {
        public static string EnumToString(this SituacaoModelo situacaoCompra)
        {
            var display = situacaoCompra.GetDisplay();
            return display != null ? display.Name : situacaoCompra.ToString();
        }
    }
}