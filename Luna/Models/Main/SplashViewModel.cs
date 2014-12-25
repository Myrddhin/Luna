using System.Threading.Tasks;
using Loki.UI;
using Luna.Services.Configuration;

namespace Luna.UI.Main
{
    public class SplashViewModel : Screen, ISplashViewModel
    {
        public IApplicationConfiguration Application { get; set; }

        public SplashViewModel()
        {
            DisplayName = "Luna";
        }

        public async Task ApplicationInitialize()
        {
            await Task.Factory.StartNew(() =>
            {
                switch (Application.ApplicationStatus)
                {
                    case Luna.Model.Storage.RepositoryStatus.NeedUpgrade:
                        Application.Upgrade();
                        break;

                    case Luna.Model.Storage.RepositoryStatus.New:
                        Application.Install();
                        break;

                    case Luna.Model.Storage.RepositoryStatus.OK:
                    default:
                        break;
                }
            });
        }
    }
}