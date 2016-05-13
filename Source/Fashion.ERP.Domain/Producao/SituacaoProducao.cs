using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Producao
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<SituacaoProducao>))]
    public enum SituacaoProducao
    {
        [Display(Name = "INICIADA")]
        Iniciada,
        [Display(Name = "EM PROGRAMAÇÃO")]
        EmProgramacao,
        [Display(Name = "RISCO")]
        Risco,
        [Display(Name = "CORTE")]
        Corte,
        [Display(Name = "EM PROCESSO")]
        EmProcesso,
        [Display(Name = "PRODUZIDA PARCIAL")]
        ProduzidaParcial,
        [Display(Name = "PRODUZIDA TOTAL")]
        ProduzidaTotal,
        [Display(Name = "CANCELADA")]
        Cancelada,
    }

    public static class SituacaoProducaoExtensions
    {
        public static string EnumToString(this SituacaoProducao situacaoProducao)
        {
            var display = situacaoProducao.GetDisplay();
            return display != null ? display.Name : situacaoProducao.ToString();
        }
    }
}