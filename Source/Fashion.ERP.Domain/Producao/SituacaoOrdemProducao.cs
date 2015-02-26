using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Producao
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<SituacaoOrdemProducao>))]
    public enum SituacaoOrdemProducao
    {
        [Display(Name = "Não iniciada")]
        NaoIniciada,
        [Display(Name = "Em produção")]
        EmProducao,
        [Display(Name = "Produzida parcial")]
        ProduzidaParcial,
        [Display(Name = "Produzida total")]
        ProduzidaTotal,
        [Display(Name = "Cancelada")]
        Cancelada,
    }

    public static class SituacaoOrdemProducaoParametroExtensions
    {
        public static string EnumToString(this SituacaoOrdemProducao situacaoOrdemProducao)
        {
            var display = situacaoOrdemProducao.GetDisplay();
            return display != null ? display.Name : situacaoOrdemProducao.ToString();
        }
    }
}