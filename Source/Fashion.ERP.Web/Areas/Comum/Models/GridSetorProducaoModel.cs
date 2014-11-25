using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class GridSetorProducaoModel
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        
        [Display(Name = "Departamento produtivo")]
        public string DepartamentoProducao { get; set; }

        public bool Ativo { get; set; }
    }
}