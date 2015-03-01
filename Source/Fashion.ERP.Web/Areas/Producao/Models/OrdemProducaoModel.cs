using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Producao;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class OrdemProducaoModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Número")]
        [Required(ErrorMessage = "Informe o número")]
        public long Numero { get; set; }

        [Display(Name = "TAG / Ano")]
        [Required(ErrorMessage = "Informe a TAG")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Tag { get; set; }

        [Display(Name = "Ano")]
        [Required(ErrorMessage = "Informe o ano")]
        public long Ano { get; set; }

        [Display(Name = "Data OP")]
        [Required(ErrorMessage = "Informe a data da ordem de produção")]
        public DateTime Data { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Data programação")]
        [Required(ErrorMessage = "Informe a data da programação")]
        public DateTime DataProgramação { get; set; }

        [Display(Name = "Situação")]
        [Required(ErrorMessage = "Informe a situação da ordem de produção")]
        public SituacaoOrdemProducao SituacaoOrdemProducao { get; set; }

        [Display(Name = "Tipo OP")]
        [Required(ErrorMessage = "Informe o tipo da ordem de produção")]
        public TipoOrdemProducao TipoOrdemProducao { get; set; }

        [Display(Name = "Data previsão")]
        [Required(ErrorMessage = "Informe a data de previsão")]
        public DateTime DataPrevisao { get; set; }

        [Display(Name = "Observação")]
        [StringLength(4000, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Observacao { get; set; }

        // Matriz solicitada
        public IList<long> ItemVariacaoMatrizes { get; set; }

        public IList<long> ItemTamanhos { get; set; }

        public IList<int> ItemQuantidades { get; set; }

        // Fluxo básico
        public IList<long> FluxoDepartamentos { get; set; }
    }
}