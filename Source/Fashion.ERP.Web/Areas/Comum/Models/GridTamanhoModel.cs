using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class GridTamanhoModel
    {
        public long Id { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        public string Sigla { get; set; }
        public bool Ativo { get; set; }
    }
}