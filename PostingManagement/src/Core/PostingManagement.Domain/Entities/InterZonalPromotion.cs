using System.ComponentModel.DataAnnotations;

namespace PostingManagement.Domain.Entities
{
    public class InterZonalPromotion
    {        
        [Key]
        public int EmployeeId { get; set; }
        public string ZonePreference1 { get; set; } = null!;
        public string Zone1RegionPreference1 { get; set; } = null!;
        public string? Zone1RegionPreference2 { get; set; }
        public string? Zone1RegionPreference3 { get; set; }
        public string? ZonePreference2 { get; set; }
        public string? Zone2RegionPreference1 { get; set; }
        public string? Zone2RegionPreference2 { get; set; }
        public string? Zone2RegionPreference3 { get; set; }
        public string? ZonePreference3 { get; set; }
        public string? Zone3RegionPreference1 { get; set; }
        public string? Zone3RegionPreference2 { get; set; }
        public string? Zone3RegionPreference3 { get; set; }
        public string? ZonePreference4 { get; set; }
        public string? Zone4RegionPreference1 { get; set; }
        public string? Zone4RegionPreference2 { get; set; }
        public string? Zone4RegionPreference3 { get; set; }
        public string? ZonePreference5 { get; set; }
        public string? Zone5RegionPreference1 { get; set; }
        public string? Zone5RegionPreference2 { get; set; }
        public string? Zone5RegionPreference3 { get; set; }
    }
}
