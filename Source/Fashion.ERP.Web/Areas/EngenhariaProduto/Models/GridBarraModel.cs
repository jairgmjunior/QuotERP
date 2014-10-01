using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class GridBarraModel
    {
        public long Id { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public bool Ativo { get; set; }
    }
}