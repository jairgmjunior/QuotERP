using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Financeiro.Models
{
    public class GridBaixaChequeRecebidoModel
    {
        public long Id { get; set; }

        [Display(Name = "Data")]
        public virtual string Data { get; set; }
        
        [Display(Name = "Juros")]
        public virtual string ValorJuros { get; set; }
        
        [Display(Name = "Desconto")]
        public virtual string ValorDesconto { get; set; }
        
        [Display(Name = "Valor")]
        public virtual string Valor { get; set; } 
    }
}