using PostingManagement.UI.Models;

namespace PostingManagement.UI.Services.ExcelUploadService.Contracts
{
    public interface IExcelUploadService
    {
        public Task<ExcelUploadResponseModel> UploadExcel(ExcelUploadViewModel model, string uploadedBy);
    }
}
