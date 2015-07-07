using System.Data.Entity;
using System.Threading.Tasks;
using Loki.Common;
using Luna.Data.Local;
using Luna.Model;
using Luna.Services.Configuration;

namespace Luna.Data.Storage
{
    public abstract class BaseProvider : BaseObject
    {
        public IApplicationState State { get; set; }

        protected async Task Save<T>(IDataContext context, DbSet<T> store, T entity) where T : Entity
        {
            T old = await store.FindAsync(entity.Id);

            var state = old == null ? EntityState.Added : EntityState.Modified;

            context.SetState(entity, state);
        }

        protected async Task Remove<T>(IDataContext context, DbSet<T> store, T entity) where T : Entity
        {
            T old = await store.FindAsync(entity.Id);
            if (old != null)
            {
                context.SetState(entity, EntityState.Deleted);
            }
        }
    }
}