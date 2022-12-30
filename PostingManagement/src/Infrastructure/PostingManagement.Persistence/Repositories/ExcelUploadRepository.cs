using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData;
using PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData.BranchMasterRecords;
using PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData.DepartmentMasterRecords;
using PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData.EmployeeMasterRecords;
using PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData.InterRegionalPromotionRecords;
using PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData.InterRegionalRequestRecords;
using PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData.InterZonalPromotionRecords;
using PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData.InterZonalRequestRecords;
using PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData.RegionMasterRecords;
using PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData.ZoneMasterRecords;
using PostingManagement.Application.Helper.Constants;
using PostingManagement.Application.Responses;
using PostingManagement.Domain;
using PostingManagement.Domain.Entities;
using System.Data;
using System.Reflection;

namespace PostingManagement.Persistence.Repositories
{
    public class ExcelUploadRepository : IExcelUploadRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ExcelUploadRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ExcelUploadResult> AddAsync<T>(string uploadedBy, List<T> excelData, string fileName)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }

            //Converting List of object to data table
            foreach (T item in excelData)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    if (Props[i].PropertyType == typeof(DateTime) || Props[i].PropertyType == typeof(DateTime?))
                    {
                        DateTime date = (DateTime)Props[i].GetValue(item, null);
                        values[i] = date.ToShortDateString();
                    }
                    else
                    {
                        //inserting property values to datatable rows
                        values[i] = Props[i].GetValue(item, null);
                    }
                }
                dataTable.Rows.Add(values);
            }

            //save the data to the DataBase By Calling StoreProcedure according to their dataTypes
            SqlParameter dataTableParameter = new SqlParameter();
            SqlParameter uploadedByParameter = new SqlParameter() { ParameterName = "@uploadedBy", SqlDbType = SqlDbType.VarChar, Size = 30, Value = uploadedBy };
            SqlParameter fileNameParameter = new SqlParameter() { ParameterName = "@fileName", SqlDbType = SqlDbType.VarChar, Size = 200, Value = fileName };
            SqlParameter successCount = new SqlParameter() { ParameterName = "@successCount", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            SqlParameter uploadStatus = new SqlParameter() { ParameterName = "@status", SqlDbType = SqlDbType.VarChar, Size = 50, Direction = ParameterDirection.Output };
            SqlParameter batchId = new SqlParameter() { ParameterName = "@batchId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            var result = 0;

            //Calling the Store Procedure 
            switch (typeof(T).Name)
            {
                case nameof(BranchMaster):
                    dataTableParameter = new SqlParameter() { ParameterName = "@branchMasterData", SqlDbType = SqlDbType.Structured, Value = dataTable, TypeName = "BranchMasterTableType" };
                    result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC STP_BranchMaster_BulkUpload @branchMasterData,@uploadedBy,@fileName,@batchId OUTPUT,@successCount OUTPUT, @status OUTPUT"
                   , dataTableParameter, uploadedByParameter, fileNameParameter, batchId, successCount, uploadStatus);
                    break;

                case nameof(EmployeeMaster):
                    dataTableParameter = new SqlParameter() { ParameterName = "@employeeMasterData", SqlDbType = SqlDbType.Structured, Value = dataTable, TypeName = "EmployeeMasterTableType" };
                    result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC STP_EmployeeMasterData_InsertingData @employeeMasterData,@uploadedBy,@fileName,@batchId OUTPUT, @successCount OUTPUT, @status OUTPUT"
                   , dataTableParameter, uploadedByParameter, fileNameParameter, batchId, successCount, uploadStatus);
                    break;

                case nameof(InterRegionalPromotion):
                    dataTableParameter = new SqlParameter() { ParameterName = "@interRegionPromotionData", SqlDbType = SqlDbType.Structured, Value = dataTable, TypeName = "InterRegionalPromotionTableType" };
                    result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC STP_InterRegionalPromotion_BulkUpload @interRegionPromotionData,@uploadedBy,@fileName, @batchId OUTPUT,@successCount OUTPUT, @status OUTPUT"
                   , dataTableParameter, uploadedByParameter, fileNameParameter, batchId, successCount, uploadStatus);
                    break;

                case nameof(InterRegionRequestTransfer):
                    dataTableParameter = new SqlParameter() { ParameterName = "@interRegionRequestTransferData", SqlDbType = SqlDbType.Structured, Value = dataTable, TypeName = "InterRegionRequestTransferTableType" };
                    result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC STP_InterRegionRequestTransfer_BulkUpload @interRegionRequestTransferData,@uploadedBy,@fileName,@batchId OUTPUT,@successCount OUTPUT, @status OUTPUT"
                   , dataTableParameter, uploadedByParameter, fileNameParameter, batchId, successCount, uploadStatus);
                    break;

                case nameof(InterZonalPromotion):
                    dataTableParameter = new SqlParameter() { ParameterName = "@interZonalPromotionData", SqlDbType = SqlDbType.Structured, Value = dataTable, TypeName = "InterZonalPromotionTableType" };
                    result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC STP_InterZonalPromotion_BulkUpload @interZonalPromotionData,@uploadedBy,@fileName, @batchId OUTPUT,@successCount OUTPUT, @status OUTPUT"
                   , dataTableParameter, uploadedByParameter, fileNameParameter, batchId, successCount, uploadStatus);
                    break;

                case nameof(InterZonalRequestTransfer):
                    dataTableParameter = new SqlParameter() { ParameterName = "@interZonalRequestTransferTableType", SqlDbType = SqlDbType.Structured, Value = dataTable, TypeName = "InterZonalRequestTransferTableType" };
                    result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC STP_InterZonalRequestTransfer_BulkUpload @interZonalRequestTransferTableType,@uploadedBy,@fileName, @batchId OUTPUT,@successCount OUTPUT, @status OUTPUT"
                   , dataTableParameter, uploadedByParameter, fileNameParameter, batchId, successCount, uploadStatus);
                    break;

                case nameof(RegionMaster):
                    dataTableParameter = new SqlParameter() { ParameterName = "@regionMasterData", SqlDbType = SqlDbType.Structured, Value = dataTable, TypeName = "RegionMasterDataType" };
                    result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC STP_RegionMaster_BulkUpload @regionMasterData,@uploadedBy,@fileName, @batchId OUTPUT,@successCount OUTPUT, @status OUTPUT"
                   , dataTableParameter, uploadedByParameter, fileNameParameter, batchId, successCount, uploadStatus);
                    break;

                case nameof(ZoneMaster):
                    dataTableParameter = new SqlParameter() { ParameterName = "@zoneMasterData", SqlDbType = SqlDbType.Structured, Value = dataTable, TypeName = "ZoneMasterDataType" };
                    result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC STP_ZoneMaster_BulkUpload @zoneMasterData,@uploadedBy,@fileName, @batchId OUTPUT,@successCount OUTPUT, @status OUTPUT"
                   , dataTableParameter, uploadedByParameter, fileNameParameter, batchId, successCount, uploadStatus);
                    break;

                case nameof(DepartmentMaster):
                    dataTableParameter = new SqlParameter() { ParameterName = "@departmentMasterData", SqlDbType = SqlDbType.Structured, Value = dataTable, TypeName = "DepartmentMasterDataType" };
                    result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC STP_DepartmentMaster_BulkUpload @departmentMasterData,@uploadedBy,@fileName, @batchId OUTPUT,@successCount OUTPUT, @status OUTPUT"
                   , dataTableParameter, uploadedByParameter, fileNameParameter, batchId, successCount, uploadStatus);
                    break;

                case nameof(VacancyPool):
                    dataTableParameter = new SqlParameter() { ParameterName = "@vacancyPoolData", SqlDbType = SqlDbType.Structured, Value = dataTable, TypeName = "VacancyPoolTableType" };
                    result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC STP_VacancyPool_BulkUpload @vacancyPoolData,@uploadedBy,@fileName, @batchId OUTPUT,@successCount OUTPUT, @status OUTPUT"
                   , dataTableParameter, uploadedByParameter, fileNameParameter, batchId, successCount, uploadStatus);
                    break;

                default: throw new ArgumentException("This file type is note present");
            }

            int batchid = Convert.ToInt32(batchId.Value);
            int successcount = Convert.ToInt32(successCount.Value);
            string status = Convert.ToString(uploadStatus.Value);

            return new ExcelUploadResult() { SuccessCount = successcount, UploadStatus = status, BatchId = batchid };
        }

        public async Task<string> GetAllRecords<T>(GetExcelDataQuery<T> request) where T : class
        {
            if (request.FileTypeCode == (int)ExcelFileType.BranchMaster)
            {
                SqlParameter BatchId = new SqlParameter() { ParameterName = "@batchId", SqlDbType = SqlDbType.Int, Value = request.BatchId };
                SqlParameter pageNumerParameter = new SqlParameter() { ParameterName = "@PageNumber", SqlDbType = SqlDbType.Int, Value = request.PageNumber };
                SqlParameter numberOfRecordsParameter = new SqlParameter() { ParameterName = "@NumberOfRecords", SqlDbType = SqlDbType.Int, Value = request.NumberOfRecords };
                SqlParameter sortDirectionParameter = new SqlParameter() { ParameterName = "@SortDirection", SqlDbType = SqlDbType.VarChar, Size = 20, Value = request.SortDirection };
                SqlParameter sortColumnParameter = new SqlParameter() { ParameterName = "@SortColumn", SqlDbType = SqlDbType.VarChar, Size = 30, Value = request.SortColumn };
                SqlParameter totalRecordsParameter = new SqlParameter() { ParameterName = "@TotalRecords", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
                var branchMasterList = _dbContext.Set<BranchMasterRecordsDto>().FromSqlRaw("EXEC STP_BranchMasterDataTable_DisplayRecordsByBatch @batchId,@PageNumber,@NumberOfRecords, @SortDirection,@SortColumn, @TotalRecords OUTPUT",
                    BatchId, pageNumerParameter, numberOfRecordsParameter, sortDirectionParameter, sortColumnParameter, totalRecordsParameter).ToList();
                int totalRecords = Convert.ToInt32(totalRecordsParameter.Value);
                UploadedRecordsResponse<BranchMasterRecordsDto> result = new()
                {
                    Data = branchMasterList,
                    TotalRecords = totalRecords
                };
                var response = JsonConvert.SerializeObject(result);
                return response;
            }
            else if (request.FileTypeCode == (int)ExcelFileType.DepartmentMaster)
            {
                SqlParameter BatchId = new SqlParameter() { ParameterName = "@batchId", SqlDbType = SqlDbType.Int, Value = request.BatchId };
                SqlParameter pageNumerParameter = new SqlParameter() { ParameterName = "@PageNumber", SqlDbType = SqlDbType.Int, Value = request.PageNumber };
                SqlParameter numberOfRecordsParameter = new SqlParameter() { ParameterName = "@NumberOfRecords", SqlDbType = SqlDbType.Int, Value = request.NumberOfRecords };
                SqlParameter sortDirectionParameter = new SqlParameter() { ParameterName = "@SortDirection", SqlDbType = SqlDbType.VarChar, Size = 20, Value = request.SortDirection };
                SqlParameter sortColumnParameter = new SqlParameter() { ParameterName = "@SortColumn", SqlDbType = SqlDbType.VarChar, Size = 30, Value = request.SortColumn };
                SqlParameter totalRecordsParameter = new SqlParameter() { ParameterName = "@TotalRecords", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
                var departmentMasterList = _dbContext.Set<DepartmentMasterRecordsDto>().FromSqlRaw("EXEC STP_DepartmentMasterDataTable_DisplayRecordsByBatch @batchId,@PageNumber,@NumberOfRecords, @SortDirection, @SortColumn, @TotalRecords OUTPUT",
                    BatchId, pageNumerParameter, numberOfRecordsParameter, sortDirectionParameter, sortColumnParameter, totalRecordsParameter).ToList();
                int totalRecords = Convert.ToInt32(totalRecordsParameter.Value);
                UploadedRecordsResponse<DepartmentMasterRecordsDto> result = new()
                {
                    Data = departmentMasterList,
                    TotalRecords = totalRecords
                };
                var response = JsonConvert.SerializeObject(result);
                return response;
            }
            else if (request.FileTypeCode == (int)ExcelFileType.EmployeeMaster)
            {
                SqlParameter BatchId = new SqlParameter() { ParameterName = "@batchId", SqlDbType = SqlDbType.Int, Value = request.BatchId };
                SqlParameter pageNumerParameter = new SqlParameter() { ParameterName = "@PageNumber", SqlDbType = SqlDbType.Int, Value = request.PageNumber };
                SqlParameter numberOfRecordsParameter = new SqlParameter() { ParameterName = "@NumberOfRecords", SqlDbType = SqlDbType.Int, Value = request.NumberOfRecords };
                SqlParameter sortDirectionParameter = new SqlParameter() { ParameterName = "@SortDirection", SqlDbType = SqlDbType.VarChar, Size = 20, Value = request.SortDirection };
                SqlParameter sortColumnParameter = new SqlParameter() { ParameterName = "@SortColumn", SqlDbType = SqlDbType.VarChar, Size = 30, Value = request.SortColumn };
                SqlParameter totalRecordsParameter = new SqlParameter() { ParameterName = "@TotalRecords", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
                var employeeMasterList = _dbContext.Set<EmployeeMasterRecordsDto>().FromSqlRaw("EXEC STP_EmployeeMasterDataTable_DisplayRecordsByBatch @batchId,@PageNumber,@NumberOfRecords, @SortDirection,@SortColumn, @TotalRecords OUTPUT",
                    BatchId, pageNumerParameter, numberOfRecordsParameter, sortDirectionParameter, sortColumnParameter, totalRecordsParameter).ToList();
                int totalRecords = Convert.ToInt32(totalRecordsParameter.Value);
                UploadedRecordsResponse<EmployeeMasterRecordsDto> result = new()
                {
                    Data = employeeMasterList,
                    TotalRecords = totalRecords
                };               
                var response = JsonConvert.SerializeObject(result);
                return response;
            }
            else if (request.FileTypeCode == (int)ExcelFileType.InterRegionPromotion)
            {
                SqlParameter BatchId = new SqlParameter() { ParameterName = "@batchId", SqlDbType = SqlDbType.Int, Value = request.BatchId };
                SqlParameter pageNumerParameter = new SqlParameter() { ParameterName = "@PageNumber", SqlDbType = SqlDbType.Int, Value = request.PageNumber };
                SqlParameter numberOfRecordsParameter = new SqlParameter() { ParameterName = "@NumberOfRecords", SqlDbType = SqlDbType.Int, Value = request.NumberOfRecords };
                SqlParameter sortDirectionParameter = new SqlParameter() { ParameterName = "@SortDirection", SqlDbType = SqlDbType.VarChar, Size = 20, Value = request.SortDirection };
                SqlParameter sortColumnParameter = new SqlParameter() { ParameterName = "@SortColumn", SqlDbType = SqlDbType.VarChar, Size = 30, Value = request.SortColumn };
                SqlParameter totalRecordsParameter = new SqlParameter() { ParameterName = "@TotalRecords", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
                var interRegionPromotionList = _dbContext.Set<InterRegionalPromotionRecordsDto>().FromSqlRaw("EXEC STP_InterRegionalPromotionTbl_DisplayRecordsByBatch @batchId,@PageNumber,@NumberOfRecords, @SortDirection, @SortColumn, @TotalRecords OUTPUT",
                    BatchId, pageNumerParameter, numberOfRecordsParameter, sortDirectionParameter, sortColumnParameter, totalRecordsParameter).ToList();
                int totalRecords = Convert.ToInt32(totalRecordsParameter.Value);
                UploadedRecordsResponse<InterRegionalPromotionRecordsDto> result = new()
                {
                    Data = interRegionPromotionList,
                    TotalRecords = totalRecords
                };
                var response = JsonConvert.SerializeObject(result);
                return response;
            }
            else if (request.FileTypeCode == (int)ExcelFileType.InterRegionRequestTransfer)
            {
                SqlParameter BatchId = new SqlParameter() { ParameterName = "@batchId", SqlDbType = SqlDbType.Int, Value = request.BatchId };
                SqlParameter pageNumerParameter = new SqlParameter() { ParameterName = "@PageNumber", SqlDbType = SqlDbType.Int, Value = request.PageNumber };
                SqlParameter numberOfRecordsParameter = new SqlParameter() { ParameterName = "@NumberOfRecords", SqlDbType = SqlDbType.Int, Value = request.NumberOfRecords };
                SqlParameter sortDirectionParameter = new SqlParameter() { ParameterName = "@SortDirection", SqlDbType = SqlDbType.VarChar, Size = 20, Value = request.SortDirection };
                SqlParameter sortColumnParameter = new SqlParameter() { ParameterName = "@SortColumn", SqlDbType = SqlDbType.VarChar, Size = 30, Value = request.SortColumn };
                SqlParameter totalRecordsParameter = new SqlParameter() { ParameterName = "@TotalRecords", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
                var interRegionRequestList = _dbContext.Set<InterRegionalRequestRecordsDto>().FromSqlRaw("EXEC STP_InterRegionRequestTransferTbl_DisplayRecordsByBatch @batchId,@PageNumber,@NumberOfRecords, @SortDirection, @SortColumn, @TotalRecords OUTPUT",
                    BatchId, pageNumerParameter, numberOfRecordsParameter, sortDirectionParameter, sortColumnParameter, totalRecordsParameter).ToList();
                int totalRecords = Convert.ToInt32(totalRecordsParameter.Value);
                UploadedRecordsResponse<InterRegionalRequestRecordsDto> result = new()
                {
                    Data = interRegionRequestList,
                    TotalRecords = totalRecords
                };
                var response = JsonConvert.SerializeObject(result);
                return response;
            }
            else if (request.FileTypeCode == (int)ExcelFileType.InterZonalPromotion)
            {
                SqlParameter BatchId = new SqlParameter() { ParameterName = "@batchId", SqlDbType = SqlDbType.Int, Value = request.BatchId };
                SqlParameter pageNumerParameter = new SqlParameter() { ParameterName = "@PageNumber", SqlDbType = SqlDbType.Int, Value = request.PageNumber };
                SqlParameter numberOfRecordsParameter = new SqlParameter() { ParameterName = "@NumberOfRecords", SqlDbType = SqlDbType.Int, Value = request.NumberOfRecords };
                SqlParameter sortDirectionParameter = new SqlParameter() { ParameterName = "@SortDirection", SqlDbType = SqlDbType.VarChar, Size = 20, Value = request.SortDirection };
                SqlParameter sortColumnParameter = new SqlParameter() { ParameterName = "@SortColumn", SqlDbType = SqlDbType.VarChar, Size = 30, Value = request.SortColumn };
                SqlParameter totalRecordsParameter = new SqlParameter() { ParameterName = "@TotalRecords", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
                var interZonalPromotionList = _dbContext.Set<InterZonalPromotionRecordsDto>().FromSqlRaw("EXEC STP_InterZonalPromotionTbl_DisplayRecordsByBatch @batchId,@PageNumber,@NumberOfRecords, @SortDirection, @SortColumn, @TotalRecords OUTPUT",
                    BatchId, pageNumerParameter, numberOfRecordsParameter, sortDirectionParameter, sortColumnParameter, totalRecordsParameter).ToList();
                int totalRecords = Convert.ToInt32(totalRecordsParameter.Value);
                UploadedRecordsResponse<InterZonalPromotionRecordsDto> result = new()
                {
                    Data = interZonalPromotionList,
                    TotalRecords = totalRecords
                };
                var response = JsonConvert.SerializeObject(result);
                return response;
            }
            else if (request.FileTypeCode == (int)ExcelFileType.InterZonalRequestTranfer)
            {
                SqlParameter BatchId = new SqlParameter() { ParameterName = "@batchId", SqlDbType = SqlDbType.Int, Value = request.BatchId };
                SqlParameter pageNumerParameter = new SqlParameter() { ParameterName = "@PageNumber", SqlDbType = SqlDbType.Int, Value = request.PageNumber };
                SqlParameter numberOfRecordsParameter = new SqlParameter() { ParameterName = "@NumberOfRecords", SqlDbType = SqlDbType.Int, Value = request.NumberOfRecords };
                SqlParameter sortDirectionParameter = new SqlParameter() { ParameterName = "@SortDirection", SqlDbType = SqlDbType.VarChar, Size = 20, Value = request.SortDirection };
                SqlParameter sortColumnParameter = new SqlParameter() { ParameterName = "@SortColumn", SqlDbType = SqlDbType.VarChar, Size = 30, Value = request.SortColumn };
                SqlParameter totalRecordsParameter = new SqlParameter() { ParameterName = "@TotalRecords", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
                var interZonalRequestList = _dbContext.Set<InterZonalRequestRecordsDto>().FromSqlRaw("EXEC STP_InterZonalRequestTransferTbl_DisplayRecordsByBatch @batchId,@PageNumber,@NumberOfRecords, @SortDirection, @SortColumn, @TotalRecords OUTPUT",
                    BatchId, pageNumerParameter, numberOfRecordsParameter, sortDirectionParameter, sortColumnParameter, totalRecordsParameter).ToList();
                int totalRecords = Convert.ToInt32(totalRecordsParameter.Value);
                UploadedRecordsResponse<InterZonalRequestRecordsDto> result = new()
                {
                    Data = interZonalRequestList,
                    TotalRecords = totalRecords
                };
                var response = JsonConvert.SerializeObject(result);
                return response;
            }
            else if (request.FileTypeCode == (int)ExcelFileType.RegionMaster)
            {
                SqlParameter BatchId = new SqlParameter() { ParameterName = "@batchId", SqlDbType = SqlDbType.Int, Value = request.BatchId };
                SqlParameter pageNumerParameter = new SqlParameter() { ParameterName = "@PageNumber", SqlDbType = SqlDbType.Int, Value = request.PageNumber };
                SqlParameter numberOfRecordsParameter = new SqlParameter() { ParameterName = "@NumberOfRecords", SqlDbType = SqlDbType.Int, Value = request.NumberOfRecords };
                SqlParameter sortDirectionParameter = new SqlParameter() { ParameterName = "@SortDirection", SqlDbType = SqlDbType.VarChar, Size = 20, Value = request.SortDirection };
                SqlParameter sortColumnParameter = new SqlParameter() { ParameterName = "@SortColumn", SqlDbType = SqlDbType.VarChar, Size = 30, Value = request.SortColumn };
                SqlParameter totalRecordsParameter = new SqlParameter() { ParameterName = "@TotalRecords", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
                var regionMasterList = _dbContext.Set<RegionMasterRecordsDto>().FromSqlRaw("EXEC STP_RegionMasterDataTable_DisplayRecordsByBatch @batchId,@PageNumber,@NumberOfRecords, @SortDirection, @SortColumn, @TotalRecords OUTPUT",
                    BatchId, pageNumerParameter, numberOfRecordsParameter, sortDirectionParameter, sortColumnParameter, totalRecordsParameter).ToList();
                int totalRecords = Convert.ToInt32(totalRecordsParameter.Value);
                UploadedRecordsResponse<RegionMasterRecordsDto> result = new()
                {
                    Data = regionMasterList,
                    TotalRecords = totalRecords
                };
                var response = JsonConvert.SerializeObject(result);
                return response;
            }
            else if (request.FileTypeCode == (int)ExcelFileType.ZoneMaster)
            {
                SqlParameter BatchId = new SqlParameter() { ParameterName = "@batchId", SqlDbType = SqlDbType.Int, Value = request.BatchId };
                SqlParameter pageNumerParameter = new SqlParameter() { ParameterName = "@PageNumber", SqlDbType = SqlDbType.Int, Value = request.PageNumber };
                SqlParameter numberOfRecordsParameter = new SqlParameter() { ParameterName = "@NumberOfRecords", SqlDbType = SqlDbType.Int, Value = request.NumberOfRecords };
                SqlParameter sortDirectionParameter = new SqlParameter() { ParameterName = "@SortDirection", SqlDbType = SqlDbType.VarChar, Size = 20, Value = request.SortDirection };
                SqlParameter sortColumnParameter = new SqlParameter() { ParameterName = "@SortColumn", SqlDbType = SqlDbType.VarChar, Size = 30, Value = request.SortColumn };
                SqlParameter totalRecordsParameter = new SqlParameter() { ParameterName = "@TotalRecords", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
                var zoneMasterList = _dbContext.Set<ZoneMasterRecordsDto>().FromSqlRaw("EXEC STP_ZoneMasterDataTable_DisplayRecordsByBatch @batchId,@PageNumber,@NumberOfRecords, @SortDirection, @SortColumn, @TotalRecords OUTPUT",
                    BatchId, pageNumerParameter, numberOfRecordsParameter, sortDirectionParameter, sortColumnParameter, totalRecordsParameter).ToList();
                int totalRecords = Convert.ToInt32(totalRecordsParameter.Value);
                UploadedRecordsResponse<ZoneMasterRecordsDto> result = new()
                {
                    Data = zoneMasterList,
                    TotalRecords = totalRecords
                };
                var response = JsonConvert.SerializeObject(result);
                return response;
            }

            return "No Records Found";
        }

        public async Task<List<UploadHistoryDetails>> GetUploadHistoryList(int fileTypeCode)
        {
            var fileTypeCodeParameter = new SqlParameter() { ParameterName = "@fileTypeCode", SqlDbType = SqlDbType.Int, Value = fileTypeCode };
            var historyList = await _dbContext.Set<UploadHistoryDetails>().FromSqlRaw("EXEC STP_GetUploadHistoryDetails @fileTypeCode", fileTypeCodeParameter).ToListAsync();
            return historyList;
        }
    }
}

