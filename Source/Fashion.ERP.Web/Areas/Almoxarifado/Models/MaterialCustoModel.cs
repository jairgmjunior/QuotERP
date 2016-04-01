using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fashion.ERP.Web.Models;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class MaterialCustoModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Referência")]
        public string Referencia { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Unidade de Medida")]
        public string UnidadeMedida { get; set; }

        public string Foto { get; set; }

        public string FuncionarioLogado { get; set; }

        public IList<GridMaterialCustoModel> GridItens { get; set; }
    }
}