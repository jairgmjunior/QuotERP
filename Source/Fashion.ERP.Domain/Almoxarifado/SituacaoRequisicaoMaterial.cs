using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Almoxarifado
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<SituacaoRequisicaoMaterial>))]
    public enum SituacaoRequisicaoMaterial
    {
        [Display(Name = "NÃO ATENDIDO")]
        NaoAtendido,
        [Display(Name = "ATENDIDO PARCIAL")]
        AtendidoParcial,
        [Display(Name = "ATENDIDO TOTAL")]
        AtendidoTotal,
        [Display(Name = "CANCELADO")]
        Cancelado
    }

    public static class SituacaoRequisicaoMaterialExtensions
    {
        public static string EnumToString(this SituacaoRequisicaoMaterial situacaoRequisicaoMaterial)
        {
            var display = situacaoRequisicaoMaterial.GetDisplay();
            return display != null ? display.Name : situacaoRequisicaoMaterial.ToString();
        }
    }
}