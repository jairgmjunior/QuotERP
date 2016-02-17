using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Almoxarifado
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<GeneroCategoria>))]
    public enum GeneroCategoria
    {
        [Display(Name = "Tecido")]
        Tecido,
        [Display(Name = "Aviamento")]
        Aviamento,
        [Display(Name = "Bordado")]
        Bordado,
        [Display(Name = "Estampa")]
        Estampa,
        [Display(Name = "Lavada")]
        Lavada,
        [Display(Name = "Linha")]
        Linha,
        [Display(Name = "Outros")]
        Outros
    }

    public static class GeneroCategoriaExtensions
    {
        public static string EnumToString(this GeneroCategoria generoCategoria)
        {
            var display = generoCategoria.GetDisplay();
            return display != null ? display.Name.ToUpper() : generoCategoria.ToString().ToUpper();
        }
    }
}