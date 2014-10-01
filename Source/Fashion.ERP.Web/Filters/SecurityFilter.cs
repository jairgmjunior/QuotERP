using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Helpers;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Mvc.Security;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Filters
{
    public class SecurityFilter : AuthorizeAttribute
    {
        #region Variaveis
        private readonly IRepository<Usuario> _usuarioRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public SecurityFilter(ILogger logger,
            IRepository<Usuario> usuarioRepository)
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
        }
        #endregion

        #region AuthorizeCore
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            try
            {
                if (httpContext == null)
                    throw new ArgumentNullException("httpContext");

                // Se é uma requisição ajax, autorizar
                if (httpContext.Request.IsAjaxRequest())
                    return true;

                if (httpContext.Request.Url != null &&
                    FormsAuthentication.LoginUrl == httpContext.Request.Url.LocalPath)
                    return false;

                var userId = FashionSecurity.GetLoggedUserId();

                if (userId.HasValue)
                {
                    var usuario = _usuarioRepository.Get(userId.Value);

                    if (usuario != null)
                    {
                        var rd = httpContext.Request.RequestContext.RouteData;
                        var action = rd.GetRequiredString("action");
                        var controller = rd.GetRequiredString("controller");
                        var area = (rd.Values["area"] ?? rd.DataTokens["area"]) as string;

                        if (PermissaoHelper.PossuiPermissao(action, controller, area))
                            return true;
                    }
                }

            }
            catch (Exception exception)
            {
                _logger.Error(exception.GetMessage());
            }

            return false;
        }
        #endregion
    }
}