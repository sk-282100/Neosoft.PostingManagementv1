using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Contracts.Persistence
{
    public interface IRoleRepository : IAsyncRepository<Role>
    {
        public Task<bool> AddAsync(string roleName);
        public Task<Role> GetRoleByName(string roleName);
        public Task<Role> GetRoleById (int roleId);
        public Task<bool> DeleteAsync(Role role);
        public Task<List<Role>> GetAllRoles();
        public Task<bool> IsRoleAlreadyExist(string roleName);
    }
}
