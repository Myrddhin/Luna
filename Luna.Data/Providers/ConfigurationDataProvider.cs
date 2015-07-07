using System.Linq;
using System.Threading.Tasks;
using Luna.Data.Local.Configuration;
using Luna.Model.Storage;
using Luna.Services.Configuration;

namespace Luna.Data.Storage
{
    internal class ConfigurationDataProvider : BaseProvider, IConfigurationDataProvider
    {
        public IConfigurationContext DataContext { get; set; }

        public IRepositoryStore CloudContext { get; set; }

        public async Task SaveAsync(Repository repository)
        {
            await Save<Repository>(DataContext, DataContext.Repositories, repository);
        }

        public async Task SaveChangesAsync()
        {
            await DataContext.SaveChangesAsync();
        }

        public async Task EnsureCloudRefresh()
        {
            if (State.ActiveRepository.Online)
            {
                var cloud = await CloudContext.GetAsync(State.ActiveRepository.Id);
                await SaveAsync(cloud);
            }
            else
            {
                await Task.FromResult(false);
            }
        }

        public IQueryable<Repository> Repositories
        {
            get
            {
                return DataContext.Repositories;
            }
        }
    }
}