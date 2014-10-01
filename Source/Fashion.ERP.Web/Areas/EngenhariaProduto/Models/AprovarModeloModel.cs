using System;
using System.Collections.Generic;
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

        [Display(Name = "Programação produção")]
        public DateTime? ProgramacaoProducao { get; set; }

        [Display(Name = "Quantidade produção")]
        [Required(ErrorMessage = "Informe a quantidade de itens para a produção")]
        public int? QuantidadeProducao { get; set; }

        [Display(Name = "Coleção aprovada")]
        public long? Colecao { get; set; }
        
        public long? Barra { get; set; }
        public long? Segmento { get; set; }

        [Display(Name = "Produto base")]
        public long? ProdutoBase { get; set; }
        public long? Comprimento { get; set; }
        public long? Natureza { get; set; }
        public long? Grade { get; set; }

        [Display(Name = "Dificuldade")]
        public long? ClassificacaoDificuldade { get; set; }

        public string ProdutoBaseDescricao { get; set; }
        public string ComprimentoDescricao { get; set; }
        public string BarraDescricao { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Possui submodelos")]
        public bool PossuiSubmodelos { get; set; }

        public List<int> Sequencias { get; set; }

        public List<long> ProdutoBases { get; set; }

        public List<long> Comprimentos { get; set; }

        public List<string> Descricoes { get; set; }

        public List<long> Barras { get; set; }

        public List<int> QuantidadeProducoes { get; set; }
    }
}