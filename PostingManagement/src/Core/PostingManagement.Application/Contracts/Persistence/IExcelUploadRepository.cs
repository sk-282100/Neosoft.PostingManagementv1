using PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData;
using PostingManagement.Domain;
using PostingManagement.Domain.Entities;

namespace PostingManagement.Application.Contracts.Persistence
{
    public interface IExcelUploadRepository
    {
        /// <summary>
        /// A Generic Method which converts the list of records into DataTable of the respective Excel Model and excute respective stored procedure to upload the data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uploadedBy">username of the logged in user </param>
        /// <param name="excelData">a list of Data of the respective excel file</param>
        /// <param name="fileName">name of the excel file</param>
        /// <returns> an object which includes the uploaded status,no of the records successfully inserted and batchId </returns>
        public Task<ExcelUploadResult> AddAsync<T>(string uploadedBy, List<T> excelData, string fileName);

        /// <summary>
        /// Gets the list of the history records on the basis of filetypeCode
        /// </summary>
        /// <param name="FileTypeCode">int</param>
        /// <returns>an list of UploadHistoryDetails Model</returns>
        public Task<List<UploadHistoryDetails>> GetUploadHistoryList(int FileTypeCode);

        /// <summary>
        /// A Generic Method which gets the uploaded records by batchId and respective excelfile 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileTypeCode">int</param>
        /// <param name="batchId">int</param>
        /// <returns>a  serialized list of the respective Excel Model</returns>
        public Task<string> GetAllRecords<T>(GetExcelDataQuery<T> request) where T : class;
    }
}
