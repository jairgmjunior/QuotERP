using System;

namespace Fashion.ERP.Reporting.Compras
{
    using Telerik.Reporting;
    
    public partial class ListaRecebimentoCompraReport : Report
    {
        public ListaRecebimentoCompraReport()
        {
            InitializeComponent();
        }

        public static object AjusteValores(object value)
        {
            if (value == null || value == DBNull.Value)
                return null;

            if (value is DateTime)
            {
                var datetime = (DateTime) value;
                return datetime.Date.ToString("dd/MM/yyyy");
            }

            return value;
        }
    }
}