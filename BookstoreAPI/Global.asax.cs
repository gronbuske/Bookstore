using BookStoreClasses;
using BookStoreImplementation;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using Unity.WebApi;

namespace BookstoreAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Using Unity to bind BookstoreImplementation to the BookstoreInterface
            var container = new UnityContainer();
            container.RegisterType<IBookstoreService, BookstoreService>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

            //Initialize BookstoreConnection Singleton with correct BookstoreService implementation
            BookstoreConnection.Instance.SetService(container.Resolve<IBookstoreService>());
            
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
