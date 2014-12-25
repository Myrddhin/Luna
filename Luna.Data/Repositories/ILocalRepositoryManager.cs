using Luna.Data.Local.Configuration;
using Luna.Data.Storage;

namespace Luna.Data.Local.Storage
{
    public interface ILocalRepositoryManager : IRepositoryManager
    {
        IConfigurationContext GetConfigurationContext();
    }
}