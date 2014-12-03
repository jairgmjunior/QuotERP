using System;
using System.Data;
using System.Web.Mvc;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.UnitOfWork;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Filters
{
    public class UnitOfWorkFilter : ActionFilterAttribute
    {
        #region Variaveis
        private readonly ILogger _logger;
        private ActionExecutingContext _executingContext;
        #endregion

        #region Construtores
        public UnitOfWorkFilter(ILogger logger)
        {
            _logger = logger;
        }
        #endregion

        #region OnActionExecuting
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _executingContext = null;

            // Se a action executada for uma Partial
            if (filterContext.IsChildAction)
                return;

            Session.Current.Clear();

            // Se não for um método POST, retornar
            // info: toda a action que alterar alguma coisa no BD deve ser através de um método POST
            if (!filterContext.HttpContext.Request.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase))
                return;

            _executingContext = filterContext;

            if (!Session.Current.Transaction.IsActive)
            {
                Session.Current.BeginTransaction(IsolationLevel.ReadCommitted);
            }

            base.OnActionExecuting(filterContext);
        }
        #endregion

        #region OnActionExecuted
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // Se não for um método POST, retornar
            // info: toda a action que alterar alguma coisa no BD deve ser através de um método POST
            if (!filterContext.HttpContext.Request.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase))
                return;

            // Se a action executada for uma Partial
            if (filterContext.IsChildAction)
                return;

            if (_executingContext == null)
                return;

            try
            {
                if (!Session.Current.Transaction.IsActive) return;

                var modelstate = filterContext.Controller.ViewData.ModelState;

                if ((filterContext.Exception == null || filterContext.ExceptionHandled) && modelstate.IsValid)
                    Session.Current.Transaction.Commit();
                else
                    Session.Current.Transaction.Rollback();
            }
            catch (Exception exception)
            {
                _logger.Info(exception.GetMessage());
                Session.Current.Dispose();
                
                // Limpa as mensagem de sucesso e adiciona uma de erro
                filterContext.Controller.ClearSuccessMessages();
                filterContext.Controller.ViewData.ModelState.AddModelError(string.Empty, exception.GetMessage());

                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new ContentResult { Content = exception.GetMessage() };
                }
                else
                {
                    // Retorna para a página que gerou o erro
                    if (_executingContext.ActionParameters.ContainsKey("model"))
                    {
                        // Recupera o Model do Request e envia novamente para o Response
                        _executingContext.Controller.ViewData.Model = _executingContext.ActionParameters["model"];

                        filterContext.Result = new ViewResult
                        {
                            ViewName = _executingContext.ActionDescriptor.ActionName,
                            TempData = _executingContext.Controller.TempData,
                            ViewData = _executingContext.Controller.ViewData,
                        };
                    }
                    else
                    {
                        filterContext.Controller.AddErrorMessage(exception.GetMessage());
                    }
                }
            }
        }
        #endregion
    }
}