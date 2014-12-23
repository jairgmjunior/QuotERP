using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class ReservaMaterialModel : IModel
    {
        public long? Id { get; set; }
        
        [Display(Name = "Número")]
        public long? Numero { get; set; }

        [Display(Name = "Data")]
        public DateTime? Data { get; set; }
        
        [Display(Name = "Data de Programação")]
        [Required(ErrorMessage = "Informe a data de programação")]
        public DateTime? DataProgramacao { get; set; }

        [Display(Name = "Observação")]
        [DataType(DataType.MultilineText)]
        public string Observacao { get; set; }

        [Display(Name = "Requerente")]
        [Required(ErrorMessage = "Informe o requerente")]
        public long RequerenteId { get; set; }

        [Display(Name = "Unidade")]
        [Required(ErrorMessage = "Informe a unidade")]
        public long UnidadeId { get; set; }

        [Display(Name = "Coleção")]
        [Required(ErrorMessage = "Informe a coleção")]
        public long ColecaoId { get; set; }

        [Display(Name = "Referência de Origem")]
        [Required(ErrorMessage = "Informe a referência de origem")]
        public String ReferenciaOrigem { get; set; }

        [Display(Name = "Situação")]
        [Required(ErrorMessage = "Informe a situação")]
        public SituacaoReservaMaterial SituacaoReservaMaterial { get; set; }
        
        public IList<ReservaMaterialItemModel> GridItens { get; set; }
    }
}