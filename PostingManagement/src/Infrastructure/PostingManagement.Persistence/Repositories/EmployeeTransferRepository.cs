using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Persistence.Repositories
{
    public class EmployeeTransferRepository : IEmployeeTransferRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public EmployeeTransferRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<TransferListReponse> GetAllTransferListEmployees(int pageNumber, int numberOfRecords)
        {
            SqlParameter pageNumerParameter = new SqlParameter() { ParameterName = "@PageNumber", SqlDbType = SqlDbType.Int, Value = pageNumber };
            SqlParameter numberOfRecordsParameter = new SqlParameter() { ParameterName = "@NumberOfRecords", SqlDbType = SqlDbType.Int, Value = numberOfRecords };
            SqlParameter totalRecordsParameter = new SqlParameter() { ParameterName = "@TotalRecords", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            var result = await _dbContext.Set<TransferListVM>().FromSqlRaw("EXEC STP_ServerSideGetTransferDataAndPromotionData @PageNumber,@NumberOfRecords, @TotalRecords OUTPUT",
                pageNumerParameter, numberOfRecordsParameter, totalRecordsParameter).ToListAsync();
            int totalRecords = Convert.ToInt32(totalRecordsParameter.Value);
            return new TransferListReponse() { Data = result, TotalRecords = totalRecords };
        }

        public async Task<EmployeeDetailsForTransferList> GetEmployeeByIdAndMovementType(int employeeId, string movementType)
        {
            SqlParameter employeeIdParameter = new SqlParameter() { ParameterName = "@employeeId", SqlDbType = SqlDbType.Int, Value = employeeId };
            SqlParameter movementTypeParameter = new SqlParameter() { ParameterName = "@movementType", SqlDbType = SqlDbType.VarChar, Size = 30, Value = movementType };
            var result = await _dbContext.Set<EmployeeDetailsForTransferList>().FromSqlRaw("EXEC STP_GetAdditionalEmployeeTransferDataAndPromotionData @employeeId,@movementType", employeeIdParameter, movementTypeParameter).ToListAsync();
            return result.FirstOrDefault();
        }

        public async Task<List<TransferListVM>> GetTransferListEmployeesByEmployeeId(List<int> employeeIdList)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("Id", typeof(int)));

            // populate DataTable from your List here
            foreach (var id in employeeIdList)
            {
                dataTable.Rows.Add(id);
            }

            SqlParameter employeeIdListParameter = new SqlParameter() { ParameterName = "@employeeIdList", SqlDbType = SqlDbType.Structured, Value = dataTable, TypeName = "EmployeeIdList" }; 
            var result = await _dbContext.Set<TransferListVM>().FromSqlRaw("EXEC STP_GetTransferDataAndPromotionDataByEmployeeId @employeeIdList", employeeIdListParameter).ToListAsync();
            return result;
        }

        public ZOTransferListReponse InsertIntoTransferListForZo(List<TransferListVM> list)
        {
            DataTable dataTable = new DataTable(typeof(TransferListVM).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(TransferListVM).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (TransferListVM item in list)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            SqlParameter dataTableParameter = new SqlParameter() { ParameterName = "@transferListData", SqlDbType = SqlDbType.Structured, Value = dataTable, TypeName = "COTransferListTableType" };
            SqlParameter successCount = new SqlParameter() { ParameterName = "@successCount", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            SqlParameter uploadStatus = new SqlParameter() { ParameterName = "@status", SqlDbType = SqlDbType.VarChar, Size = 20, Direction = ParameterDirection.Output };

            var result = _dbContext.Database.ExecuteSqlRaw("EXEC STP_InsertIntoZOListTbl @transferListData, @successCount OUTPUT, @status OUTPUT",
            dataTableParameter, successCount, uploadStatus);
            int successcount = Convert.ToInt32(successCount.Value);
            string status = Convert.ToString(uploadStatus.Value);
            return new ZOTransferListReponse() { SuccessCount = successcount, Status = status };
        }
    }
}
