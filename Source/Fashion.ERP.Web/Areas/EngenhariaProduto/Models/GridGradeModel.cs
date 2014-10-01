using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class GridGradeModel
    {
        public long Id { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Data de criação")]
        public DateTime DataCriacao { get; set; }

        public bool Ativo { get; set; }
    }
}