using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class ProducaoProgramacaoMatrizCorteItemModel
    {
        [Display(Name = "Quantidade")]
        public long? Quantidade { get; set; }

        [Display(Name = "Número de Vezes")]
        [UIHint("int")]
        public long? QuantidadeVezes { get; set; }
        
        public long? Tamanho { get; set; }
        
        [Display(Name = "Tamanho")]
        public string DescricaoTamanho { get; set; }
    }
}