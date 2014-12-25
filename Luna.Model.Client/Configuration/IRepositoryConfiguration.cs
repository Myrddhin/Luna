using Luna.Model.Storage;

namespace Luna.Services.Configuration
{
    public interface IRepositoryConfiguration
    {
        RepositoryStatus? Status { get; }

        void Create();

        void Upgrade();

        bool IsReady();

        Repository Current { get; set; }
    }
}