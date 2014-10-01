using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain;

namespace Fashion.ERP.Web.Models
{
    public class RelatorioModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe o nome")]
        public string Nome { get; set; }

        [Display(Name = "Arquivo")]
        public long? Arquivo { get; set; }

        public IList<string> NomeParametro { get; set; }
        public IList<TipoRelatorioParametro> TipoParametro { get; set; }
    }
}