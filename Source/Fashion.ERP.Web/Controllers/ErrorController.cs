using System;
using System.Web;
using Ninject.Extensions.Logging;
using System.Web.Mvc;

namespace Fashion.ERP.Web.Controllers
{
    [AllowAnonymous]
    public partial class ErrorController : Controller
    {
        #region Variaveis
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public ErrorController(ILogger logger)
        {
            _logger = logger;
        }
        #endregion

        public virtual ActionResult Unknown()
        {
            _logger.Trace("Erro esconhecido.");
            return View();
        }

        public virtual ActionResult Forbidden()
        {
            _logger.Trace("Acesso negado.");
            return View();
        }

        public virtual ActionResult NotFound()
        {
            if (HttpContext.Request.UrlReferrer != null)
                _logger.Trace("Não encontrado: " + HttpContext.Request.UrlReferrer.PathAndQuery);
            
            return View();
        }

        [ValidateInput(false)]
        public virtual ActionResult InternalServerError(string exceptionMessage, string controllerName, string actionName)
        {
            if (controllerName == "Error" && actionName == "InternalServerError")
                return new EmptyResult();

            if (string.IsNullOrEmpty(exceptionMessage) ||
                string.IsNullOrEmpty(controllerName) ||
                string.IsNullOrEmpty(actionName))
                return RedirectToAction("Unknown");

            _logger.Trace("Erro do servidor.");

            return View(new HandleErrorInfo(new Exception(HttpUtility.UrlDecode(exceptionMessage)), controllerName, actionName));
        }
    }
}