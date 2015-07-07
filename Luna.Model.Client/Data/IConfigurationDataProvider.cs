using System.Linq;
using System.Threading.Tasks;
using Luna.Model.Storage;

namespace Luna.Data.Storage
{
    public interface IConfigurationDataProvider
    {
        /// <summary>
        /// Get available repositories (from application cache).
        /// </summary>
        IQueryable<Repository> Repositories { get; }

        /// <summary>
        /// Saves the repository updates.
        /// </summary>
        /// <param name="repository">Repository</param>
        Task SaveAsync(Repository repository);

        /// <summary>
        /// Persists context modifications.
        /// </summary>
        /// <returns>Task.</returns>
        Task SaveChangesAsync();
    }
}