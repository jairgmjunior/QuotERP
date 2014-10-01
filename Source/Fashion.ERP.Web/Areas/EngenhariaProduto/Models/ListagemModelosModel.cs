using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class ListagemModelosModel
    {
        [Display(Name = "Coleção")]
        public long? Colecao { get; set; }

        [Display(Name = "Estilista")]
        public long? Estilista { get; set; }

        [Display(Name = "Referência")]
        public string Referencia { get; set; }

        [Display(Name = "Referência Catálogo Material")]
        public string ReferenciaMaterial { get; set; }

        [Display(Name = "Natureza")]
        public long? Natureza { get; set; }

        [Display(Name = "Classificação")]
        public long? Classificacao { get; set; }

        public long? Grade { get; set; }

        public long? Marca { get; set; }

        public long? Comprimento { get; set; }

        public long? Barra { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Produto base")]
        public long? ProdutoBase { get; set; }

        [Display(Name = "Modelista")]
        public long? Modelista { get; set; }

        [Display(Name = "Classificação de Dificuldade")]
        public long? ClassificacaoDificuldade { get; set; }

        [Display(Name = "Tag")]
        public string Tag { get; set; }

        [Display(Name = "Aprovado em")]
        public DateTime?  DataInicial { get; set; }

        [Display(Name = "Até")]
        public DateTime? DataFinal { get; set; }

        [Display(Name = "Agrupar por")]
        public string AgruparPor { get; set; }

        [Display(Name = "Ordenar por")]
        public string OrdenarPor { get; set; }

        [Display(Name = "em")]
        public string OrdenarEm { get; set; }
    }
}