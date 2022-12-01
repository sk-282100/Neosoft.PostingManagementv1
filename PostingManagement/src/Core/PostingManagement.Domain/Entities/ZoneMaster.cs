using System.ComponentModel.DataAnnotations;

namespace PostingManagement.Domain.Entities
{
    public class ZoneMaster
    {        
        [Key]
        public string ZoneCode { get; set; }
        public string ZoneName { get; set; } = null!;
        public string State { get; set; } = null!;
        public int StateId { get; set; }
        public string District { get; set; } = null!;
        public int DistrictId { get; set; }
        public string City { get; set; } = null!;
        public int CityId { get; set; }
    }
}
