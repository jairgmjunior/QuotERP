using System;
using System.Web.Mvc;
using System.Web.Hosting;
using Fashion.ERP.Web.Helpers.Caching;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Web.Mvc;
using MvcSiteMapProvider.Web.Compilation;
using MvcSiteMapProvider.Web.UrlResolver;
using MvcSiteMapProvider.Security;
using MvcSiteMapProvider.Visitor;
using MvcSiteMapProvider.Builder;
using MvcSiteMapProvider.Caching;
using MvcSiteMapProvider.Xml;
using Ninject;
using Ninject.Modules;


namespace Fashion.ERP.Web.DI.Ninject.Modules
{
    public class MvcSiteMapProviderModule   : NinjectModule
    {
        public override void Load()
        {
            const bool securityTrimmingEnabled = false;
            const bool enableLocalization = true;
            string absoluteFileName = HostingEnvironment.MapPath("~/Mvc.sitemap");
            TimeSpan absoluteCacheExpiration = TimeSpan.FromMinutes(5);
            var includeAssembliesForScan = new[] { "Fashion.ERP.Web" };

            var currentAssembly = GetType().Assembly;
            var siteMapProviderAssembly = typeof(SiteMaps).Assembly;
            var allAssemblies = new[] { currentAssembly, siteMapProviderAssembly };
            var excludeTypes = new[] {
                typeof(SiteMapNodeVisibilityProviderStrategy),
                typeof(SiteMapXmlReservedAttributeNameProvider),
                typeof(SiteMapBuilderSetStrategy)
            };
            var multipleImplementationTypes = new[] {
                typeof(ISiteMapNodeUrlResolver),
                typeof(ISiteMapNodeVisibilityProvider),
                typeof(IDynamicNodeProvider)
            };

// Single implementations of interface with matching name (minus the "I").
            CommonConventions.RegisterDefaultConventions(
                (interfaceType, implementationType) => Kernel.Bind(interfaceType).To(implementationType).InSingletonScope(),
                new[] { siteMapProviderAssembly },
                allAssemblies,
                excludeTypes,
                string.Empty);

// Multiple implementations of strategy based extension points
            CommonConventions.RegisterAllImplementationsOfInterface(
                (interfaceType, implementationType) => Kernel.Bind(interfaceType).To(implementationType).InSingletonScope(),
                multipleImplementationTypes,
                allAssemblies,
                excludeTypes,
                "^Composite");

            Kernel.Bind<ISiteMapNodeVisibilityProviderStrategy>().To<SiteMapNodeVisibilityProviderStrategy>()
                .WithConstructorArgument("defaultProviderName", string.Empty);

            Kernel.Bind<ControllerBuilder>().ToConstant(ControllerBuilder.Current);
            Kernel.Bind<IControllerBuilder>().To<ControllerBuilderAdaptor>();
            Kernel.Bind<IBuildManager>().To<BuildManagerAdaptor>();

// Configure Security
            Kernel.Bind<AuthorizeAttributeAclModule>().ToSelf();
            Kernel.Bind<XmlRolesAclModule>().ToSelf();
            Kernel.Bind<IAclModule>().To<CompositeAclModule>()
                .WithConstructorArgument("aclModules",
                    new IAclModule[] {
                        Kernel.Get<AuthorizeAttributeAclModule>(),
                        Kernel.Get<XmlRolesAclModule>()
                    });


// Setup cache
            Kernel.Bind<System.Runtime.Caching.ObjectCache>()
                .ToConstant<System.Runtime.Caching.ObjectCache>(System.Runtime.Caching.MemoryCache.Default);
            //this.Kernel.Bind(typeof(ICacheProvider<>)).To(typeof(RuntimeCacheProvider<>));
            Kernel.Bind<ICacheProvider<ISiteMap>>().To<UserCacheProvider<ISiteMap>>(); // Cache por usuário
            Kernel.Bind<ICacheDependency>().To<RuntimeFileCacheDependency>().Named("cacheDependency1")
                .WithConstructorArgument("fileName", absoluteFileName);

            Kernel.Bind<ICacheDetails>().To<CacheDetails>().Named("cacheDetails1")
                .WithConstructorArgument("absoluteCacheExpiration", absoluteCacheExpiration)
                .WithConstructorArgument("slidingCacheExpiration", TimeSpan.MinValue)
                .WithConstructorArgument("cacheDependency", Kernel.Get<ICacheDependency>("cacheDependency1"));

// Configure the visitors
            Kernel.Bind<ISiteMapNodeVisitor>().To<UrlResolvingSiteMapNodeVisitor>();

// Prepare for our node providers
            Kernel.Bind<IXmlSource>().To<FileXmlSource>().Named("XmlSource1")
                .WithConstructorArgument("fileName", absoluteFileName);
            Kernel.Bind<ISiteMapXmlReservedAttributeNameProvider>().To<SiteMapXmlReservedAttributeNameProvider>().Named("xmlBuilderReservedAttributeNameProvider")
                .WithConstructorArgument("attributesToIgnore", new string[0]);

// Register the sitemap node providers
            Kernel.Bind<ISiteMapNodeProvider>().To<XmlSiteMapNodeProvider>().Named("xmlSiteMapNodeProvider1")
                .WithConstructorArgument("includeRootNode", true)
                .WithConstructorArgument("useNestedDynamicNodeRecursion", false)
                .WithConstructorArgument("xmlSource", Kernel.Get<IXmlSource>("XmlSource1"));

            Kernel.Bind<ISiteMapNodeProvider>().To<ReflectionSiteMapNodeProvider>().Named("reflectionSiteMapNodeProvider1")
                .WithConstructorArgument("includeAssemblies", includeAssembliesForScan)
                .WithConstructorArgument("excludeAssemblies", new string[0]);

            Kernel.Bind<ISiteMapNodeProvider>().To<CompositeSiteMapNodeProvider>().Named("siteMapNodeProvider1")
                .WithConstructorArgument("siteMapNodeProviders",
                    new[] {
                        Kernel.Get<ISiteMapNodeProvider>("xmlSiteMapNodeProvider1"),
                        Kernel.Get<ISiteMapNodeProvider>("reflectionSiteMapNodeProvider1")
                    });

// Register the sitemap builders
            Kernel.Bind<ISiteMapBuilder>().To<SiteMapBuilder>().Named("siteMapBuilder1")
                .WithConstructorArgument("siteMapNodeProvider", Kernel.Get<ISiteMapNodeProvider>("siteMapNodeProvider1"));

// Configure the builder sets
            Kernel.Bind<ISiteMapBuilderSet>().To<SiteMapBuilderSet>().Named("siteMapBuilderSet1")
                .WithConstructorArgument("instanceName", "default")
                .WithConstructorArgument("securityTrimmingEnabled", securityTrimmingEnabled)
                .WithConstructorArgument("enableLocalization", enableLocalization)
                .WithConstructorArgument("siteMapBuilder", Kernel.Get<ISiteMapBuilder>("siteMapBuilder1"))
                .WithConstructorArgument("cacheDetails", Kernel.Get<ICacheDetails>("cacheDetails1"));

            Kernel.Bind<ISiteMapBuilderSetStrategy>().To<SiteMapBuilderSetStrategy>()
                .WithConstructorArgument("siteMapBuilderSets",
                    new[] {
                        Kernel.Get<ISiteMapBuilderSet>("siteMapBuilderSet1")
                    });
        }
    }
}