using System.Web.Http;
using System.Web.Http.Dispatcher;
using Loki.IoC;
using Luna.Cloud.IoC;

namespace Luna.Cloud
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config, IObjectContext context)
        {
            // Web API configuration and services
            // config.Filters.Add(new AuthorizeAttribute());
            config.EnableSystemDiagnosticsTracing();

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Api root for global controller.
            config.Routes.MapHttpRoute(
                name: "RepositoryRoot",
                routeTemplate: "api/repositories/{id}",
                defaults: new { id = RouteParameter.Optional, controller = "repositories" });

            // Api root for repository dependant controllers.
            config.Routes.MapHttpRoute(
                name: "EntityRoute",
                routeTemplate: "api/repository/{repositoryId}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "LastModifiedRoute",
                routeTemplate: "api/repository/{repositoryId}/{controller}/lastmodified",
                defaults: new { action = "lastmodified" });

            // IoC Activator
            config.Services.Replace(typeof(IHttpControllerActivator), new IoCControllerActivator(context));
        }
    }
}