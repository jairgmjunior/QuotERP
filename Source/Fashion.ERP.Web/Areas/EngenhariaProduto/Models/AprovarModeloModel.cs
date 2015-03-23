using System;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class AprovarModeloModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Referência")]
        public string Referencia { get; set; }

        [Display(Name = "Descrição")]
        public string DescricaoModelo { get; set; }

        [Display(Name = "Estilista")]
        public string EstilistaNome { get; set; }

        [Display(Name = "Natureza")]
        public string NaturezaDescricao { get; set; }

        [Display(Name = "Artigo")]
        public string ArtigoDescricao { get; set; }

        [Display(Name = "Criação")]
        public DateTime DataCriacao { get; set; }

        [Display(Name = "Classificacao")]
        public string ClassificacaoDescricao { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Data aprovação")]
        [Required(ErrorMessage = "Informe a data de aprovação")]
        public DateTime? DataAprovacao { get; set; }

        [Display(Name = "TAG")]
        [Required(ErrorMessage = "Informe a TAG")]
        public string Tag { get; set; }

        [Display(Name = "Observação de aprovação")]
        [DataType(DataType.MultilineText)]
        [StringLength(250, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string ObservacaoAprovacao { get; set; }

        [Display(Name = "Programação de produção")]
        [Required(ErrorMessage = "informe a data de programação da produção")]
        public DateTime? ProgramacaoProducao { get; set; }

        [Display(Name = "Quantidade produção")]
        [Required(ErrorMessage = "Informe a quantidade de itens para a produção")]
        public long? QuantidadeProducao { get; set; }

        [Display(Name = "Coleção aprovada")]
        [Required(ErrorMessage = "Informe a coleção aprovada")]
        public long? Colecao { get; set; }

        [Display(Name = "Dificuldade")]
        [Required(ErrorMessage = "Informe a dificuldade")]
        public long? ClassificacaoDificuldade { get; set; }
    }
}