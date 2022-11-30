using PostingManagement.Domain.Entities;

namespace PostingManagement.Application.Contracts.Persistence
{
    public interface ILoginRepository
    {
        /// <summary>
        /// Get User Details using Username
        /// </summary>
        /// <param name="username">username to get the details</param>
        /// <returns>Returns UserDetails Modek which contains all the details of the user</returns>
        public Task<UserDetails> GetDetailsByUsername(string username);
        /// <summary>
        /// Get Role using Role Id
        /// </summary>
        /// <param name="RoleId">Role ID to get the Role</param>
        /// <returns>Returns Role Name if found</returns>
        public Task<string> GetRoleByid(int RoleId);
    }
}
