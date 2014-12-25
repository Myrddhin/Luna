using System.Data.Entity;
using Luna.Model.Configuration;

namespace Luna.Data.Local.Configuration
{
    public interface IConfigurationContext
    {
        IDbSet<ProviderConfiguration> Providers { get; }

        void MarkAsModified<T>(T repository) where T : class;

        void SaveChanges();
    }
}