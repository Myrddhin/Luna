using System.Collections.Generic;
using System.Threading.Tasks;
using Luna.Model.Storage;

namespace Luna.Services.Configuration
{
    public interface IRepositoryManagerService : IApplicationState
    {
        IEnumerable<RepositorySettings> LocalRepositories { get; }

        Task<IEnumerable<Repository>> GetCloudRepositoriesAsync();

        RepositoryStatus? Status { get; }

        Task CreateAsync();

        Task UpgradeAsync();

        bool IsReady();

        Task ConnectAsync(RepositoryBase repository);

        Task SaveChangesAsync();
    }
}