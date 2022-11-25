using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Contracts.Persistence
{
    public interface IAccountRepository
    {
        public Task<bool> AddUser(string userName, int RoleId,string createdBy);
        public Task<bool> DeleteUser(int UserRoleId ,string deletedBy);
        public Task<bool> UpdateUser(int uId, string userName, int roleId, string updatedBy);
        public Task<List<UserDetailsVm>> GetAllUserDetails();
        public Task<UserDetails> GetUserDetailsById(int UserRoleId);
        public Task<bool> IsUserNamePresent(string userName);

    }
}
