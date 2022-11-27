namespace PostingManagement.UI.Models.AccountModels
{
    public class UpdateUserRoleRequestModel
    {
        public string UId { get; set; }
        public string UserName { get; set; }
        public string RoleId { get; set; }
        public string UpdatedBy { get; set; }
    }
}
