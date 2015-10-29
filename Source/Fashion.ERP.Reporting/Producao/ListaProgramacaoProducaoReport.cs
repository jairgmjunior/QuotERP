using System;

namespace Fashion.ERP.Reporting.Producao
{
    public partial class ListaProgramacaoProducaoReport : Telerik.Reporting.Report
    {
        public ListaProgramacaoProducaoReport()
        {
            InitializeComponent();
        }

        public static object AjusteValores(object value)
        {
            if (value == null || value == DBNull.Value)
                return null;

            if (value is DateTime)
            {
                var datetime = (DateTime)value;
                return datetime.Date.ToString("dd/MM/yyyy");
            }

            return value;
        }
    }
}