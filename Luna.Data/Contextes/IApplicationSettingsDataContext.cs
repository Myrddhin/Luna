using System.Data.Entity;
using Luna.Model.Storage;

namespace Luna.Data.Local.Storage
{
    public interface IApplicationSettingsDataContext : IDataContext
    {
        DbSet<RepositorySettings> RepositorySettings { get; }
    }
}