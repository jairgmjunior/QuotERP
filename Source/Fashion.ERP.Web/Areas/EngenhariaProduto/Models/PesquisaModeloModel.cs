using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class PesquisaModeloModel : IModeloDropdownModel
    {
        [Display(Name = "Referência")]
        public string Referencia { get; set; }

        [Display(Name = "Coleção")]
        public long? Colecao { get; set; }

        public long? Estilista { get; set; }

        public long? Modelista { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Complemento")]
        public string Complemento { get; set; }

        public long? Natureza { get; set; }

        [Display(Name = "Classificação")]
        public long? Classificacao { get; set; }

        [Display(Name = "Tag")]
        public string Tag { get; set; }

        public long? Artigo { get; set; }

        [Display(Name = "Produto base")]
        public long? ProdutoBase { get; set; }

        [Display(Name = "Referência Catálogo Material")]
        public string ReferenciaMaterial { get; set; }

        public long? Comprimento { get; set; }

        public long? Barra { get; set; }

        public string Detalhamento { get; set; }

        public long? Grade { get; set; }

        public long? Marca { get; set; }

        public long? Segmento { get; set; }
        
        public string ModoConsulta { get; set; }

        [Display(Name = "Agrupar por")]
        public string AgruparPor { get; set; }

        [Display(Name = "Ordenar por")]
        public string OrdenarPor { get; set; }

        [Display(Name = "em")]
        public string OrdenarEm { get; set; }

        public IList<GridModeloModel> Grid { get; set; }
    }
}