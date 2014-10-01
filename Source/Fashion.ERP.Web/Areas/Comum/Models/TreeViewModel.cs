using System.Collections.Generic;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class TreeViewModel
    {
        public TreeViewModel()
        {
            Itens = new List<TreeViewModel>();
        }

        public long? Id { get; set; }

        public string Name { get; set; }

        public bool IsChecked { get; set; }

        public IList<TreeViewModel> Itens { get; set; }
    }
}