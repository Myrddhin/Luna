using System.Threading.Tasks;

namespace Luna.Services.Configuration
{
    public interface IRepositoryConfigurationService
    {
        string Name { get; set; }

        bool IsOnline { get; set; }

        Task SaveChangesAsync();
    }
}