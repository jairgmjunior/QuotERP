using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class GridRequisicaoMaterialItemBaixaModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Referência")]
        public string Referencia { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "UND")]
        public string UND { get; set; }

        [Display(Name = "Qtde Atendida")]
        public double QtdeAtendida { get; set; }

        [Display(Name = "Qtde Pendente")]
        public double QtdePendente { get; set; }

        public bool Check { get; set; }

        [Display(Name = "Qtde Estoque")]
        [DisplayFormat(DataFormatString = "{0:n4}")]
        public double QtdeEstoque { get; set; }

        [Display(Name = "Qtde Baixa")]
        [DisplayFormat(DataFormatString = "{0:n4}")]
        public double QtdeBaixa { get; set; }
        
        [Display(Name = "Situação")]
        public string SituacaoRequisicaoMaterialDescricao { get; set; }
    }
}