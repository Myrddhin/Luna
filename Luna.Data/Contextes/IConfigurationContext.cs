using System.Data.Entity;
using Luna.Model.Storage;

namespace Luna.Data.Local.Configuration
{
    public interface IConfigurationContext : IDataContext
    {
        DbSet<Repository> Repositories { get; }
    }
}