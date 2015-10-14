using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Compras;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class NecessidadeCompraMaterialModel
    {
        public NecessidadeCompraMaterialModel()
        {
            //preencher as listas para não bugar o componente de multiselect
            Categorias = new List<long?>();
            Subcategorias = new List<long?>();
            Colecoes = new List<long?>();
        }
        
        [Display(Name = "Categoria")]
        public List<long?> Categorias { get; set; }

        [Display(Name = "Subcategoria")]
        public List<long?> Subcategorias { get; set; }
        
        [Display(Name = "Coleção")]
        public List<long?> Colecoes { get; set; }
        
        [Display(Name = "Referência do Material")]
        public long? Material { get; set; }

        [Display(Name = "Fornecedor")]
        public long? Fornecedor { get; set; }
        
        [Display(Name = "Unidade")]
        [Required(ErrorMessage = "Informe a unidade.")]
        public long? Unidade { get; set; }
        
        public string ModoConsulta { get; set; }

        [Display(Name = "Agrupar por")]
        public string AgruparPor { get; set; }

        [Display(Name = "Ordenar por")]
        public string OrdenarPor { get; set; }

        [Display(Name = "em")]
        public string OrdenarEm { get; set; }

    }
}