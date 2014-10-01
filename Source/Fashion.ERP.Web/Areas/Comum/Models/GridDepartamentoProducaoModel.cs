using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class GridDepartamentoProducaoModel
    {
        public long Id { get; set; }

        public string Nome { get; set; }

        [Display(Name = "Criação")]
        public string Criacao { get; set; }

        [Display(Name = "Produção")]
        public string Producao { get; set; }

        public bool Ativo { get; set; }
    }
}