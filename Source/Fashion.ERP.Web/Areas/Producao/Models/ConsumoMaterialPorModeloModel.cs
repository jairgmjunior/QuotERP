using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class ConsumoMaterialProgramadoModel
    {
        public ConsumoMaterialProgramadoModel()
        {
            //preencher as listas para não bugar o componente de multiselect
            Categorias = new List<long?>();
            Subcategorias = new List<long?>();
        }

        [Display(Name = "Tag")]
        public string Tag { get; set; }

        [Display(Name = "Ano")]
        public long? Ano { get; set; }
        
        [Display(Name = "Remessa")]
        public long? RemessaProducao { get; set; }

        [Display(Name = "Categoria do Material")]
        public IList<long?> Categorias { get; set; }

        [Display(Name = "Subcategoria do Material")]
        public IList<long?> Subcategorias { get; set; }

        [Display(Name = "Referência do Material")]
        public long? Material { get; set; }

        [Display(Name = "Data Programada")]
        public DateTime? DataInicial { get; set; }

        [Display(Name = "Até")]
        public DateTime? DataFinal { get; set; }

        [Display(Name = "Ordenar por")]
        public string OrdenarPor { get; set; }

        [Display(Name = "em")]
        public string OrdenarEm { get; set; }
    }
}