using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class ProgramacaoProducaoMatrizCorteModel : IModel
    {
        public long? Id { get; set; }
        
        [Display(Name = "Quantidade")]
        [Required(ErrorMessage = "Informe a quantidade")]
        public long QuantidadeItem { get; set; }

        [Display(Name = "Total de Enfesto")]
        public long TotalEnfesto
        {
            get
            {
                if (TotalNumeroVezes != 0)
                    return QuantidadeItem / TotalNumeroVezes;
                return 0;
            }
        }

        [Display(Name = "Tipo de Enfesto de Tecido")]
        public int TipoEnfestoTecido { get; set; }

        [Display(Name = "Total do Número de Vezes")]
        public long TotalNumeroVezes { get; set; }

        public IList<ProgramacaoProducaoMatrizCorteItemModel> GridMatrizCorteItens { get; set; }
    }
}