using System;
using System.Threading.Tasks;
using Luna.Model.IO;
using Luna.Services.Common;
using SpreadsheetLight;

namespace Luna.Services.IO
{
    internal class StandardExcelDataConverterService : BaseService, IDataConverterService
    {
        private SLDocument workbook;

        private string fileName;

        public async Task StartGeneration(string filePath)
        {
            await Task.Run(() =>
         {
             fileName = filePath;
             workbook = new SLDocument();
         });
        }

        public void CreatePart(string partName)
        {
            workbook.AddWorksheet(partName);
        }

        public async Task EndGeneration()
        {
            await Task.Run(() =>
            {
                workbook.SaveAs(fileName);
                workbook.Dispose();
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (workbook != null)
            {
                workbook.Dispose();
            }
        }

        public void SetValue(int row, int col, string value)
        {
            workbook.SetCellValue(row, col, value);
        }

        public void SetValue(int row, int col, DateTime value)
        {
            workbook.SetCellValue(row, col, value);
        }

        public void SetValue(int row, int col, decimal value)
        {
            workbook.SetCellValue(row, col, value);
        }

        public void SetValue(int row, int col, int value)
        {
            workbook.SetCellValue(row, col, value);
        }

        public void SetValue(int row, int col, bool value)
        {
            workbook.SetCellValue(row, col, value);
        }
    }
}