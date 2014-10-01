using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class GridModeloModel
    {
        public long Id { get; set; }

        [Display(Name = "Referência")]
        public string Referencia { get; set; } 

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Coleção")]
        public string Colecao { get; set; }

        public string Estilista { get; set; }

        public string Grade { get; set; }

        public string Marca { get; set; }

        public string Segmento { get; set; }

        public string Lavada { get; set; }

        [Display(Name = "Linha casa")]
        public string LinhaCasa { get; set; }

    }
}