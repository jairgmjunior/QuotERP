using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Producao
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<SituacaoProgramacaoProducao>))]
    public enum SituacaoProgramacaoProducao
    {
        [Display(Name = "INICIADA")]
        Iniciada,
        [Display(Name = "EM RESERVA")]
        EmReserva,
        [Display(Name = "EM REQUISIÇÃO")]
        EmRequisicao,
        [Display(Name = "FINALIZADA")]
        Finalizada,
        [Display(Name = "CANCELADA")]
        Cancelada,
    }

    public static class SituacaoProgramacaoProducaoExtensions
    {
        public static string EnumToString(this SituacaoProgramacaoProducao situacaoProgramacaoProducao)
        {
            var display = situacaoProgramacaoProducao.GetDisplay();
            return display != null ? display.Name : situacaoProgramacaoProducao.ToString();
        }
    }
}