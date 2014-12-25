using System.Data.Entity;
using Luna.Model.Storage;

namespace Luna.Data.Local.Storage
{
    public interface IApplicationDataContext
    {
        IDbSet<Repository> Repositories { get; }

        void MarkAsModified<T>(T repository) where T : class;

        void SaveChanges();
    }
}