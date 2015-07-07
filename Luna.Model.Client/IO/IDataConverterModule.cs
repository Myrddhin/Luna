using System.Collections.Generic;
using System.Threading.Tasks;

namespace Luna.Model.IO
{
    public interface IDataConverterModule : IModule
    {
        Task<IEnumerable<DataConverterLog>> GenerateFile(string fileName, IOMode mode);

        Task<IEnumerable<DataConverterLog>> ImportFile(string fileName, IOMode mode);
    }
}