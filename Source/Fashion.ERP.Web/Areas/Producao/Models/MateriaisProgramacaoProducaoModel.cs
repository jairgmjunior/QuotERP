using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Almoxarifado;

namespace Fashion.ERP.Web.Areas.Producao.Models
{
    public class MateriaisProgramacaoProducaoModel
    {
        public MateriaisProgramacaoProducaoModel()
        {
            //preencher as listas para não bugar o componente de multiselect
            Categorias = new List<long?>();
            Subcategorias = new List<long?>();
            Departamentos = new List<long?>();
            GeneroCategorias = new List<GeneroCategoria>();
        }

        [Display(Name = "Tag")]
        public string Tag { get; set; }

        [Display(Name = "Ano")]
        public long? Ano { get; set; }

        [Display(Name = "Remessa")]
        [Required(ErrorMessage = "Informe a remessa de produção")]
        public long? RemessaProducao { get; set; }

        [Display(Name = "Departamento")]
        public IList<long?> Departamentos { get; set; }

        [Display(Name = "Gênero da Categoria")]
        public IList<GeneroCategoria> GeneroCategorias { get; set; }

        [Display(Name = "Categoria do Material")]
        public IList<long?> Categorias { get; set; }

        [Display(Name = "Subcategoria do Material")]
        public IList<long?> Subcategorias { get; set; }

        [Display(Name = "Referência do Material")]
        public long? Material { get; set; }

        [Display(Name = "Data Programada")]
        public DateTime? DataInicial { get; set; }

        [Display(Name = "Até")]
        public DateTime? DataFinal { get; set; }

        [Display(Name = "Sem Foto")]
        public Boolean SemFoto { get; set; }

        [Display(Name = "Ordenar por")]
        public string OrdenarPor { get; set; }

        [Display(Name = "em")]
        public string OrdenarEm { get; set; }
    }
}