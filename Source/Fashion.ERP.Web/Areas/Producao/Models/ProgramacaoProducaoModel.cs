using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class ProgramacaoProducaoModel : IModel
    {
        public long? Id { get; set; }
        
        [Display(Name = "Número")]
        public long? Numero { get; set; }

        [Display(Name = "Data")]
        public DateTime? Data { get; set; }

        [Display(Name = "Data Programada")]
        [Required(ErrorMessage = "Informe a data programada")]
        public DateTime? DataProgramada { get; set; }

        [Display(Name = "Observação")]
        [DataType(DataType.MultilineText)]
        public string Observacao { get; set; }

        [Display(Name = "Responsável")]
        [Required(ErrorMessage = "Informe o responsável")]
        public long? Responsavel { get; set; }

        [Display(Name = "Coleção")]
        [Required(ErrorMessage = "Informe a coleção")]
        public long? Colecao { get; set; }

        [Display(Name = "Referência")]
        [Required(ErrorMessage = "Informe a ficha técnica")]
        public long? FichaTecnica { get; set; }

        [Display(Name = "Quantidade")]
        [Required(ErrorMessage = "Informe a quantidade")]
        public long Quantidade { get; set; }

        [Display(Name = "Total de Enfesto")]
        public long TotalEnfesto
        {
            get
            {
                if (TotalNumeroVezes != 0)
                    return Quantidade / TotalNumeroVezes;
                return 0;
            }
        }

        [Display(Name = "Tipo de Enfesto de Tecido")]
        public TipoEnfestoTecido TipoEnfestoTecido { get; set; }

        [Display(Name = "Total do Número de Vezes")]
        public long TotalNumeroVezes { get; set; }

        public IList<ProgramacaoProducaoMatrizCorteItemModel> GridItens { get; set; }
    }
}