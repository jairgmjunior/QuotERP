using System;
using Fashion.ERP.Domain.Compras;

namespace Fashion.ERP.Reporting.Compras
{
    public partial class PedidoCompraReport : Telerik.Reporting.Report
    {
        public PedidoCompraReport()
        {
            InitializeComponent();
        }

        public static object FreteEnumToString(object value)
        {
            if (value == null || value == DBNull.Value)
                return null;

            if (value is TipoCobrancaFrete)
            {
                var tipoCobrancaFrete = (TipoCobrancaFrete)value;
                return tipoCobrancaFrete.EnumToString();
            }

            return value;
        }
    }
}