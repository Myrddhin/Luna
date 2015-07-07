using System.Threading.Tasks;

namespace Luna.Model
{
    public interface IModule
    {
        Task SaveChangesAsync();
    }
}