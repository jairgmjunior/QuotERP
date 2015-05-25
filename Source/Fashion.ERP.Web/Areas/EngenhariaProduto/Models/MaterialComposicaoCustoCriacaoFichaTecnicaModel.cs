using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class MaterialComposicaoCustoCriacaoFichaTecnicaModel
    {
        [Display(Name = "Id")]
        public long? Id { get; set; }

        [Display(Name = "Material")]
        public long? MaterialId { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Referência")]
        public string Referencia { get; set; }

        [Display(Name = "Unidade")]
        public string UnidadeMedida { get; set; }
        
        [Display(Name = "Custo")]
        public double? Custo { get; set; }
    }
}