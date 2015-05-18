using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class GridEsbocoMatrizCorteModel
    {
        public long Id { get; set; }

        [Display(Name = "Referência")]
        public string Referencia { get; set; } 

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Tag/Ano")]
        public string TagAno { get; set; }

        [Display(Name = "Dificuldade")]
        public string Dificuldade { get; set; }
        
        [Display(Name = "Quantidade")]
        public long? Quantidade { get; set; }

        [Display(Name = "Coleção Aprovada")]
        public string ColecaoAprovada { get; set; }
    }
}