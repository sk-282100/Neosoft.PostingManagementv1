using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;

namespace PostingManagement.Persistence.Repositories
{
    public class ExcelUploadRepository : IExcelUploadRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ExcelUploadRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<string> GetAllRecords<T>(int fileTypeCode, int batchId)
        {
            if (fileTypeCode == 1)
            {                                
                var BatchId = new SqlParameter() { ParameterName = "@batchId", SqlDbType = SqlDbType.Int, Value = batchId };
                var branchMasterList = _dbContext.Set<BranchMaster>().FromSqlRaw("EXEC STP_BranchMasterDataTable_DisplayRecordsByBatch @batchId", BatchId).ToList();                
                if (branchMasterList.Count > 0)
                {                    
                    return JsonConvert.SerializeObject(branchMasterList); 
                }                
            }
            else if (fileTypeCode == 2)
            {                
                var BatchId = new SqlParameter() { ParameterName = "@batchId", SqlDbType = SqlDbType.Int, Value = batchId };
                var departmentMasterList = await _dbContext.Set<DepartmentMaster>().FromSqlRaw("EXEC STP_DepartmentMasterDataTable_DisplayRecordsByBatch @batchId", BatchId).ToListAsync();
                if (departmentMasterList.Count > 0)
                {
                    return JsonConvert.SerializeObject(departmentMasterList);
                }                
            }
            else if(fileTypeCode == 3)
            {
                var BatchId = new SqlParameter() { ParameterName = "@batchId", SqlDbType = SqlDbType.Int, Value = batchId };
                var employeeMasterList = await _dbContext.Set<EmployeeMaster>().FromSqlRaw("EXEC STP_EmployeeMasterDataTable_DisplayRecordsByBatch @batchId", BatchId).ToListAsync();
                if (employeeMasterList.Count > 0)
                {
                    return JsonConvert.SerializeObject(employeeMasterList);
                }                
            }
            else if (fileTypeCode == 4)
            {
                var BatchId = new SqlParameter() { ParameterName = "@batchId", SqlDbType = SqlDbType.Int, Value = batchId };
                var interRegionalPromotionList = await _dbContext.Set<InterRegionalPromotion>().FromSqlRaw("EXEC STP_InterRegionalPromotionTbl_DisplayRecordsByBatch @batchId", BatchId).ToListAsync();
                if (interRegionalPromotionList.Count > 0)
                {
                    return JsonConvert.SerializeObject(interRegionalPromotionList);
                }                
            }
            else if (fileTypeCode == 5)
            {
                var BatchId = new SqlParameter() { ParameterName = "@batchId", SqlDbType = SqlDbType.Int, Value = batchId };
                var interRegionalRequestList = await _dbContext.Set<InterRegionRequestTransfer>().FromSqlRaw("EXEC STP_InterRegionRequestTransferTbl_DisplayRecordsByBatch @batchId", BatchId).ToListAsync();
                if (interRegionalRequestList.Count > 0)
                {
                    return JsonConvert.SerializeObject(interRegionalRequestList);
                }                
            }
            else if (fileTypeCode == 6)
            {
                var BatchId = new SqlParameter() { ParameterName = "@batchId", SqlDbType = SqlDbType.Int, Value = batchId };
                var interZonalPromotionList = await _dbContext.Set<InterZonalPromotion>().FromSqlRaw("EXEC STP_InterZonalPromotionTbl_DisplayRecordsByBatch @batchId", BatchId).ToListAsync();
                if (interZonalPromotionList.Count > 0)
                {
                    return JsonConvert.SerializeObject(interZonalPromotionList);
                }                
            }
            else if (fileTypeCode == 7)
            {
                var BatchId = new SqlParameter() { ParameterName = "@batchId", SqlDbType = SqlDbType.Int, Value = batchId };
                var interZonalRequestList = await _dbContext.Set<InterZonalRequestTransfer>().FromSqlRaw("EXEC STP_InterZonalRequestTransferTbl_DisplayRecordsByBatch @batchId", BatchId).ToListAsync();
                if (interZonalRequestList.Count > 0)
                {
                    return JsonConvert.SerializeObject(interZonalRequestList);
                }                
            }
            else if (fileTypeCode == 8)
            {                
                var BatchId = new SqlParameter() { ParameterName = "@batchId", SqlDbType = SqlDbType.Int, Value = batchId };
                var regionMasterList = await _dbContext.Set<RegionMaster>().FromSqlRaw("EXEC STP_RegionMasterDataTable_DisplayRecordsByBatch @batchId", BatchId).ToListAsync();
                if (regionMasterList.Count > 0)
                {
                    return JsonConvert.SerializeObject(regionMasterList);
                }                
            }
            else if (fileTypeCode == 9)
            {                
                var BatchId = new SqlParameter() { ParameterName = "@batchId", SqlDbType = SqlDbType.Int, Value = batchId };
                var zoneMasterList = await _dbContext.Set<ZoneMaster>().FromSqlRaw("EXEC STP_ZoneMasterDataTable_DisplayRecordsByBatch @batchId", BatchId).ToListAsync();
                if (zoneMasterList.Count > 0)
                {
                    return JsonConvert.SerializeObject(zoneMasterList);
                }                
            }            
            return "No Records Found!";         
        }
        public async Task<ExcelUploadResult> AddAsync<T>(string uploadedBy, List<T> excelData ,string fileName) 
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {                
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in excelData)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    if(Props[i].PropertyType == typeof(DateTime))
                    {
                        DateTime date= (DateTime)Props[i].GetValue(item, null);
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

            switch (typeof(T).Name)
            {
                case nameof(BranchMaster):
                     dataTableParameter = new SqlParameter() { ParameterName = "@branchMasterData", SqlDbType = SqlDbType.Structured, Value = dataTable, TypeName = "BranchMasterTableType" };
                     result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC STP_BranchMaster_BulkUpload @branchMasterData,@uploadedBy,@fileName,@successCount OUTPUT, @status OUTPUT, @batchId OUTPUT"
                    , dataTableParameter, uploadedByParameter,fileNameParameter, successCount, uploadStatus, batchId);
                    break;

                case nameof(EmployeeMaster):
                      dataTableParameter = new SqlParameter() { ParameterName = "@employeeMasterData", SqlDbType = SqlDbType.Structured, Value = dataTable, TypeName = "EmployeeMasterTableType" };
                     result = _dbContext.Database.ExecuteSqlRaw("EXEC STP_EmployeeMasterData_InsertingData @employeeMasterData,@uploadedBy,@fileName,@batchId OUTPUT, @successCount OUTPUT, @status OUTPUT"
                    , dataTableParameter, uploadedByParameter, fileNameParameter, successCount, uploadStatus, batchId);
                    Console.WriteLine(result);
                    break;

                case nameof(InterRegionalPromotion):
                    dataTableParameter = new SqlParameter() { ParameterName = "@interRegionPromotionData", SqlDbType = SqlDbType.Structured, Value = dataTable, TypeName = "InterRegionalPromotionTableType" };
                    result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC STP_InterRegionalPromotion_BulkUpload @interRegionPromotionData,@uploadedBy,@fileName,@successCount OUTPUT, @status OUTPUT, @batchId OUTPUT"
                   , dataTableParameter, uploadedByParameter, fileNameParameter, successCount, uploadStatus, batchId);
                    break;

                case nameof(InterRegionRequestTransfer):
                    dataTableParameter = new SqlParameter() { ParameterName = "@interRegionRequestTransferData", SqlDbType = SqlDbType.Structured, Value = dataTable, TypeName = "InterRegionRequestTransferTableType" };
                    result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC STP_InterRegionRequestTransfer_BulkUpload @interRegionRequestTransferData,@uploadedBy,@fileName,@successCount OUTPUT, @status OUTPUT, @batchId OUTPUT"
                   , dataTableParameter, uploadedByParameter, fileNameParameter, successCount, uploadStatus, batchId);
                    break;

                case nameof(InterZonalPromotion):
                    dataTableParameter = new SqlParameter() { ParameterName = "@interZonalPromotionData", SqlDbType = SqlDbType.Structured, Value = dataTable, TypeName = "InterZonalPromotionTableType" };
                    result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC STP_InterZonalPromotion_BulkUpload @interZonalPromotionData,@uploadedBy,@fileName,@successCount OUTPUT, @status OUTPUT, @batchId OUTPUT"
                   , dataTableParameter, uploadedByParameter, fileNameParameter, successCount, uploadStatus, batchId);
                    break;

                case nameof(InterZonalRequestTransfer):
                    dataTableParameter = new SqlParameter() { ParameterName = "@interZonalRequestTransferTableType", SqlDbType = SqlDbType.Structured, Value = dataTable, TypeName = "InterZonalRequestTransferTableType" };
                    result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC STP_InterZonalRequestTransfer_BulkUpload @interZonalRequestTransferTableType,@uploadedBy,@fileName,@successCount OUTPUT, @status OUTPUT, @batchId OUTPUT"
                   , dataTableParameter, uploadedByParameter, fileNameParameter, successCount, uploadStatus, batchId);
                    break;

                case nameof(RegionMaster):
                    dataTableParameter = new SqlParameter() { ParameterName = "@regionMasterData", SqlDbType = SqlDbType.Structured, Value = dataTable, TypeName = "RegionMasterDataType" };
                    result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC STP_RegionMaster_BulkUpload @regionMasterData,@uploadedBy,@fileName,@successCount OUTPUT, @status OUTPUT, @batchId OUTPUT"
                   , dataTableParameter, uploadedByParameter, fileNameParameter, successCount, uploadStatus, batchId);
                    break;

                case nameof(ZoneMaster):
                    dataTableParameter = new SqlParameter() { ParameterName = "@zoneMasterData", SqlDbType = SqlDbType.Structured, Value = dataTable, TypeName = "ZoneMasterDataType" };
                    result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC STP_ZoneMaster_BulkUpload @zoneMasterData,@uploadedBy,@fileName,@successCount OUTPUT, @status OUTPUT, @batchId OUTPUT"
                   , dataTableParameter, uploadedByParameter, fileNameParameter, successCount, uploadStatus, batchId);
                    break;

                case nameof(DepartmentMaster):
                    dataTableParameter = new SqlParameter() { ParameterName = "@departmentMasterData", SqlDbType = SqlDbType.Structured, Value = dataTable, TypeName = "DepartmentMasterDataType" };
                    result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC STP_DepartmentMaster_BulkUpload @departmentMasterData,@uploadedBy,@fileName,@successCount OUTPUT, @status OUTPUT, @batchId OUTPUT"
                   , dataTableParameter, uploadedByParameter, fileNameParameter, successCount, uploadStatus, batchId);
                    break;

                default: throw new ArgumentException("This file type is note present");
            }

            int batchid = Convert.ToInt32(batchId.Value);
            int successcount = Convert.ToInt32(successCount.Value);
            string status = Convert.ToString(uploadStatus.Value);

            return new ExcelUploadResult() { SuccessCount = successcount, UploadStatus = status, BatchId = batchid };
        }
        

        public async Task<List<UploadHistoryDetails>> GetUploadHistoryList(int fileTypeCode)
        {
            var fileTypeCodeParameter = new SqlParameter() { ParameterName = "@fileTypeCode", SqlDbType = SqlDbType.Int,  Value = fileTypeCode };

            //var historyList = _dbContext.UploadHistoryDetails.ToList();
            var historyList = await _dbContext.Set<UploadHistoryDetails>().FromSqlRaw("EXEC STP_GetUploadHistoryDetails @fileTypeCode", fileTypeCodeParameter).ToListAsync();
            return historyList;
        }
    }
}

