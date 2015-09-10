using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class ConsumoMaterialPorModeloModel
    {
        [Display(Name = "Coleção")]
        public long? Colecao { get; set; }

        [Display(Name = "Coleção Aprovada")]
        public long? ColecaoAprovada { get; set; }

        [Display(Name = "Categoria")]
        public long? Categoria { get; set; }

        [Display(Name = "Subcategoria")]
        public long? Subcategoria { get; set; }

        [Display(Name = "Família")]
        public long? Familia { get; set; }

        [Display(Name = "Período de Avaliação")]
        public DateTime? DataInicial { get; set; }

        [Display(Name = "Até")]
        public DateTime? DataFinal { get; set; }

        [Display(Name = "Ordenar por")]
        public string OrdenarPor { get; set; }

        [Display(Name = "em")]
        public string OrdenarEm { get; set; }
    }
}