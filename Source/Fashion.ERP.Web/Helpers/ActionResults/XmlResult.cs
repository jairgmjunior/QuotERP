using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;

namespace Fashion.ERP.Web.Helpers.ActionResults
{
    internal class XmlResult : ActionResult
    {
        public object Data { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = "text/xml";

            var writer = XmlWriter.Create(response.Output, new XmlWriterSettings() { OmitXmlDeclaration = true });

            new XmlSerializer(Data.GetType()).Serialize(writer, Data, new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") }));
        }
    }
}