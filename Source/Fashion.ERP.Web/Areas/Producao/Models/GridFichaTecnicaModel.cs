using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class GridFichaTecnicaModel
    {
        public long Id { get; set; }

        [Display(Name = "Tag")]
        public string Tag { get; set; }

        [Display(Name = "Ano")]
        public long Ano { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Referência")]
        public string Referencia { get; set; }

        [Display(Name = "Coleção")]
        public string Colecao { get; set; }

        [Display(Name = "Marca")]
        public string Marca { get; set; }

        [Display(Name = "Natureza")]
        public string Natureza { get; set; }

        [Display(Name = "Foto")]
        public string Foto { get; set; }

        [Display(Name = "Classificação")]
        public string Classificacao { get; set; }

        [Display(Name = "Catalogo")]
        public string Catalogo { get; set; }

        [Display(Name = "Estilista")]
        public string Estilista { get; set; }
    }
}