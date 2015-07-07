using System.Data.Entity;

namespace Luna.Cloud.Data
{
    public partial class LunaDataContext : DbContext
    {
        public LunaDataContext()
            : base("name=MainDatabase")
        {
        }
    }
}