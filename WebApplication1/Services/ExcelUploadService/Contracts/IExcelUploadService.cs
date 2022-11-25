using PostingManagement.UI.Models;
using PostingManagement.UI.Models.Responses;

namespace PostingManagement.UI.Services.ExcelUploadService.Contracts
{
    public interface IExcelUploadService
    {
        public Task<ExcelUploadResponseModel> UploadExcel(ExcelUploadViewModel model, string uploadedBy);
        public  Task<Response<List<UploadHistoryDetails>>> GetUploadHistories(int fileTypeCode);
        public Task<string> GetUploadedRecordsByBatchId(int batchId, int fileTypeCode);
    }
}
