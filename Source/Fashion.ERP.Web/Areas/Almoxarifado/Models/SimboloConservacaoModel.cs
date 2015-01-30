using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class SimboloConservacaoModel : IModel
    {

        public long? Id { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Digite uma descrição")]
        public String Descricao { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "Selecione uma categoria")]
        public CategoriaConservacao CategoriaConservacao { get; set; }

        public long? FotoId { get; set; }

        [Display(Name = "Imagem")]
        [Required(ErrorMessage = "Informe a imagem")]
        public string FotoNome { get; set; }
    }
}