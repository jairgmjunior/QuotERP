using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class PesquisaMaterialModel : IMaterialDropdownModel
    {
        [Display(Name = "Referência")]
        [StringLength(20, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Referencia { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Descricao { get; set; }

        [Display(Name = "Código de barras")]
        [StringLength(8, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string CodigoBarra { get; set; }

        [Display(Name = "NCM")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Ncm { get; set; }

        [Display(Name = "Origem CST")]
        public long? OrigemSituacaoTributaria { get; set; }

        [Display(Name = "Localização")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Localizacao { get; set; }

        [Display(Name = "Ativo")]
        public bool? Ativo { get; set; }

        [Display(Name = "Gênero fiscal")]
        public long? GeneroFiscal { get; set; }

        [Display(Name = "Unidade de medida")]
        public long? UnidadeMedida { get; set; }

        [Display(Name = "Marca do material")]
        public long? MarcaMaterial { get; set; }

        [Display(Name = "Família")]
        public long? Familia { get; set; }

        [Display(Name = "Tipo do item")]
        public long? TipoItem { get; set; }

        [Display(Name = "Categoria")]
        public long? Categoria { get; set; }

        [Display(Name = "Subcategoria")]
        public long? Subcategoria { get; set; }

        public string ModoConsulta { get; set; }

        [Display(Name = "Tipo")]
        public string TipoRelatorio { get; set; }

        [Display(Name = "Agrupar por")]
        public string AgruparPor { get; set; }

        [Display(Name = "Ordenar por")]
        public string OrdenarPor { get; set; }

        [Display(Name = "em")]
        public string OrdenarEm { get; set; }

        public IList<GridMaterialModel> Grid { get; set; }
    }
}