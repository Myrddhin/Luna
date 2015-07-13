using Loki.IoC;
using Loki.IoC.Registration;

using Luna.Data.Cloud.CRM;
using Luna.Data.Cloud.Meta;
using Luna.Model;
using Luna.Model.CRM;
using Luna.Services.Configuration;

namespace Luna.Data.Cloud.Azure
{
    public class Installer : LokiContextInstaller
    {
        public override void Install(IObjectContext context)
        {
            context.Register(Element.For<IRepositoryStore>().ImplementedBy<RepositoryStore>());
            context.Register(Element.For<ICloudStore<Tag>>().ImplementedBy<TagStore>());
            context.Register(Element.For<AzureRequester>());
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