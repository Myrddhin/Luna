using Loki.IoC;
using Loki.IoC.Registration;
using Luna.Cloud.Controllers;

namespace Luna.Cloud.Install
{
    public class ControllerInstaller : LokiContextInstaller
    {
        public override void Install(IObjectContext context)
        {
            base.Install(context);

            // Subscription
            context.Register(Element.For<RepositoriesController>().Lifestyle.Transient);

            // CRM
            context.Register(Element.For<TagsController>().Lifestyle.Transient);
        }
    }
}