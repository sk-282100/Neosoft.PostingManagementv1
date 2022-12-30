using Nancy.Json;
using PostingManagement.UI.Models;
using PostingManagement.UI.Models.ExcelFileTypes;
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
        /// <param name="request"> UploadedRecordsRequest model which has batchId, fileTypeCode and pageNumber and numberOfrecords for server side pagination</param>        
        /// <returns> Records and total numbers of the Upload History in JSON string Format</returns>                
        public Task<string> GetUploadedRecordsByBatchId(UploadedRecordsRequest request);
        public Task<ViewRecordsResponse<EmployeeMaster>> EmployeeMasterRecords(int id, Pagination pagination);
        Task<ViewRecordsResponse<BranchMaster>> BranchMasterRecords(int id, Pagination pagination);
        Task<ViewRecordsResponse<DepartmentMaster>> DepartmentMasterRecords(int id, Pagination pagination);
        Task<ViewRecordsResponse<InterRegionalPromotion>> InterRegionPromotionRecords(int id, Pagination pagination);
        Task<ViewRecordsResponse<InterRegionRequestTransfer>> InterRegionRequestTransferRecords(int id, Pagination pagination);
        Task<ViewRecordsResponse<InterZonalPromotion>> InterZonalPromotionRecords(int id, Pagination pagination);
        Task<ViewRecordsResponse<InterZonalRequestTransfer>> InterZonalRequestTranferRecords(int id, Pagination pagination);
        Task<ViewRecordsResponse<RegionMaster>> RegionMasterRecords(int id, Pagination pagination);
        Task<ViewRecordsResponse<ZoneMaster>> ZoneMasterRecords(int id, Pagination pagination);
        
        /// <summary>
        /// Get the status of the employee transfer workflow of current month
        /// </summary>
        /// <returns>response model contain's the staus of workflow </returns>
        public Task<Response<GetWorkFlowStatus>> GetWorkFlowStaus();
        
        /// <summary>
        /// delete the Uploaded excel for reset the workflow 
        /// </summary>
        /// <returns>response model containing the deletion status </returns>
        public Task<Response<bool>> ResetWorkflow();
    }
}
