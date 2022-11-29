using System.ComponentModel.DataAnnotations;

namespace PostingManagement.UI.Models.ExcelFileTypes
{
    public class DepartmentMaster
    {
        public int BatchId { get; set; }
        [Key]
        public int DepartmentCode { get; set; }
        public string DepartmentName { get; set; } = null!;
    }
}
