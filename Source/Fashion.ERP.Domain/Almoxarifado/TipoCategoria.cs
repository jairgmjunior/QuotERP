using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Almoxarifado
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<TipoCategoria>))]
    public enum TipoCategoria
    {
        [Display(Name = "Uso e consumo")]
        UsoConsumo,
        [Display(Name = "Matéria prima")]
        MateriaPrima
    }

    public static class TipoCategoriaExtensions
    {
        public static string EnumToString(this TipoCategoria tipoCategoria)
        {
            var display = tipoCategoria.GetDisplay();
            return display != null ? display.Name.ToUpper() : tipoCategoria.ToString().ToUpper();
        }
    }
}