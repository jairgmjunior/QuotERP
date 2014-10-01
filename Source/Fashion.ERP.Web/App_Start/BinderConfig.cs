using System.Web.Mvc;
using Fashion.ERP.Web.Helpers.ModelBinders;

namespace Fashion.ERP.Web
{
    public class BinderConfig
    {
        public static void RegisterBinders(ModelBinderDictionary binders)
        {
            binders.Add(typeof(string), new TrimModelBinder());
        }
    }
}