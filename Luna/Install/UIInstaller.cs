using Loki.IoC;
using Loki.IoC.Registration;
using Luna.UI.Main;

namespace Luna.UI
{
    public class Installer : LokiContextInstaller
    {
        public override void Install(IObjectContext context)
        {
            context.Register(Element.For<SplashViewModel>().Lifestyle.Transient);
            context.Register(Element.For<MainViewModel>().Lifestyle.Transient);
            context.Register(Element.For<OpenViewModel>().Lifestyle.Transient);
        }

        private static IContextInstaller ui = new Installer();

        public static IContextInstaller All
        {
            get
            {
                return ui;
            }
        }
    }
}