using System.IO;
using System.Linq;
using Telerik.Reporting;

namespace Fashion.ERP.Reporting.Helpers
{
    public static class ReportExtensions
    {
        #region ToByteStream
        public static byte[] ToByteStream(this Report report)
        {
            return new InstanceReportSource {ReportDocument = report}.ToByteStream();
        }
        #endregion

        #region ToByteStream
        public static byte[] ToByteStream(this ReportSource reportSource)
        {
            return new Telerik.Reporting.Processing.ReportProcessor()
                .RenderReport("PDF", reportSource, null)
                .DocumentBytes;
        }
        #endregion

        #region ToStream
        public static Stream ToStream(this Report report)
        {
            return new MemoryStream(report.ToByteStream());
        }
        #endregion

        #region GetTextBox
        public static TextBox GetTextBox(this GroupSection groupSection, string textbox)
        {
            var itens = groupSection.Items.Find(textbox, true);

            if (itens.Any())
                return itens[0] as TextBox;

            return null;
        }
        #endregion

        #region ToSimNao
        public static string ToSimNao(this bool value)
        {
            return value ? "Sim" : "Não";
        }
        #endregion
    }
}