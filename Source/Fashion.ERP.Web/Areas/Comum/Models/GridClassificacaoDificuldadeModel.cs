using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class GridClassificacaoDificuldadeModel
    {
        public long Id { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Criação")]
        public string Criacao { get; set; }

        [Display(Name = "Produção")]
        public string Producao { get; set; }

        public bool Ativo { get; set; } 
    }
}