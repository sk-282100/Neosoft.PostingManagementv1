using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Persistence.Repositories
{    
    public class LoginRepository : BaseRepository<LoginModel>, ILoginRepository
    {
        private readonly ApplicationDbContext _context;
        public LoginRepository(ApplicationDbContext dbContext, ILogger<LoginModel> logger) : base(dbContext, logger)
        {
            _context = dbContext;
        }
        public async Task<UserDetails> GetDetailsByUsername(string username)
        {
            var userDetails = await _context.UserDetailsTbl.FirstAsync(x => x.UserName == username && x.DeletedBy == null);
            return userDetails;
        }

        public async Task<string> GetRoleByid(int RoleId)
        {
            var result = await _context.RoleTable.FirstAsync(x => x.RoleId == RoleId);
            return result.RoleName;
        }
    }
}

