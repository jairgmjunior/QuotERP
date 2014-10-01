using System.Collections.Generic;

namespace Fashion.ERP.Web.Areas.Dev.Models
{
    public class MenuBuilderModel
    {
        public IList<Node> ItensMenu { get; set; }
        public IList<Node> ItensDisponiveis { get; set; }
    }
}