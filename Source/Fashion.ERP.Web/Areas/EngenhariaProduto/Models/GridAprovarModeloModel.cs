using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class GridAprovarModeloModel
    {
        public long Id { get; set; }

        [Display(Name = "Referência")]
        public string Referencia { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Estilista")]
        public string Estilista { get; set; }

        [Display(Name = "Marca")]
        public string Marca { get; set; }

        [Display(Name = "Coleção")]
        public string Colecao { get; set; }

        [Display(Name = "Aprovado")]
        public bool? Aprovado { get; set; }
    }
}