using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Fashion.ERP.Web.Helpers;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.UnitOfWork.Logger;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web
{
    public class MvcApplication : HttpApplication
    {        
        #region Variáveis
        private static readonly ILogger Logger;
        #endregion

        #region Construtores
        static MvcApplication()
        {
            var factory = DependencyResolver.Current.GetService<ILoggerFactory>();
            Logger = factory.GetCurrentClassLogger();
        }
        #endregion

        #region Application_Start
        protected void Application_Start()
        {
            Logger.Trace("Inicializando aplicativo.");

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("pt-BR");

            AreaRegistration.RegisterAllAreas();

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BinderConfig.RegisterBinders(ModelBinders.Binders);

            //DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;

            Upload.DeleteAllTempDirectories();
            //Upload.DeleteOldFiles();

            // Faz o log de todas as consultas com o banco de dados (configurado pelo web.config)
            if (Logger.IsTraceEnabled)
                NhLoggerEvents.NhLogger += args => ThreadPool.QueueUserWorkItem(LogNHibernate, args);
        }

        #endregion

        #region Application_EndRequest
        protected void Application_EndRequest()
        {
            //using (var t = NHSession.Current.BeginTransaction())
            //{
            //    try
            //    {
            //        t.Commit();
            //    }
            //    catch (Exception)
            //    {
            //        t.Rollback();
            //        NHSession.Current.Dispose();
            //    }
            //}

            //switch (Context.Response.StatusCode)
            //{
            //    case 403: // Forbidden
            //        Server.TransferRequest(@"~/Error/NoAccess");
            //        break;
            //    case 404: // NotFound
            //        Server.TransferRequest(@"~/Error/NotFound");
            //        break;
            //    case 500: // InternalServerError
            //        Server.TransferRequest(@"~/Error/InternalServerError");
            //        break;
            //    case 501: // NotImplemented
            //    case 502: // BadGateway
            //    case 503: // ServiceUnavailable
            //    case 504: // GatewayTimeout
            //        Server.TransferRequest(@"~/Error/Unknown");
            //        break;
            //}
        }
        #endregion

        #region Session_Start
        protected void Session_Start(Object sender, EventArgs e)
        {
            if (Session.IsNewSession)
            {
                Upload.CreateTempDirectory(Session);
            }
        }
        #endregion

        #region Session_End
        protected void Session_End(object sender, EventArgs e)
        {
            Upload.DeleteTempDirectory(Session);
        }
        #endregion

        #region Application_Error
        protected void Application_Error()
        {
            var lastException = Server.GetLastError();
            Logger.Fatal(lastException.GetMessage());
        }
        #endregion

        #region LogNHibernate
        private static void LogNHibernate(object o)
        {
            var e = o as NhLoggerEventArgs;
            if (e != null) Logger.Trace(e.Mensagem);
        }
        #endregion
    }
}