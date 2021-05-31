using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using tlrsCartonManager.DAL.Models.GenericReport;
using tlrsCartonManager.DAL.Models.Report;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using tlrsCartonManager.Services.Report.Core;

namespace tlrsCartonManager.Services.Report
{
    public class ReportGeneratingService
    {
        private readonly IGenericReportManagerRepository _genericReportManagerRepository;

        public ReportGeneratingService(IGenericReportManagerRepository genericReportManagerRepository)
        {
            _genericReportManagerRepository = genericReportManagerRepository;
        }

        public byte[] GenerateReportData(GenerateReportRequest request)
        {

            IWorkbook workbook = new XSSFWorkbook();

            ISheet jobSheet = workbook.CreateSheet(request.ReportName);

            var rowIndex = 0;
            IRow headerRow = jobSheet.CreateRow(rowIndex);

            ICellStyle headerStyle = workbook.CreateCellStyle();
            headerStyle.FillBackgroundColor = IndexedColors.White.Index;
            headerStyle.FillForegroundColor = IndexedColors.Black.Index;
            headerStyle.FillPattern = FillPattern.SolidForeground;
            var headerFont = workbook.CreateFont();
            headerFont.IsBold = true;
            headerFont.Color = IndexedColors.White.Index;
            headerStyle.SetFont(headerFont);


            var reportData = _genericReportManagerRepository.GetReportData(new GenericReportData()
            {
                ReportName = request.ReportName,
                ReportFilters = request.ReportFilters.Select(f => new GenericReportFilterOption()
                {
                    ColumnId = f.ColumnId,
                    ColumnName = f.ColumnName,
                    FilterOperator = f.FilterOperator,
                    FromValue = f.FromValue,
                    IsSelect = f.IsSelect,
                    SortBy = f.SortBy,
                    SortOrder = f.SortOrder,
                    ToValue = f.ToValue
                }).ToList()
            });

            if (reportData == null)
            {
                throw new Exception("Unable to generate the report data");
            }

            if (!reportData.Any())
            {
                throw new Exception("No data for generate report");
            }

            var headerPointer = 0;
            var rowPointer = 1;

            //Set headers
            foreach (var header in reportData[0].Select(dataItem => dataItem.Key))
            {
                headerRow.CreateCell(headerPointer).SetCellValue(header);
                ++headerPointer;
            }

            foreach (var dataItem in reportData)
            {
                rowIndex++;

                IRow row = jobSheet.CreateRow(rowIndex);

                //Set values
                headerPointer = 0;

                foreach (var data in dataItem)
                {
                    row.CreateCell(headerPointer).SetCellValue(data.Value);
                    headerPointer++;
                }
            }

            for (int i = 0; i < reportData[0].Select(dataItem => dataItem.Key).Count(); i++)
            {
                jobSheet.AutoSizeColumn(i);
                jobSheet.GetRow(0).GetCell(i).CellStyle = headerStyle;
            }

            var memoryStream = new MemoryStream();

            workbook.Write(memoryStream);

            GC.SuppressFinalize(workbook);

            return memoryStream.ToArray();

        }
    }
}
