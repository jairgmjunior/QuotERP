using System.Reflection;
using System.Web.Mvc;

namespace Fashion.ERP.Web.Helpers.Attributes
{
    /// <summary>
    /// Define um método a ser executado para preencher a ViewData com dados usados na view.
    /// Opcionalmente o método pode receber o model como parâmetro.
    /// </summary>
    public class PopulateViewDataAttribute : ActionFilterAttribute
    {
        #region Variáveis
        private readonly string _methodName;
        #endregion

        #region Construtores
        public PopulateViewDataAttribute(string methodName)
        {
            _methodName = methodName;
        }

        #endregion

        #region OnResultExecuting
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            // Se houver um redirecionamento, não chamar método
            if (filterContext.Result is RedirectToRouteResult ||
                filterContext.Result is JsonResult)
                return;

            var type = filterContext.Controller.GetType();
            var methodInfo = type.GetMethod(_methodName, BindingFlags.NonPublic | BindingFlags.Instance);

            if (methodInfo != null)
            {
                var parameters = methodInfo.GetParameters();

                if (parameters.Length == 0)
                {
                    methodInfo.Invoke(filterContext.Controller, null);
                }
                else
                {
                    var model = new[] { filterContext.Controller.ViewData.Model };
                    methodInfo.Invoke(filterContext.Controller, model);
                }
            }
        }
        #endregion
    }
}