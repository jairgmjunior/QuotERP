using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;
using System.Collections.Generic;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class NaturezaModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Informe a descrição")]
        [StringLength(60, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Descricao { get; set; }

        public List<SequenciaOperacionalModel> SequenciasOperacionais { get; set; }
    }
}