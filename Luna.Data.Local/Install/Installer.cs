using Loki.IoC;
using Loki.IoC.Registration;
using Luna.Data.Local.Storage;

namespace Luna.Data.Local.SQLite
{
    public class Installer : LokiContextInstaller
    {
        public override void Install(IObjectContext context)
        {
            context.Register(Element.For<IGlobalRepositoryManager>().ImplementedBy<GlobalRepositoryManager>());
            context.Register(Element.For<ILocalRepositoryManager>().ImplementedBy<LocalRepositoryManager>());
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