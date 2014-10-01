﻿using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class TamanhoModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Informe a descrição")]
        [StringLength(60, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe a sigla")]
        [StringLength(10, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Sigla { get; set; }
    }
}