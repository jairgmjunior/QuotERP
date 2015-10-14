using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Compras;

namespace Fashion.ERP.Web.Areas.Compras.Models
{
    public class MateriaisPedidosCompraModel
    {
        public MateriaisPedidosCompraModel()
        {
            //preencher as listas para não bugar o componente de multiselect
            Categorias = new List<long?>();
            Subcategorias = new List<long?>();
            SituacoesCompra = new List<SituacaoCompra?>();
        }
        
        [Display(Name = "Categoria")]
        public List<long?> Categorias { get; set; }

        [Display(Name = "Subcategoria")]
        public List<long?> Subcategorias { get; set; }
        
        [Display(Name = "Data de Entrega")]
        public DateTime? DataEntregaInicial { get; set; }

        [Display(Name = "Até")]
        public DateTime? DataEntregaFinal { get; set; }
        
        [Display(Name = "Número")]
        public long? Numero { get; set; }
        
        [Display(Name = "Referência do Material")]
        public long? Material { get; set; }

        [Display(Name = "Fornecedor")]
        public long? Fornecedor { get; set; }
        
        [Display(Name = "Situação")]
        public List<SituacaoCompra?> SituacoesCompra { get; set; }
        
        public SituacaoCompra Situacao { get; set; }
        
        public string ModoConsulta { get; set; }

        [Display(Name = "Agrupar por")]
        public string AgruparPor { get; set; }

        [Display(Name = "Ordenar por")]
        public string OrdenarPor { get; set; }

        [Display(Name = "em")]
        public string OrdenarEm { get; set; }

    }
}