using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Financeiro.Models
{
    public class GridOcorrenciaCompensacaoModel
    {
        public long Id { get; set; }

        [Display(Name = "Data")]
        public string Data { get; set; }
        
        [Display(Name = "Situação")]
        public string ChequeSituacao { get; set; }

        [Display(Name = "Compensação")]
        public string Compensacao { get; set; }
    }
}