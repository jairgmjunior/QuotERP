using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class ConsultaEstoqueMaterialModel
    {
        [Display(Name = "Unidade")]
        public long? Unidade { get; set; }

        [Display(Name = "Depósito")]
        public long? DepositoMaterial { get; set; }

        [Display(Name = "Referência")]
        public long? Material { get; set; }
        
        [Display(Name = "Descrição")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Descricao { get; set; }

        [Display(Name = "Categoria")]
        public List<string> Categorias { get; set; }

        [Display(Name = "Subcategoria")]
        public List<string> Subcategorias { get; set; }

        [Display(Name = "Família")]
        public List<string> Familias { get; set; }

        [Display(Name = "Marca")]
        public List<string> Marcas { get; set; }

        public string ModoConsulta { get; set; }

        [Display(Name = "Ordenar por")]
        public string OrdenarPor { get; set; }

        [Display(Name = "em")]
        public string OrdenarEm { get; set; }

        public IList<GridConsultaEstoqueMaterialModel> Grid { get; set; }
    }
}