using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class GridDepositoMaterialModel
    {
        public long Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Data abertura")]
        public DateTime DataAbertura { get; set; }

        [Display(Name = "Unidade")]
        public string Unidade { get; set; }

        public bool Ativo { get; set; }
    }
}