using System.Threading.Tasks;
using Luna.Data.Local.Storage;
using Luna.Model.Storage;
using Luna.Services.Common;

namespace Luna.Services.Configuration
{
    internal class ApplicationInstallService : BaseService, IApplicationInstallService
    {
        public IApplicationDataContainer ApplicationDataContainer { get; set; }

        private ContainerInstallService install;

        public ApplicationInstallService()
        {
            install = new ContainerInstallService();
        }

        public async Task InstallAsync()
        {
            await Task.WhenAll(install.InstallAsync(ApplicationFileName), Task.Delay(2000));
        }

        public override void BeginInit()
        {
            base.BeginInit();
            install.DataContainer = ApplicationDataContainer;
        }

        public async Task UpgradeAsync()
        {
            await Task.WhenAll(install.UpgradeAsync(ApplicationFileName), Task.Delay(2000));
        }

        public RepositoryStatus ApplicationStatus
        {
            get { return install.GetStatus(ApplicationFileName); }
        }

        private const string ApplicationFileName = "applicationdata.db3";
    }
}