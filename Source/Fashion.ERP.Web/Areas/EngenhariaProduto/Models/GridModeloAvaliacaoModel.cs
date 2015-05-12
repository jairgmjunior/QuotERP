using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.EngenhariaProduto;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class GridModeloAvaliacaoModel
    {
        public long Id { get; set; }

        [Display(Name = "Referência")]
        public string Referencia { get; set; } 

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        
        public string Estilista { get; set; }

        [Display(Name = "Tag")]
        public string Tag { get; set; }

        [Display(Name = "Coleção")]
        public string Colecao { get; set; }

        [Display(Name = "Coleção Aprovada")]
        public string ColecaoAprovada { get; set; }

        [Display(Name = "Foto")]
        public string Foto { get; set; }
        
        [Display(Name = "Situação")]
        public SituacaoModelo Situacao { get; set; }
    }
}