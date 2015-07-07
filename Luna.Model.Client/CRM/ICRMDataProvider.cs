using System.Linq;
using System.Threading.Tasks;
using Luna.Model.CRM;

namespace Luna.Data.CRM
{
    public interface ICRMDataProvider
    {
        IQueryable<Contact> Contacts { get; }

        IQueryable<Tag> Tags { get; }

        Task SaveAsync(Contact contact);

        Task SaveAsync(Tag tag);

        Task RemoveAsync(Contact contact);

        Task RemoveAsync(Tag tag);

        Task SaveChangesAsync();
    }
}