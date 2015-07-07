using Loki.IoC;

namespace Luna.Cloud.IoC
{
    public class DefaultInstaller : LokiContextInstaller
    {
        public override void Install(IObjectContext context)
        {
            base.Install(context);
        }

        public static IContextInstaller AllServices = new DefaultInstaller();
    }
}