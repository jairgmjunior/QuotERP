using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;
namespace Fashion.ERP.Domain.Almoxarifado
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<CategoriaConservacao>))]
    public enum CategoriaConservacao
    {
        [Display(Name = "Lavagem")]
        Lavagem,
        [Display(Name = "Alvejamento")]
        Alvejamento,
        [Display(Name = "Secagem em tambor")]
        Bordado,
        [Display(Name = "Secagem Natural")]
        Estampa,
        [Display(Name = "Passadoria")]
        Lavada,
        [Display(Name = "Limpeza a Seco Profissional")]
        LimpezaSecoProfissional,
        [Display(Name = "Limpeza a Úmido Profissional")]
        LimpezaAUmido
    }
}
