using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class SequenciaProducaoModel
    {
        public long? Id { get; set; }

        public long? ModeloId { get; set; }

        [Display(Name = "Departamento")]
        [Required(ErrorMessage = "Informe o departamento.")]
        public String NomeDepartamento { get; set; }

        public long? DepartamentoId { get; set; }
        
        [Display(Name = "Setor")]
        public String NomeSetor { get; set; }

        public long? SetorId { get; set; }
    }
}