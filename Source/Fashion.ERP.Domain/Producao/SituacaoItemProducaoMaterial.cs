using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Producao
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<SituacaoItemProducaoMaterial>))]
    public enum SituacaoItemProducaoMaterial
    {
        [Display(Name = "INICIADA")]
        Iniciada,
        [Display(Name = "EM NECESSIDADE")]
        EmNecessidade,
        [Display(Name = "EM REQUISIÇÃO")]
        EmRequisicao,
        [Display(Name = "CANCELADA")]
        Cancelada,
    }

    public static class SituacaoItemProducaoMaterialExtensions
    {
        public static string EnumToString(this SituacaoItemProducaoMaterial situacaoProducaoMaterial)
        {
            var display = situacaoProducaoMaterial.GetDisplay();
            return display != null ? display.Name : situacaoProducaoMaterial.ToString();
        }
    }
}