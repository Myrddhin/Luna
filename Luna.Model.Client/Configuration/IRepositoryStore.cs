using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Luna.Model.Storage;

namespace Luna.Services.Configuration
{
    public interface IRepositoryStore
    {
        Task<IEnumerable<Repository>> GetAllAsync();

        Task<Repository> GetAsync(Guid id);

        Task<IEnumerable<Repository>> GetAllAsync(DateTime refreshDate);

        Task SaveAsync(Repository item);
    }
}