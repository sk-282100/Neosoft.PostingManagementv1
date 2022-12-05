using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        public async Task<List<TransferListVM>> GetAllTransferListEmployees()
        {
            var result = await _dbContext.Set<TransferListVM>().FromSqlRaw("EXEC STP_GetTransferDataAndPromotionData").ToListAsync();
            return result;
        }

        public async Task<EmployeeDetailsForTransferList> GetEmployeeByIdAndMovementType(int employeeId, string movementType)
        {
            SqlParameter employeeIdParameter = new SqlParameter() { ParameterName = "@employeeId", SqlDbType = SqlDbType.Int, Value = employeeId };
            SqlParameter movementTypeParameter = new SqlParameter() { ParameterName = "@movementType", SqlDbType = SqlDbType.VarChar, Size = 30, Value = movementType };
            var result = await _dbContext.Set<EmployeeDetailsForTransferList>().FromSqlRaw("EXEC STP_GetAdditionalEmployeeTransferDataAndPromotionData @employeeId,@movementType", employeeIdParameter, movementTypeParameter).ToListAsync();
            return result.FirstOrDefault();
        }
    }
}
