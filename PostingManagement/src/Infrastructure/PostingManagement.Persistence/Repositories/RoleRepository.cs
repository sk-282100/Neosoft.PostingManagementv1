using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Domain.Entities;

namespace PostingManagement.Persistence.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger _logger;
        public RoleRepository(ApplicationDbContext applicationDbContext, ILogger<Role> logger) : base(applicationDbContext, logger)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
        }                
        public async Task<bool> AddAsync(string roleName)
        {
            Role role = new Role { RoleName = roleName };
            //var r = _applicationDbContext.RoleTable.Add(role);
            _applicationDbContext.RoleTable.Add(role);
            return await _applicationDbContext.SaveChangesAsync() == 1? true : false;            
        }

        public async Task<bool> DeleteAsync(Role role)
        {
            _applicationDbContext.RoleTable.Remove(role);
            return await _applicationDbContext.SaveChangesAsync() == 1 ? true : false;
        }

        public async Task<List<Role>> GetAllRoles()
        {
            return await _applicationDbContext.RoleTable.ToListAsync();
        }

        public async Task<Role> GetRoleById(int roleId)
        {
            return await _applicationDbContext.RoleTable.Where(x => x.RoleId == roleId).FirstOrDefaultAsync();
        }

        public async Task<Role> GetRoleByName(string roleName)
        {
            return await _applicationDbContext.RoleTable.Where(x => x.RoleName == roleName).FirstOrDefaultAsync();
        }
    }
}
