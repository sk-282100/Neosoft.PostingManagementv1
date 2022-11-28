using PostingManagement.UI.Models;
using PostingManagement.UI.Models.Responses;

namespace PostingManagement.UI.Services.RoleService
{
    public interface IRoleService
    {
        public Task<Response<bool>> AddRole(RoleModel roleModel);
        public Task<List<RoleModel>> GetAllRoles();
        public Task<Response<bool>> RemoveRole(string id);
        //public Task<RoleModel> GetRoleById(int id);
        public Task<Response<bool>> EditRole(RoleModel roleModel);
        public Task<Response<RoleModel>>GetRoleById(string id);
        public Task<Response<bool>> IsRoleAlreadyExist(string roleName);   
    }
}
