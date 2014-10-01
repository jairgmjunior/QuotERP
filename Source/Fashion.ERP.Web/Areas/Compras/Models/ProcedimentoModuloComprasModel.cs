using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class AutorizacoesModel : IModel
    {
        public AutorizacoesModel()
        {
            Funcionarios = new List<long?>();
        }

        public long? Id { get; set; }

        [Display(Name = "Procedimento")]
        public string Procedimento { get; set; }

        [Display(Name = "Funcionário")]
        public IList<long?> Funcionarios { get; set; }
    }
}