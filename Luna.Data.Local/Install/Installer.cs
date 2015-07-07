using Loki.IoC;
using Loki.IoC.Registration;
using Luna.Data.Local.Configuration;
using Luna.Data.Local.Storage;

namespace Luna.Data.Local.SQLite
{
    public class Installer : LokiContextInstaller
    {
        public override void Install(IObjectContext context)
        {
            context.Register(Element.For<IApplicationDataContainer>().ImplementedBy<GlobalRepositoryManager>());
            context.Register(Element.For<IRepositoryDataContainer>().ImplementedBy<LocalRepositoryManager>());

            context.Register(Element.For<IApplicationSettingsDataContext>().ImplementedBy<UserSettingsDataContext>());
            context.Register(Element.For<IConfigurationContext>().ImplementedBy<ConfigurationContext>());
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