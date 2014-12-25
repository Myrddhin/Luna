using Luna.Data.Storage;

namespace Luna.Data.Local.Storage
{
    public interface IGlobalRepositoryManager : IRepositoryManager
    {
        IApplicationDataContext GetApplicationDataContext();
    }
}