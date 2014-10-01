using System.Web.Mvc;

namespace Fashion.ERP.Web.Helpers.Attributes
{
    public abstract class ModelStateTempDataTransfer : ActionFilterAttribute
    {
        protected static readonly string Key = typeof(ModelStateTempDataTransfer).FullName;
    }
}