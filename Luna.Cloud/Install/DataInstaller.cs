using Loki.IoC;
using Loki.IoC.Registration;
using Luna.Cloud.Data.CRM;
using Luna.Cloud.Data.Meta;

namespace Luna.Cloud.Install
{
    public class DataInstaller : LokiContextInstaller
    {
        public override void Install(IObjectContext context)
        {
            base.Install(context);
            context.Register(Element.For<MetaDataContext>().Lifestyle.PerRequest);
            context.Register(Element.For<CRMContext>().Lifestyle.PerRequest);
        }
    }
}