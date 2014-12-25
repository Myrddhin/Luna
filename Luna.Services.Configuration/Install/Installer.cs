using Loki.IoC;
using Loki.IoC.Registration;

namespace Luna.Services.Configuration
{
    public class Installer : LokiContextInstaller
    {
        public override void Install(IObjectContext context)
        {
            context.Register(Element.For<IApplicationConfiguration>().ImplementedBy<ApplicationConfigurationService>());
            context.Register(Element.For<IRepositoryConfiguration>().ImplementedBy<ClientConfigurationService>());
        }

        private static Installer all = new Installer();

        public static Installer All
        {
            get { return all; }
        }
    }
}