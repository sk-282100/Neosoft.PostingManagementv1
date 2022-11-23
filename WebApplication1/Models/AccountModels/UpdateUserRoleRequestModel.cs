namespace PostingManagement.UI.Models.AccountModels
{
    public class UpdateUserRoleRequestModel
    {
        public int UId { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public string UpdatedBy { get; set; }
    }
}
