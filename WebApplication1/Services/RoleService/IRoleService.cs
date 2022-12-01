using PostingManagement.UI.Models;
using PostingManagement.UI.Models.Responses;

namespace PostingManagement.UI.Services.RoleService
{
    public interface IRoleService
    {
        /// <summary>
        /// call API for add the new Role
        /// </summary>
        /// <returns>Response object containing the success status</returns>
        public Task<Response<bool>> AddRole(RoleModel roleModel);

        /// <summary>
        /// Call the API for Geting All role Records
        /// </summary>
        /// <returns>List of RoleModel Objects</returns>
        public Task<List<RoleModel>> GetAllRoles();

        /// <summary>
        /// Call the API for Remove the role 
        /// </summary>
        /// <returns>Response object containing the Remove status in bool</returns>
        public Task<Response<bool>> RemoveRole(string id);

        /// <summary>
        /// Call the API for Update the role 
        /// </summary>
        /// <returns>Response object containing the Update status in bool</returns>
        public Task<Response<bool>> EditRole(RoleModel roleModel);

        /// <summary>
        /// Call the API for Get the Role of respective Role Id 
        /// </summary>
        /// <returns>Response object containing the success status in bool</returns>
        public Task<Response<RoleModel>>GetRoleById(string id);

        /// <summary>
        /// Call the API for checking the respective Role is Present or not
        /// </summary>
        /// <returns>Response object containing the check status in bool</returns>
        public Task<Response<bool>> IsRoleAlreadyExist(string roleName);   
    }
}
