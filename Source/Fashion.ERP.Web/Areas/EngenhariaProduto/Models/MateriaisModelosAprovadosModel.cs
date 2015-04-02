using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class MateriaisModelosAprovadosModel
    {
        public MateriaisModelosAprovadosModel()
        {
            //preencher as listas para não bugar o componente de multiselect
            Colecoes = new List<long?>();
            ColecoesAprovadas = new List<long?>();
            Categorias = new List<long?>();
            Subcategorias = new List<long?>();
            DepartamentoProducao = new List<long?>();
        }

        [Display(Name = "Coleção")]
        public IList<long?> Colecoes { get; set; }

        [Display(Name = "Coleção Aprovada")]
        public List<long?> ColecoesAprovadas { get; set; }

        [Display(Name = "Categoria")]
        public List<long?> Categorias { get; set; }

        [Display(Name = "Sub-Categoria")]
        public List<long?> Subcategorias { get; set; }
        
        [Display(Name = "Departamento de Produção")]
        public List<long?> DepartamentoProducao { get; set; }
        
        [Display(Name = "Data de Programação da Produção")]
        public DateTime? DataProgramacaoProducao { get; set; }

        [Display(Name = "Tag")]
        public string Tag { get; set; }

        public string ModoConsulta { get; set; }

        [Display(Name = "Agrupar por")]
        public string AgruparPor { get; set; }
    }
}