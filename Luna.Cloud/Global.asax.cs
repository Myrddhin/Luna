using System.Web.Http;
using Loki.Common;
using Loki.IoC;
using Luna.Cloud.Install;
using Luna.Cloud.IoC;

namespace Luna.Cloud
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private IObjectContext context;

        protected void Application_Start()
        {
            Toolkit.Initialize();
            context = Toolkit.IoC.DefaultContext;
            DefaultInstaller.AllServices.Install(context);
            GlobalInstaller.All.Install(context);

            GlobalConfiguration.Configure(config => WebApiConfig.Register(config, context));
        }

        public override void Dispose()
        {
            Toolkit.Shutdown();
            base.Dispose();
        }
    }
}