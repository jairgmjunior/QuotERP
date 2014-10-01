using System.Collections.Generic;

namespace Fashion.ERP.Web.Areas.Dev.Models
{
    /// <summary>
    /// Estrutura responsável por retornar os itens da treeview (kendo ui).
    /// </summary>
    public struct Node
    {
        // ReSharper disable InconsistentNaming
        public long id { get; set; }
        public long parentid { get; set; }
        public string text { get; set; }
        public string area { get; set; }
        public string controller { get; set; }
        public string action { get; set; }
        public bool exibeNoMenu { get; set; }
        public IList<Node> items { get; set; }
        // ReSharper restore InconsistentNaming
    }
}