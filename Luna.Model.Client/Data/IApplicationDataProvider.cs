using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Luna.Model.Storage;

namespace Luna.Data.Storage
{
    /// <summary>
    /// Application data provider.
    /// </summary>
    public interface IApplicationDataProvider
    {
        /// <summary>
        /// Get available repositories (from application cache).
        /// </summary>
        IQueryable<RepositorySettings> LocalRepositories { get; }

        /// <summary>
        /// Gets available cloud repositories.
        /// </summary>
        Task<IEnumerable<Repository>> GetCloudRepositoriesAsync();

        /// <summary>
        /// Saves the repository updates.
        /// </summary>
        /// <param name="repository">Repository</param>
        Task SaveAsync(RepositorySettings repository);

        /// <summary>
        /// Persists context modifications.
        /// </summary>
        /// <returns>Task.</returns>
        Task SaveChangesAsync();
    }
}