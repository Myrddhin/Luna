using System.ComponentModel;
using System.Linq;
using Luna.Model.Storage;

namespace Luna.Data.Storage
{
    public interface IApplicationDataProvider : INotifyPropertyChanged
    {
        IQueryable<Repository> Repositories { get; }

        IRepositoryManager ApplicationStore { get; }

        Repository Current { get; set; }

        IRepositoryManager ClientStore { get; }

        void SaveRepository(Repository repository);
    }
}