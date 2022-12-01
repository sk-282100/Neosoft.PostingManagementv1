using System.ComponentModel.DataAnnotations;

namespace PostingManagement.Domain.Entities
{
    public class DepartmentMaster
    {        
        [Key]
        public int DepartmentCode { get; set; }
        public string DepartmentName { get; set; } = null!;
    }
}
