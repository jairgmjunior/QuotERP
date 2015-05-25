using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.EngenhariaProduto;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class PesquisaModeloAprovacaoModel
    {
        [Display(Name = "Tag")]
        public string Tag { get; set; }

        [Display(Name = "Estilista")]
        public long? Estilista { get; set; }

        [Display(Name = "Coleção Aprovada")]
        public long? ColecaoAprovada { get; set; }

        [Display(Name = "Dificuldade")]
        public long? ClassificacaoDificuldade { get; set; }

        [Display(Name = "Situação")]
        public SituacaoModelo? Situacao { get; set; }

        public string ModoConsulta { get; set; }

        [Display(Name = "Ordenar por")]
        public string OrdenarPor { get; set; }

        [Display(Name = "em")]
        public string OrdenarEm { get; set; }
    }
}