using System;
using System.Web;
using System.Web.Mvc;

namespace Fashion.ERP.Web.Helpers.ModelBinders
{
    public class TrimModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            try
            {
                var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
                if (valueResult == null || string.IsNullOrEmpty(valueResult.AttemptedValue))
                    return null;

                return valueResult.AttemptedValue.Trim();
            }
            catch (Exception exception)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, exception.Message);
            }

            return null;
        }
    }
}