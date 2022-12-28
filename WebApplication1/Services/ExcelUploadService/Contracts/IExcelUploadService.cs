using PostingManagement.UI.Models;
using PostingManagement.UI.Models.Responses;

namespace PostingManagement.UI.Services.ExcelUploadService.Contracts
{
    public interface IExcelUploadService
    {   
        /// <summary>
        /// Upload the Excel Data according to excel formate 
        /// </summary>
        /// <returns>Response model contains the success count and status</returns>
        public Task<ExcelUploadResponseModel> UploadExcel(ExcelUploadViewModel model, string uploadedBy);

        /// <summary>
        /// Get the Excel file upload History
        /// </summary>
        /// <returns>List of UploadHistoryDetails object contains the history of Excel Uploads</returns>
        public Task<Response<List<UploadHistoryDetails>>> GetUploadHistories(int fileTypeCode);

        /// <summary>
        /// Get the Excel Records by Upload Batch Id
        /// </summary>
        /// <param name="batchId">Unique Id for Upload</param>
        /// <param name="fileTypeCode">File type Id</param>
        /// <returns> Records of the Upload History in JSON string Format</returns>
        public Task<string> GetUploadedRecordsByBatchId(int batchId, int fileTypeCode);
        public Task<Response<GetWorkFlowStatus>> GetWorkFlowStaus();


    }
}
