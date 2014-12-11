using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class SequenciaOperacionalModel
    {
        public long? Id { get; set; }
        public long? NaturezaId { get; set; }

        [Display(Name = "Departamento")]
        [Required(ErrorMessage = "Informe o departamento.")]
        public String NomeDepartamento { get; set; }

        public long? DepartamentoId { get; set; }

        [Display(Name = "Setor")]
        [Required(ErrorMessage = "Informe o setor.")]
        public String NomeSetor { get; set; }

        public long? SetorId { get; set; }

        [Display(Name = "Operação")]
        [Required(ErrorMessage = "Informe a operação.")]
        public String NomeOperacao { get; set; }

        public long? OperacaoId { get; set; }


    }
}