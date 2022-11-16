using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PostingManagement.Application.Contracts.Persistence;
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
            var result = 0;

            switch (typeof(T).Name)
            {
                case nameof(BranchMaster):
                     dataTableParameter = new SqlParameter() { ParameterName = "@branchMasterData", SqlDbType = SqlDbType.Structured, Value = dataTable, TypeName = "BranchMasterTableType" };
                     result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC STP_BranchMaster_BulkUpload @branchMasterData,@uploadedBy,@fileName,@successCount OUTPUT, @status OUTPUT"
                    , dataTableParameter, uploadedByParameter,fileNameParameter, successCount, uploadStatus);
                    break;

                case nameof(EmployeeMaster):
                      dataTableParameter = new SqlParameter() { ParameterName = "@employeeMasterData", SqlDbType = SqlDbType.Structured, Value = dataTable, TypeName = "EmployeeMasterTableType" };
                     result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC STP_EmployeeMasterData_InsertingData @employeeMasterData,@uploadedBy,@fileName,@successCount OUTPUT, @status OUTPUT"
                    , dataTableParameter, uploadedByParameter, fileNameParameter, successCount, uploadStatus);
                    break;

                case nameof(InterRegionalPromotion):
                    dataTableParameter = new SqlParameter() { ParameterName = "@interRegionPromotionData", SqlDbType = SqlDbType.Structured, Value = dataTable, TypeName = "InterRegionalPromotionTableType" };
                    result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC STP_InterRegionalPromotion_BulkUpload @interRegionPromotionData,@uploadedBy,@fileName,@successCount OUTPUT, @status OUTPUT"
                   , dataTableParameter, uploadedByParameter, fileNameParameter, successCount, uploadStatus);
                    break;

                case nameof(InterRegionRequestTransfer):
                    dataTableParameter = new SqlParameter() { ParameterName = "@interRegionRequestTransferData", SqlDbType = SqlDbType.Structured, Value = dataTable, TypeName = "InterRegionRequestTransferTableType" };
                    result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC STP_InterRegionRequestTransfer_BulkUpload @interRegionRequestTransferData,@uploadedBy,@fileName,@successCount OUTPUT, @status OUTPUT"
                   , dataTableParameter, uploadedByParameter, fileNameParameter, successCount, uploadStatus);
                    break;

                case nameof(InterZonalPromotion):
                    dataTableParameter = new SqlParameter() { ParameterName = "@interZonalPromotionData", SqlDbType = SqlDbType.Structured, Value = dataTable, TypeName = "InterZonalPromotionTableType" };
                    result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC STP_InterZonalPromotion_BulkUpload @interZonalPromotionData,@uploadedBy,@fileName,@successCount OUTPUT, @status OUTPUT"
                   , dataTableParameter, uploadedByParameter, fileNameParameter, successCount, uploadStatus);
                    break;

                case nameof(InterZonalRequestTransfer):
                    dataTableParameter = new SqlParameter() { ParameterName = "@interZonalRequestTransferTableType", SqlDbType = SqlDbType.Structured, Value = dataTable, TypeName = "InterZonalRequestTransferTableType" };
                    result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC STP_InterZonalRequestTransfer_BulkUpload @interZonalRequestTransferTableType,@uploadedBy,@fileName,@successCount OUTPUT, @status OUTPUT"
                   , dataTableParameter, uploadedByParameter, fileNameParameter, successCount, uploadStatus);
                    break;

                case nameof(RegionMaster):
                    dataTableParameter = new SqlParameter() { ParameterName = "@regionMasterData", SqlDbType = SqlDbType.Structured, Value = dataTable, TypeName = "RegionMasterDataType" };
                    result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC STP_RegionMaster_BulkUpload @regionMasterData,@uploadedBy,@fileName,@successCount OUTPUT, @status OUTPUT"
                   , dataTableParameter, uploadedByParameter, fileNameParameter, successCount, uploadStatus);
                    break;

                case nameof(ZoneMaster):
                    dataTableParameter = new SqlParameter() { ParameterName = "@zoneMasterData", SqlDbType = SqlDbType.Structured, Value = dataTable, TypeName = "ZoneMasterDataType" };
                    result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC STP_ZoneMaster_BulkUpload @zoneMasterData,@uploadedBy,@fileName,@successCount OUTPUT, @status OUTPUT"
                   , dataTableParameter, uploadedByParameter, fileNameParameter, successCount, uploadStatus);
                    break;

                case nameof(DepartmentMaster):
                    dataTableParameter = new SqlParameter() { ParameterName = "@departmentMasterData", SqlDbType = SqlDbType.Structured, Value = dataTable, TypeName = "DepartmentMasterDataType" };
                    result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC STP_DepartmentMaster_BulkUpload @departmentMasterData,@uploadedBy,@fileName,@successCount OUTPUT, @status OUTPUT"
                   , dataTableParameter, uploadedByParameter, fileNameParameter, successCount, uploadStatus);
                    break;

                default: throw new ArgumentException("This file type is note present");
            }

            var successcount = Convert.ToInt32(successCount.Value);
            string status = Convert.ToString(uploadStatus.Value);

            return new ExcelUploadResult() { SuccessCount = successcount, UploadStatus = status };
        }

        public async Task<List<UploadHistoryDetails>> GetUploadHistoryList(int fileTypeCode)
        {
            var fileTypeCodeParameter = new SqlParameter() { ParameterName = "@fileTypeCode", SqlDbType = SqlDbType.Int,  Value = fileTypeCode };

            //var historyList =
            //     _dbContext.UploadHistoryDetails.ToList();
            var historyList =await _dbContext.Set<UploadHistoryDetails>().FromSqlRaw("EXEC STP_GetUploadHistoryDetails @fileTypeCode", fileTypeCodeParameter).ToListAsync();
            return historyList;
        }
    }
}

