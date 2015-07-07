using System;
using System.Threading.Tasks;

namespace Luna.Model.IO
{
    public interface IDataConverterService
    {
        Task StartGeneration(string filePath);

        void CreatePart(string partName);

        void SetValue(int row, int col, string value);

        void SetValue(int row, int col, DateTime value);

        void SetValue(int row, int col, decimal value);

        void SetValue(int row, int col, int value);

        void SetValue(int row, int col, bool value);

        Task EndGeneration();
    }
}