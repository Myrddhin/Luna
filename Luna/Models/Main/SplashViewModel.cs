using System.Threading.Tasks;
using Loki.UI;
using Luna.Services.Configuration;

namespace Luna.UI.Main
{
    public class SplashViewModel : Screen, ISplashViewModel
    {
        public IApplicationInstallService Installer { get; set; }

        public SplashViewModel()
        {
            DisplayName = "Luna";
        }

        public async Task ApplicationInitialize()
        {
            switch (Installer.ApplicationStatus)
            {
                case Luna.Model.Storage.RepositoryStatus.NeedUpgrade:
                    await Installer.UpgradeAsync();
                    break;

                case Luna.Model.Storage.RepositoryStatus.New:
                    await Installer.InstallAsync();
                    break;

                case Luna.Model.Storage.RepositoryStatus.OK:
                default:
                    await Task.Yield();
                    break;
            }
        }
    }
}