using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class SolicitacaoMaterialCompraModel
    {
        public SolicitacaoMaterialCompraModel()
        {
            //preencher as listas para não bugar o componente de multiselect
            Marcas = new List<long?>();
            Categorias = new List<long?>();
            Subcategorias = new List<long?>();
        }
        
        [Display(Name = "Marca")]
        public IList<long?> Marcas { get; set; }

        [Display(Name = "Coleção Aprovada")]
        public long? ColecaoAprovada { get; set; }

        [Display(Name = "Categoria")]
        public IList<long?> Categorias { get; set; }

        [Display(Name = "Subcategoria")]
        public IList<long?> Subcategorias { get; set; }
        
        [Display(Name = "Período de Aprovação")]
        public DateTime? DataInicial { get; set; }

        [Display(Name = "Até")]
        public DateTime? DataFinal { get; set; }

        [Display(Name = "Referência")]
        public long? Material { get; set; }

        [Display(Name = "Ordenar por")]
        public string OrdenarPor { get; set; }

        [Display(Name = "em")]
        public string OrdenarEm { get; set; }
    }
}