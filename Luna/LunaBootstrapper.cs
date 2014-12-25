using Luna.UI.Main;

namespace Luna.UI
{
    public class LunaBootstrapper : Loki.UI.Wpf.WpfSplashBootStrapper<MainViewModel, SplashViewModel>
    {
        public LunaBootstrapper()
            : base(new SplashView())
        {
            BootStrapper.Install(
                Luna.Data.Local.SQLite.Installer.All,
                Luna.Data.Installer.All,
                Luna.Services.Configuration.Installer.All,
                Luna.UI.Installer.All);
        }
    }
}