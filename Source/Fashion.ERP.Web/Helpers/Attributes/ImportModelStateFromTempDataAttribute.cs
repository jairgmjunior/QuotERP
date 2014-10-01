using System.Web.Mvc;

namespace Fashion.ERP.Web.Helpers.Attributes
{
    public class ImportModelStateFromTempDataAttribute : ModelStateTempDataTransfer
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var modelState = filterContext.Controller.TempData[Key] as ModelStateDictionary;

            if (modelState != null)
            {
                //Only Import if we are viewing
                if (filterContext.Result is ViewResult)
                {
                    filterContext.Controller.ViewData.ModelState.Merge(modelState);
                }
                else
                {
                    //Otherwise remove it.
                    filterContext.Controller.TempData.Remove(Key);
                }
            }

            base.OnActionExecuted(filterContext);
        }
    }
}