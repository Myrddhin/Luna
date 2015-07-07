using System.Collections.Generic;
using System.Threading.Tasks;
using Luna.Data.CRM;
using Luna.Model.IO;
using Luna.Services.Common;

namespace Luna.Services.IO
{
    internal class DataConverterModule : BaseService, IDataConverterModule
    {
        public IDataConverterFactory ConverterFactory { get; set; }

        public ICRMDataProvider CRMData { get; set; }

        public override void BeginInit()
        {
            base.BeginInit();
        }

        public async Task<IEnumerable<DataConverterLog>> GenerateFile(string fileName, IOMode mode)
        {
            IDataConverterService converter = null;
            var log = new List<DataConverterLog>();
            await Task.Delay(1000);
            switch (mode)
            {
                case IOMode.StandardExcel:
                    converter = ConverterFactory.GetStandardExcel();
                    break;

                default:
                    break;
            }

            if (converter == null)
            {
                return log;
            }

            try
            {
                await converter.StartGeneration(fileName);
                log.AddRange(await GenerateCRMPart(converter));
                await converter.EndGeneration();

                return log;
            }
            finally
            {
                ConverterFactory.Release(converter);
            }
        }

        private async Task<IEnumerable<DataConverterLog>> GenerateCRMPart(IDataConverterService converter)
        {
            return await Task.Run(() =>
             {
                 var log = new List<DataConverterLog>();
                 converter.CreatePart("CRM");

                 // Write header
                 converter.SetValue(1, 1, "Clé");
                 converter.SetValue(1, 2, "Nom");

                 return log;
             });
        }

        public async Task<IEnumerable<DataConverterLog>> ImportFile(string fileName, IOMode mode)
        {
            throw new System.NotImplementedException();
        }

        public async Task SaveChangesAsync()
        {
            AcceptChanges();
            await CRMData.SaveChangesAsync();
        }
    }
}