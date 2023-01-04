using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Domain.Entities;

namespace PostingManagement.Persistence.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger _logger;
        public RoleRepository(ApplicationDbContext applicationDbContext, ILogger<Role> logger)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
        }                
        public async Task<bool> AddAsync(string roleName)
        {
            Role role = new Role { RoleName = roleName };
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
            return await _applicationDbContext.RoleTable.OrderByDescending(x=>x.RoleId).ToListAsync();
        }

        public async Task<Role> GetRoleById(int roleId)
        {
            return await _applicationDbContext.RoleTable.Where(x => x.RoleId == roleId).FirstOrDefaultAsync();
        }

        public async Task<Role> GetRoleByName(string roleName)
        {
            return await _applicationDbContext.RoleTable.Where(x => x.RoleName == roleName).FirstOrDefaultAsync();
        }
        public async Task<bool> IsRoleAlreadyExist(string roleName)
        {
            return await _applicationDbContext.RoleTable.AnyAsync(x => x.RoleName == roleName);
        }
        public async Task<bool> UpdateRole(int roleId, string roleName)
        {
            var role = await _applicationDbContext.RoleTable.FirstOrDefaultAsync(x => x.RoleId == roleId);
            role.RoleId = roleId;
            role.RoleName = roleName;            
            //Update
            _applicationDbContext.Entry(role).State = EntityState.Modified;
            return _applicationDbContext.SaveChanges() == 1 ? true : false;
        }
    }
}
