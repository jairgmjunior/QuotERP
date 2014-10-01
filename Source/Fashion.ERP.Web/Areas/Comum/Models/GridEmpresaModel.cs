using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class GridEmpresaModel
    {
        public long Id { get; set; }

        [Display(Name = "Código")]
        public long Codigo { get; set; }

        [Display(Name = "CNPJ")]
        public string CpfCnpj { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        public bool Ativo { get; set; }
    }
}