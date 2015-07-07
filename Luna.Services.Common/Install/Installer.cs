using Loki.IoC;
using Loki.IoC.Registration;
using Luna.Model.IO;
using Luna.Services.IO;

namespace Luna.Services.Configuration
{
    public class Installer : LokiContextInstaller
    {
        public override void Install(IObjectContext context)
        {
            context.Register(Element.For<IApplicationInstallService>().ImplementedBy<ApplicationInstallService>());

            context.Register(Element.For<IRepositoryManagerService, IApplicationState>().ImplementedBy<ApplicationConfigurationService>());
            context.Register(Element.For<IRepositoryConfigurationService>().ImplementedBy<RepositoryConfigurationService>().Lifestyle.Transient);

            context.Register(Element.For<IDataConverterModule>().ImplementedBy<DataConverterModule>().Lifestyle.Transient);
            context.Register(Element.For<IDataConverterFactory>().AsFactory());
            context.Register(Element.For<IDataConverterService>().ImplementedBy<StandardExcelDataConverterService>().Named("StandardExcel").Lifestyle.Transient);
        }

        private static Installer all = new Installer();

        public static Installer All
        {
            get { return all; }
        }
    }
}