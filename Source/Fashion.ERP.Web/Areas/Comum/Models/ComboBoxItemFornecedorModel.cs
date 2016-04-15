using System;
using NHibernate.Mapping;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class ComboBoxItemFornecedorModel
    {
        public long Id { get; set; }

        public string Tooltip { get; set; }

        public string CodigoNome { get; set; }
    }
}