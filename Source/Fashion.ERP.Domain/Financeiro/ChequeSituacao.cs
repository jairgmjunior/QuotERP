using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Financeiro
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<ChequeSituacao>))]
    public enum ChequeSituacao
    {
        [Display(Name = "Não depositado")]
        NaoDepositado,
        [Display(Name = "Custódia")]
        Custodia,
        [Display(Name = "Depositado")]
        Depositado,
        [Display(Name = "Repassado")]
        Repassado,
        [Display(Name = "Roubado")]
        Roubado,
        [Display(Name = "Quitado parcial")]
        QuitadoParcial,
        [Display(Name = "Quitado total")]
        QuitadoTotal
    }

    public static class ChequeSituacaoExtensions
    {
        public static string EnumToString(this ChequeSituacao chequeSituacao)
        {
            var display = chequeSituacao.GetDisplay();
            return display != null ? display.Name : chequeSituacao.ToString();
        }
    }
}