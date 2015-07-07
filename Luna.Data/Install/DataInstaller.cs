using Loki.IoC;
using Loki.IoC.Registration;
using Luna.Data.Storage;

namespace Luna.Data
{
    public class Installer : LokiContextInstaller
    {
        public override void Install(IObjectContext context)
        {
            context.Register(Element.For<IApplicationDataProvider>().ImplementedBy<ApplicationDataProvider>().Lifestyle.Transient);
            context.Register(Element.For<IConfigurationDataProvider>().ImplementedBy<ConfigurationDataProvider>().Lifestyle.Transient);
            // context.Register(Element.For<ICRMDataProvider>().ImplementedBy<CRMDataProvider>().Lifestyle.Transient);
        }

        private static Installer all = new Installer();

        public static Installer All
        {
            get
            {
                return all;
            }
        }
    }
}