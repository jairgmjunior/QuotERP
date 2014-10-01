using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class PesquisaAprovarModeloModel
    {
        [Display(Name = "Referência")]
        public string Referencia { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Estilista")]
        public long? Estilista { get; set; }

        [Display(Name = "Marca")]
        public long? Marca { get; set; }

        [Display(Name = "Coleção")]
        public long? Colecao { get; set; }

        [Display(Name = "Aprovado")]
        public bool? Aprovado { get; set; }

        [Display(Name = "Ordenar por")]
        public string OrdenarPor { get; set; }

        [Display(Name = "em")]
        public string OrdenarEm { get; set; }

        public IList<GridAprovarModeloModel> Grid { get; set; } 
    }
}