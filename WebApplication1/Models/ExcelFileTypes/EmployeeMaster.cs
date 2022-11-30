using System.ComponentModel.DataAnnotations;

namespace PostingManagement.UI.Models.ExcelFileTypes
{
    public class EmployeeMaster
    {
        public int BatchId { get; set; }
        [Key]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string LocationDesired { get; set; } = null!;
        public string DepartmentRo { get; set; } = null!;
        public string RegionCode { get; set; } = null!;
        public string RegionName { get; set; } = null!;
        public string ZoneCode { get; set; } = null!;
        public string ZoneName { get; set; } = null!;
        public string JobCode { get; set; } = null!;
        public string Designation { get; set; } = null!;
        public DateTime RoleStartDate { get; set; }
        public string ScaleCode { get; set; } = null!;
        public string Scale { get; set; } = null!;
        public string UbijobRole { get; set; } = null!;
        public string BankName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string Sex { get; set; } = null!;
        public string Disability { get; set; } = null!;
        public DateTime LocationStartDate { get; set; }
        public string DomicileZone { get; set; } = null!;
        public DateTime RostartDate { get; set; }
        public DateTime LastPromotionDate { get; set; }
        public DateTime ZostartDate { get; set; }
    }
}
