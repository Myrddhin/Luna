using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace Luna.Data.Local
{
    public interface IDataContext
    {
        DbChangeTracker ChangeTracker { get; }

        Task SaveChangesAsync();

        void SetState(object entity, EntityState state);
    }
}