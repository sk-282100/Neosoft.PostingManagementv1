using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PostingManagement.Application.Contracts.Persistence;
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

        public async Task<string> GetAllRecords<T>(int fileTypeCode, int batchId)
        {
            if (fileTypeCode == (int)ExcelFileType.BranchMaster)
            {
                var BatchId = new SqlParameter() { ParameterName = "@batchId", SqlDbType = SqlDbType.Int, Value = batchId };
                var branchMasterList = _dbContext.Set<BranchMasterRecordsDto>().FromSqlRaw("EXEC STP_BranchMasterDataTable_DisplayRecordsByBatch @batchId", BatchId).ToList();
                if (branchMasterList.Count > 0)
                {
                    return JsonConvert.SerializeObject(branchMasterList);
                }
            }
            else if (fileTypeCode == (int)ExcelFileType.DepartmentMaster)
            {
                var BatchId = new SqlParameter() { ParameterName = "@batchId", SqlDbType = SqlDbType.Int, Value = batchId };
                var departmentMasterList = await _dbContext.Set<DepartmentMasterRecordsDto>().FromSqlRaw("EXEC STP_DepartmentMasterDataTable_DisplayRecordsByBatch @batchId", BatchId).ToListAsync();
                if (departmentMasterList.Count > 0)
                {
                    return JsonConvert.SerializeObject(departmentMasterList);
                }
            }
            else if (fileTypeCode == (int)ExcelFileType.EmployeeMaster)
            {
                var BatchId = new SqlParameter() { ParameterName = "@batchId", SqlDbType = SqlDbType.Int, Value = batchId };
                var employeeMasterList = _dbContext.Set<EmployeeMasterRecordsDto>().FromSqlRaw("EXEC STP_EmployeeMasterDataTable_DisplayRecordsByBatch @batchId", BatchId).ToList();
                if (employeeMasterList.Count > 0)
                {
                    return JsonConvert.SerializeObject(employeeMasterList);
                }
            }
            else if (fileTypeCode == (int)ExcelFileType.InterRegionPromotion)
            {
                var BatchId = new SqlParameter() { ParameterName = "@batchId", SqlDbType = SqlDbType.Int, Value = batchId };
                var interRegionalPromotionList = await _dbContext.Set<InterRegionalPromotionRecordsDto>().FromSqlRaw("EXEC STP_InterRegionalPromotionTbl_DisplayRecordsByBatch @batchId", BatchId).ToListAsync();
                if (interRegionalPromotionList.Count > 0)
                {
                    return JsonConvert.SerializeObject(interRegionalPromotionList);
                }
            }
            else if (fileTypeCode == (int)ExcelFileType.InterRegionRequestTransfer)
            {
                var BatchId = new SqlParameter() { ParameterName = "@batchId", SqlDbType = SqlDbType.Int, Value = batchId };
                var interRegionalRequestList = await _dbContext.Set<InterRegionalRequestRecordsDto>().FromSqlRaw("EXEC STP_InterRegionRequestTransferTbl_DisplayRecordsByBatch @batchId", BatchId).ToListAsync();
                if (interRegionalRequestList.Count > 0)
                {
                    return JsonConvert.SerializeObject(interRegionalRequestList);
                }
            }
            else if (fileTypeCode == (int)ExcelFileType.InterZonalPromotion)
            {
                var BatchId = new SqlParameter() { ParameterName = "@batchId", SqlDbType = SqlDbType.Int, Value = batchId };
                var interZonalPromotionList = await _dbContext.Set<InterZonalPromotionRecordsDto>().FromSqlRaw("EXEC STP_InterZonalPromotionTbl_DisplayRecordsByBatch @batchId", BatchId).ToListAsync();
                if (interZonalPromotionList.Count > 0)
                {
                    return JsonConvert.SerializeObject(interZonalPromotionList);
                }
            }
            else if (fileTypeCode == (int)ExcelFileType.InterZonalRequestTranfer)
            {
                var BatchId = new SqlParameter() { ParameterName = "@batchId", SqlDbType = SqlDbType.Int, Value = batchId };
                var interZonalRequestList = await _dbContext.Set<InterZonalRequestRecordsDto>().FromSqlRaw("EXEC STP_InterZonalRequestTransferTbl_DisplayRecordsByBatch @batchId", BatchId).ToListAsync();
                if (interZonalRequestList.Count > 0)
                {
                    return JsonConvert.SerializeObject(interZonalRequestList);
                }
            }
            else if (fileTypeCode == (int)ExcelFileType.RegionMaster)
            {
                var BatchId = new SqlParameter() { ParameterName = "@batchId", SqlDbType = SqlDbType.Int, Value = batchId };
                var regionMasterList = await _dbContext.Set<RegionMasterRecordsDto>().FromSqlRaw("EXEC STP_RegionMasterDataTable_DisplayRecordsByBatch @batchId", BatchId).ToListAsync();
                if (true)
                {
                    return JsonConvert.SerializeObject(regionMasterList);
                }
            }
            else if (fileTypeCode == (int)ExcelFileType.ZoneMaster)
            {
                var BatchId = new SqlParameter() { ParameterName = "@batchId", SqlDbType = SqlDbType.Int, Value = batchId };
                var zoneMasterList = await _dbContext.Set<ZoneMasterRecordsDto>().FromSqlRaw("EXEC STP_ZoneMasterDataTable_DisplayRecordsByBatch @batchId", BatchId).ToListAsync();
                if (zoneMasterList.Count > 0)
                {
                    return JsonConvert.SerializeObject(zoneMasterList);
                }
            }
            return "No Records Found!";
        }
        public async Task<List<UploadHistoryDetails>> GetUploadHistoryList(int fileTypeCode)
        {
            var fileTypeCodeParameter = new SqlParameter() { ParameterName = "@fileTypeCode", SqlDbType = SqlDbType.Int, Value = fileTypeCode };
            var historyList = await _dbContext.Set<UploadHistoryDetails>().FromSqlRaw("EXEC STP_GetUploadHistoryDetails @fileTypeCode", fileTypeCodeParameter).ToListAsync();
            return historyList;
        }
        
        public async Task<WorkFlowStatusModel> GetWorkFlowStatus()
        {
            SqlParameter employeeMasterListStatus = new SqlParameter() { ParameterName = "@employeeMasterListStatus", SqlDbType = SqlDbType.VarChar, Size = 10, Direction = ParameterDirection.Output };
            SqlParameter vacancyListStatus = new SqlParameter() { ParameterName = "@vacancyListStatus", SqlDbType = SqlDbType.VarChar, Size = 10, Direction = ParameterDirection.Output };
            SqlParameter interRequestStatus = new SqlParameter() { ParameterName = "@interRequestStatus", SqlDbType = SqlDbType.VarChar, Size = 10, Direction = ParameterDirection.Output };
            SqlParameter generateEmployeeTransfer = new SqlParameter() { ParameterName = "@generateEmployeeTransfer", SqlDbType = SqlDbType.VarChar, Size = 10, Direction = ParameterDirection.Output };
            SqlParameter workFlowStatus = new SqlParameter() { ParameterName = "@workflowStatus", SqlDbType = SqlDbType.VarChar, Size = 20, Direction = ParameterDirection.Output };

            var result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC STP_CheckIfListsUploadedForCurrentMonth @employeeMasterListStatus OUTPUT,@vacancyListStatus OUTPUT,@interRequestStatus OUTPUT,@generateEmployeeTransfer OUTPUT,@workflowStatus OUTPUT",
                employeeMasterListStatus, vacancyListStatus, interRequestStatus, generateEmployeeTransfer, workFlowStatus);

            WorkFlowStatusModel status = new WorkFlowStatusModel()
            {

                EmployeeMasterListStatus = Convert.ToString(employeeMasterListStatus.Value),
                VacancyListStatus = Convert.ToString(vacancyListStatus.Value),
                InterRequestStatus = Convert.ToString(interRequestStatus.Value),
                GenerateEmployeeTransfer = Convert.ToString(generateEmployeeTransfer.Value),
                WorkFlowstatus = Convert.ToString(workFlowStatus.Value)
            };
            return status;
        }
        public async Task<bool> ResetWorkflow()
        {
            try
            {
                var result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC STP_ResetWorkFlow");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

