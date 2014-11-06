using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Financeiro;

namespace Fashion.ERP.Web.Areas.Financeiro.Models
{
    public class PesquisaTituloPagarModel
    {
        [Display(Name = "Unidade")]
        public long? Unidade { get; set; } 

        [Display(Name = "Número")]
        public string Numero { get; set; }

        [Display(Name = "Fornecedor")]
        public long? Fornecedor { get; set; }

        [Display(Name = "Situação")]
        public SituacaoTitulo? SituacaoTitulo { get; set; }

        [Display(Name = "Data emissão")]
        public DateTime? DataEmissaoInicio { get; set; }
        
        [Display(Name = "Até")]
        public DateTime? DataEmissaoFim { get; set; }

        [Display(Name = "Data cadastro")]
        public DateTime? DataCadastroInicio { get; set; }

        [Display(Name = "Até")]
        public DateTime? DataCadastroFim { get; set; }

        [Display(Name = "Data vencimento")]
        public DateTime? DataVencimentoInicio { get; set; }

        [Display(Name = "Até")]
        public DateTime? DataVencimentoFim { get; set; }

        [Display(Name = "Valor")]
        public double? ValorInicio { get; set; }

        [Display(Name = "Até")]
        public double? ValorFim { get; set; }

        public string ModoConsulta { get; set; }

        [Display(Name = "Agrupar por")]
        public string AgruparPor { get; set; }

        [Display(Name = "Ordenar por")]
        public string OrdenarPor { get; set; }

        [Display(Name = "em")]
        public string OrdenarEm { get; set; }

        public IList<GridTituloPagarModel> Grid { get; set; }
    }
}