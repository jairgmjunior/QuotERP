using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Web.Areas.Almoxarifado.Models;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class PesquisaSimboloConservacaoModel
    {
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Categoria")]
        public CategoriaConservacao? CategoriaConservacao { get; set; }


        public string ModoConsulta { get; set; }

        [Display(Name = "Agrupar por")]
        public string AgruparPor { get; set; }

        [Display(Name = "Ordenar por")]
        public string OrdenarPor { get; set; }

        [Display(Name = "em")]
        public string OrdenarEm { get; set; }

        public IList<GridSimboloConservacaoModel> Grid { get; set; }
    }
}