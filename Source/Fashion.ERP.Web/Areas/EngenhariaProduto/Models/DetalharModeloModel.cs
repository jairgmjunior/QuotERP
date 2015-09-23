using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class DetalharModeloModel
    {
        public long Id { get; set; }

        [Display(Name = "Referência")]
        public string Referencia { get; set; }

        [Display(Name = "Coleção")]
        public string Colecao { get; set; }

        [Display(Name = "Estilista")]
        public string Estilista { get; set; }

        [Display(Name = "Data criação")]
        public string DataCriacao { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Natureza")]
        public string Natureza { get; set; }

        [Display(Name = "Classificação")]
        public string Classificacao { get; set; }

        [Display(Name = "Artigo")]
        public string Artigo { get; set; }

        [Display(Name = "Produto base")]
        public string ProdutoBase { get; set; }

        [Display(Name = "Comprimento")]
        public string Comprimento { get; set; }

        [Display(Name = "Tipo de barra")]
        public string Barra { get; set; }

        [Display(Name = "Detalhamento")]
        public string Detalhamento { get; set; }

        [Display(Name = "Grade")]
        public string Grade { get; set; }

        [Display(Name = "Tamanho padrão")]
        public string TamanhoPadrao { get; set; }

        [Display(Name = "Marca")]
        public string Marca { get; set; }

        [Display(Name = "Segmento")]
        public string Segmento { get; set; }

        [Display(Name = "Lavada")]
        public string Lavada { get; set; }
        
        [Display(Name = "Linha casa")]
        public string LinhaCasa { get; set; }

        [Display(Name = "Situação")]
        public string Situacao { get; set; }

        [Display(Name = "Data aprovação")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? DataAprovacao { get; set; }

        [Display(Name = "Fotos")]
        public List<ModeloFotoModel> Fotos { get; set; }

        [Display(Name = "Linha bordado")]
        public string[] LinhaBordados { get; set; }

        [Display(Name = "Linha pesponto")]
        public string[] LinhaPespontos { get; set; }

        [Display(Name = "Linha travete")]
        public string[] LinhaTravetes { get; set; }

        [Display(Name = "Variação")]
        public Dictionary<string, List<string>> Variacoes { get; set; }

        [Display(Name = "Sequência Produção")]
        public List<GridSequenciaProducaoModel> SequenciaProducao { get; set; }

        [Display(Name = "Modelista")]
        public string Modelista { get; set; }

        [Display(Name = "Cós")]
        public string Cos { get; set; }

        [Display(Name = "Passante")]
        public string Passante { get; set; }

        [Display(Name = "Entrepernas")]
        public string Entrepernas { get; set; }

        [Display(Name = "Boca")]
        public string Boca { get; set; }

        [Display(Name = "Modelagem")]
        public string Modelagem { get; set; }

        [Display(Name = "Etiqueta da marca")]
        public string EtiquetaMarca { get; set; }

        [Display(Name = "Etiqueta composicao")]
        public string EtiquetaComposicao { get; set; }

        [Display(Name = "Tag")]
        public string Tag { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        [Display(Name = "Forro")]
        public string Forro { get; set; }

        [Display(Name = "Tecido Comp.")]
        public string TecidoComplementar { get; set; }

        [Display(Name = "Dificuldade")]
        public string Dificuldade { get; set; }

        [Display(Name = "Dificuldade")]
        public string DificuldadeAprovacao { get; set; }

        [Display(Name = "Qtd. Produção")]
        public long QuantidadeMix { get; set; }
        
        [Display(Name = "Complemento")]
        public string ComplementoAprovacao { get; set; }

        [Display(Name = "Submodelos Aprovados")]
        public int QuantidadeSubmodelos { get; set; }
    }
}