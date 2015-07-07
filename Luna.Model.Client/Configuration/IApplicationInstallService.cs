using System.Threading.Tasks;
using Luna.Model.Storage;

namespace Luna.Services.Configuration
{
    public interface IApplicationInstallService
    {
        RepositoryStatus ApplicationStatus { get; }

        Task InstallAsync();

        Task UpgradeAsync();
    }
}