using ClosedXML.Excel;
using Application.ServiceHelper;

namespace Infrastructure.Helper
{
    public class ExcelExporter : IExcelExporter
    {
        public byte[] ExportToExcel<T>(List<T> data, string sheetName)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(sheetName);

                // Add headers (assuming properties of T)
                var properties = typeof(T).GetProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    worksheet.Cell(1, i + 1).Value = properties[i].Name;
                }

                // Add data
                for (int row = 0; row < data.Count; row++)
                {
                    for (int col = 0; col < properties.Length; col++)
                    {
                        worksheet.Cell(row + 2, col + 1).Value = (XLCellValue)properties[col].GetValue(data[row]);
                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}
