using System;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Fashion.ERP.Web.Helpers.ActionResults;
using Fashion.Framework.Common.Extensions;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Filters
{
    public class ExceptionFilter : FilterAttribute, IExceptionFilter
    {
        #region Variaveis
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public ExceptionFilter(ILogger logger)
        {
            _logger = logger;
        }
        #endregion

        #region OnException
        public void OnException(ExceptionContext filterContext)
        {
            // Cria um log com os erros
            _logger.Info(filterContext.Exception + "\r\n" + filterContext.Exception.StackTrace);

            // Tenta não mostrar a tela de erro do IIS
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

            if (filterContext.IsChildAction || filterContext.ExceptionHandled)
                return;

            // Não tratar exceções que não forem do tipo InternalServerError
            Exception exception = filterContext.Exception;
            if (new HttpException(null, exception).GetHttpCode() != 500)
                return;

            var exceptionMessage = HttpUtility.UrlEncode(exception.GetMessage());

            filterContext.ExceptionHandled = true;

            // Se for uma requisição Ajax, retornar um json com o erro
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonNetResult
                   {
                       Data = new { Error = exception.GetMessage()},
                       JsonRequestBehavior = JsonRequestBehavior.AllowGet
                   };
                return;
            }

            if (filterContext.Exception is System.Security.SecurityException)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                (
                    new
                    {
                        action = "Forbidden",
                        controller = "Error",
                        area = string.Empty
                    }
                ));
            }
            else
            {
                var controllerName = (string)filterContext.RouteData.Values["controller"];
                var actionName = (string)filterContext.RouteData.Values["action"];

                Debugger.Break();
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                (
                    new
                    {
                        action = "InternalServerError",
                        controller = "Error",
                        area = string.Empty,
                        exceptionMessage,
                        controllerName,
                        actionName
                    }
                ));
            }
        }
        #endregion
    }
}