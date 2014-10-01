using System.ComponentModel;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Comum
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<Sexo>))]
    public enum Sexo
    {
        Masculino,
        Feminino,
    }

    public static class SexoExtensions
    {
        public static string EnumToString(this Sexo sexo)
        {
            var display = sexo.GetDisplay();
            return display != null ? display.Name : sexo.ToString();
        }
    }
}