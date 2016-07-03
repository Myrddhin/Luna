using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Luna.Model
{
    public interface ICloudStore<TEntity> where TEntity : ClientEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync(Guid repositoryId);

        Task<TEntity> GetAsync(Guid repositoryId, Guid id);

        Task<DateTime> LastModified(Guid repositoryId);

        Task<IEnumerable<TEntity>> GetAllAsync(Guid repositoryId, DateTime refreshDate);

        Task SaveAsync(Guid repositoryId, TEntity item);
    }
}