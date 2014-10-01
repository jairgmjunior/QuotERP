using Fashion.ERP.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class CopidarModeloModel
    {
        [Display(Name = "Referência existente")]
        [Required(ErrorMessage = "Informe a referência existente")]
        [StringLength(50, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string ReferenciaExistente { get; set; }

        [Display(Name = "Referência modelo novo")]
        [Required(ErrorMessage = "Informe a referência para um novo modelo")]
        [StringLength(50, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string ReferenciaNova { get; set; }
    }
}