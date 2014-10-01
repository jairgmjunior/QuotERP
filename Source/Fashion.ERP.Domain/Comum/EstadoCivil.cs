using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Comum
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<EstadoCivil>))]
    public enum EstadoCivil
    {
        [Display(Name = "Solteiro")]
        Solteiro,
        [Display(Name = "Casado")]
        Casado,
        [Display(Name = "Separado")]
        Separado,
        [Display(Name = "Divorciado")]
        Divorciado,
        [Display(Name = "Viúvo")]
        Viuvo,
    }

    public static class EstadoCivilExtensions
    {
        public static string EnumToString(this EstadoCivil estadoCivil)
        {
            var display = estadoCivil.GetDisplay();
            return display != null ? display.Name : estadoCivil.ToString();
        }
    }
}