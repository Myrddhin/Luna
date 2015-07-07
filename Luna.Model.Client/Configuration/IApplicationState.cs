using Luna.Model.Storage;

namespace Luna.Services.Configuration
{
    public interface IApplicationState
    {
        RepositoryBase ActiveRepository { get; }

        string UserName { get; }

        bool NetworkAvailable { get; }
    }
}