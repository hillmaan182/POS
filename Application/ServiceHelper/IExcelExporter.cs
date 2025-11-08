
namespace Application.ServiceHelper
{
    public interface IExcelExporter
    {
        byte[] ExportToExcel<T>(List<T> data,  string sheetName);
    }
}
