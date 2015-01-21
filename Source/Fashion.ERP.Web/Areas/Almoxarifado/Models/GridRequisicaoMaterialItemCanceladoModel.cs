using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class GridRequisicaoMaterialItemCanceladoModel 
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

        [Display(Name = "Qtde Disponível")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public double QtdeDisponivel { get; set; }

        [Display(Name = "Qtde Baixa")]
        public double QtdeBaixa { get; set; }

        [Display(Name = "Situação")]
        public string SituacaoRequisicaoMaterialDescricao { get; set; }
    }
}