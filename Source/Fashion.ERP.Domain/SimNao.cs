using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<SimNao>))]
    public enum SimNao
    {
        [Display(Name = "Sim")]
        Sim,
        [Display(Name = "Não")]
        Nao
    }

    public static class SimNaoExtensions
    {
        public static string EnumToString(this SimNao simNao)
        {
            var display = simNao.GetDisplay();
            return display != null ? display.Name : simNao.ToString();
        }

        public static bool EnumToBool(this SimNao simNao)
        {
            return simNao == SimNao.Sim;
        }

        public static SimNao BoolToEnum(this bool simNao)
        {
            return simNao ? SimNao.Sim : SimNao.Nao;
        }
    }
}