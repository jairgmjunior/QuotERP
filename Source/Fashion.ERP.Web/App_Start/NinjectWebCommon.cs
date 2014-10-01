using System.Web.Mvc;
using Fashion.ERP.Web.DI;
using Fashion.ERP.Web.DI.Ninject;
using Fashion.ERP.Web.DI.Ninject.Modules;
using Fashion.ERP.Web.Filters;
using Fashion.Framework.Repository;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Web;
using Ninject.Web.Mvc.FilterBindingSyntax;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Fashion.ERP.Web.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(Fashion.ERP.Web.NinjectWebCommon), "Stop")]

namespace Fashion.ERP.Web
{
    public static class NinjectWebCommon 
    {
        #region Variáveis
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();
        #endregion

        #region Start
        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));

            Bootstrapper.Initialize(CreateKernel);
            Register(Bootstrapper.Kernel);
        }
        #endregion

        #region Stop
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }
        #endregion

        #region CreateKernel
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);
            return kernel;
        }
        #endregion

        #region RegisterServices
        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>)).InSingletonScope();
            kernel.Bind(typeof(IStatelessRepository<>)).To(typeof(StatelessRepository<>)).InSingletonScope();

            kernel.BindFilter<ExceptionFilter>(FilterScope.First, 0);
            kernel.BindFilter<SecurityFilter>(FilterScope.Global, 0);
            kernel.BindFilter<UnitOfWorkFilter>(FilterScope.Action, 0);
        }
        #endregion

        #region Register
        /// <summary>
        /// Registra o InjectableDependencyResolver para resolver as dependências do MvcSiteMapProvider pelo Ninject.
        /// Este método substitui o arquivo DIConfig.cs.
        /// </summary>
        /// <param name="kernel"></param>
        static void Register(IKernel kernel)
        {
            var container = Compose(kernel);

            // Reconfigure MVC to use Service Location
            var dependencyResolver = new InjectableDependencyResolver(container, DependencyResolver.Current);
            DependencyResolver.SetResolver(dependencyResolver);

            MvcSiteMapProviderConfig.Register(container);
        }
        #endregion

        #region Compose
        /// <summary>
        /// Retorna uma instância do wrapper NinjectDependencyInjectionContainer.
        /// Este método substitui o arquivo CompositionRoot.cs.
        /// </summary>
        /// <param name="kernel"></param>
        /// <returns></returns>
        static IDependencyInjectionContainer Compose(IKernel kernel)
        {
            // Setup configuration of DI
            kernel.Load(new MvcSiteMapProviderModule());

            // Return our DI container wrapper instance
            return new NinjectDependencyInjectionContainer(kernel);
        }
        #endregion
    }
}
