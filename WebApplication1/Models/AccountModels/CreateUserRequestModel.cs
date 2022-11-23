namespace PostingManagement.UI.Models.AccountModels
{
    public class CreateUserRequestModel
    {
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public string CreatedBy { get; set; }
    }
}
