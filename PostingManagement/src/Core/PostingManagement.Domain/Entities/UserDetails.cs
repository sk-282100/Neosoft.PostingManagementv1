using System.ComponentModel.DataAnnotations;

namespace PostingManagement.Domain.Entities
{
    public class UserDetails
    {
        [Key]
        public int UId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string? Email { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
