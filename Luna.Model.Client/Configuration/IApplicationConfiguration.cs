using System.Collections.Generic;
using Luna.Model.Storage;

namespace Luna.Services.Configuration
{
    public interface IApplicationConfiguration
    {
        RepositoryStatus ApplicationStatus { get; }

        void Install();

        void Upgrade();

        IEnumerable<Repository> Repositories { get; }
    }
}