using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class ModeloAprovacaoMatrizCorteModel : IModel
    {
        public long? Id { get; set; }

        public long? IdModeloAprovacao { get; set; }

        public long? IdModelo { get; set; }

        [Display(Name = "Descrição")]
        public string DescricaoModelo { get; set; }

        [Display(Name = "Referência")]
        public string ReferenciaModelo { get; set; }

        [Display(Name = "Estilista")]
        public string EstilistaModelo { get; set; }

        [Display(Name = "Tecido")]
        public string Tecido { get; set; }
        
        [Display(Name = "Coleção")]
        public string Colecao { get; set; }

        [Display(Name = "Forro")]
        public string Forro { get; set; }

        [Display(Name = "Tag/Ano")]
        public string Tag { get; set; }

        [Display(Name = "Coleção Aprovada")]
        public string ColecaoAprovada { get; set; }

        [Display(Name = "Catálogo")]
        public string Catalogo { get; set; }

        [Display(Name = "Complemento")]
        public string Complemento { get; set; }

        [Display(Name = "Dificuldade")]
        public string Dificuldade { get; set; }

        [Display(Name = "Qtde. Total Aprovada")]
        public long QtdeTotalAprovada { get; set; }

        [Display(Name = "Referência")]
        public string Referencia { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Quantidade")]
        [Required(ErrorMessage = "Informe a quantidade")]
        public long Quantidade { get; set; }

        [Display(Name = "Total de Enfesto")]
        public long TotalEnfesto
        {
            get
            {
                if(TotalNumeroVezes != 0)
                    return Quantidade/TotalNumeroVezes;
                return 0;
            }
        }
        
        [Display(Name = "Tipo de Enfesto de Tecido")]
        public TipoEnfestoTecido TipoEnfestoTecido { get; set; }

        [Display(Name = "Total da Quantidade")]
        public long TotalQuantidade { get; set; }

        [Display(Name = "Total do Número de Vezes")]
        public long TotalNumeroVezes { get; set; }
        
        public IList<ModeloAprovacaoMatrizCorteItemModel> GridItens { get; set; }
    }
}