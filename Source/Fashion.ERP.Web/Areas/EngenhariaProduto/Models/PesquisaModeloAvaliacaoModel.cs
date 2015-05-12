using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Domain.EngenhariaProduto;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class PesquisaModeloAvaliacaoModel
    {
        [Display(Name = "Referência")]
        public string Referencia { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Tag")]
        public string Tag { get; set; }

        [Display(Name = "Estilista")]
        public long? Estilista { get; set; }

        [Display(Name = "Coleção Aprovada")]
        public long? ColecaoAprovada { get; set; }

        [Display(Name = "Situação")]
        public SituacaoModelo? Situacao { get; set; }
        
        public string ModoConsulta { get; set; }

        [Display(Name = "Ordenar por")]
        public string OrdenarPor { get; set; }

        [Display(Name = "em")]
        public string OrdenarEm { get; set; }

        public IList<GridModeloAvaliacaoModel> Grid { get; set; }  
    }
}