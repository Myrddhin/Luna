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

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Api root for global controller.
            config.Routes.MapHttpRoute(
                name: "RepositoryRoot",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }

            );

            // Api root for repository dependant controllers.
            config.Routes.MapHttpRoute(
                name: "CrmRoute",
                routeTemplate: "api/repository/{repositoryId}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            // IoC Activator
            config.Services.Replace(typeof(IHttpControllerActivator), new IoCControllerActivator(context));
        }
    }
}