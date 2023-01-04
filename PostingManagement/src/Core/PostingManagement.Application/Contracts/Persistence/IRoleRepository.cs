using PostingManagement.Domain.Entities;

namespace PostingManagement.Application.Contracts.Persistence
{
    public interface IRoleRepository
    {
        /// <summary>
        /// Add Role using Role Name
        /// </summary>
        /// <param name="roleName">Role Name to be added</param>
        /// <returns>true if added successfully</returns>
        public Task<bool> AddAsync(string roleName);
        /// <summary>
        /// Get Role using Role Name
        /// </summary>
        /// <param name="roleName">Role Name to get the Role</param>
        /// <returns>Returns Role Model which has Role Name and Role ID</returns>
        public Task<Role> GetRoleByName(string roleName);
        /// <summary>
        /// Get Role using Role Id
        /// </summary>
        /// <param name="roleId">Role ID to get the Role</param>
        /// <returns>Returns Role Model which has Role Name and Role ID</returns>
        public Task<Role> GetRoleById(int roleId);
        /// <summary>
        /// Delete Role using Role Model object
        /// </summary>
        /// <param name="role">Role object to be deleted</param>
        /// <returns></returns>
        public Task<bool> DeleteAsync(Role role);
        /// <summary>
        /// Get All Roles from the Role Table
        /// </summary>
        /// <returns>List of All the Roles</returns>
        public Task<List<Role>> GetAllRoles();
        /// <summary>
        /// to check if Role Already Exists using Role Name
        /// </summary>
        /// <param name="roleName"> Role Name to search in the table</param>
        /// <returns>true if Role Exixts; false if Role does not exist</returns>
        public Task<bool> IsRoleAlreadyExist(string roleName);
        /// <summary>
        /// Updates the existing role with new id and name
        /// </summary>
        /// <param name="roleId">role Id to be updated</param>
        /// <param name="roleName">role name to be updated</param>
        /// <returns></returns>
        public Task<bool> UpdateRole(int roleId, string roleName);

    }
}
