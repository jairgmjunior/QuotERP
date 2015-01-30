using System.ComponentModel.DataAnnotations;
namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class GridSimboloConservacaoModel
    {
        public long Id { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Display(Name = "Categoria")]
        public string CategoriaConservacao { get; set; }
        [Display(Name = "Imagem")]
        public string Foto { get; set; }
    }
}