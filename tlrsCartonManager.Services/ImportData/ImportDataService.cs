using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.Core.Enums;
using tlrsCartonManager.Services.ImportData.Import;

namespace tlrsCartonManager.Services.ImportData
{
    public  class ImportDataService
    {
        public  List<T> GetImportDetails<T>(IFormFile file, ImportType importOption) where T : class
        {
            if (file.Length <= 0)
            {
                return new List<T>();
            }

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using var ms = new MemoryStream();
            file.CopyTo(ms);

            var reader = ExcelReaderFactory.CreateReader(ms, new ExcelReaderConfiguration()
            {
                FallbackEncoding = Encoding.UTF8
            });

            var workbook = reader.AsDataSet();

            if (workbook.Tables.Count <= 0) return null;

            var table = reader.AsDataSet().Tables[0];
            var rows = (from DataRow row in table.Rows select row).ToList();

            switch (importOption)
            {
                case ImportType.AlternativeNoUpdate:
                    var importDetails = rows.Skip(1).Take(rows.Count() - 1).Select(currentRow =>
                    {
                        int.TryParse(currentRow[0].ToString() ?? "0", out var cartonNo);
                        return new ExcelParseAlternativeNoUpdateViewModel()
                        {
                            AlternativeNo = currentRow[1].ToString(),
                            CartonNo = cartonNo
                        };
                    });
                    return importDetails.Where(importDetails => importDetails.CartonNo != 0).ToList() as List<T>;

                default:
                    throw new Exception("Import not implemented");
            }

        }

    }
}
