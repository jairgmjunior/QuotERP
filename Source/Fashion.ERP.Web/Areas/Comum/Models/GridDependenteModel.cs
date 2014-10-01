using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class GridDependenteModel
    {
        public long Id { get; set; }

        [Display(Name = "Cliente")]
        public string Cliente { get; set; }
        
        [Display(Name = "Grau de dependência")]
        public string GrauDependencia { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }
        
        [Display(Name = "CPF")]
        public string Cpf { get; set; }
        
        [Display(Name = "RG")]
        public string Rg { get; set; }
        
        [Display(Name = "Órgão expedidor")]
        public string OrgaoExpedidor { get; set; } 
    }
}