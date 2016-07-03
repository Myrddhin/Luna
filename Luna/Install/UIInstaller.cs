using Loki.IoC;
using Loki.IoC.Registration;
using Luna.UI.CRM;
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
            context.Register(Element.For<DocumentsViewModel>().Lifestyle.Transient);
            context.Register(Element.For<MenuViewModel>().Lifestyle.Transient);

            context.Register(Element.For<NavigationMenuViewModel>().Lifestyle.Transient);
            context.Register(Element.For<ConfigurationViewModel>().Lifestyle.Transient);
            context.Register(Element.For<ImportViewModel>().Lifestyle.Transient);

            context.Register(Element.For<DirectoryViewModel>().Lifestyle.Transient);
            context.Register(Element.For<ContactEditViewModel>().Lifestyle.Transient);
            context.Register(Element.For<TagListViewModel>().Lifestyle.Transient);
            context.Register(Element.For<TagEditViewModel>().Lifestyle.Transient);

            context.Register(Element.For<IScreenFactory>().AsFactory());
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