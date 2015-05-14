using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class ModelosAprovadosModel
    {
        [Display(Name = "Coleção Aprovada")]
        [Required(ErrorMessage = "Informe a coleção aprovada")]
        public long? Colecao { get; set; }
        
        [Display(Name = "Catálogo")]
        public Boolean? Catalogo { get; set; }

        [Display(Name = "Dificuldade")]
        public long? ClassificacaoDificuldade { get; set; }

        [Display(Name = "Tag/Ano")]
        public string Tag { get; set; }

        [Display(Name = "Ano")]
        public string Ano { get; set; }

        [Display(Name = "Referência")]
        public long? Material { get; set; }

        [Display(Name = "Período Aprovação")]
        public DateTime? IntervaloInicial { get; set; }

        [Display(Name = "Até")]
        public DateTime? IntervaloFinal { get; set; }

        [Display(Name = "Agrupar por")]
        public string AgruparPor { get; set; }

        [Display(Name = "Ordenar por")]
        public string OrdenarPor { get; set; }

        [Display(Name = "em")]
        public string OrdenarEm { get; set; }
    }
}