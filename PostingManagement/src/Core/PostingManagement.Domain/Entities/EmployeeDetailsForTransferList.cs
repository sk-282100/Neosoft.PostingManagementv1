using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Domain.Entities
{
    [Keyless]
    public class EmployeeDetailsForTransferList
    {
        public int EmployeeId { get; set; }
        public int Age { get; set; }
        public string Gender { get; set;}
        public string JobRole { get; set; }
        public string Disability { get; set; }
        public string TransferType { get; set; }
        public string? TransferReason { get; set; } = String.Empty;
        public string CurrentRole { get; set; }
        public string CurrentRegion { get; set; }
        public string CurrentZone { get; set; }
        public string ZonePreference1 { get; set; }
        public string Zone1RegionPreference1 { get; set; }
        public string Zone1RegionPreference2 { get; set; } = String.Empty;
        public string Zone1RegionPreference3 { get; set; } = String.Empty;
        public string ZonePreference2 { get; set; } = String.Empty;
        public string Zone2RegionPreference1 { get; set; } = String.Empty;
        public string Zone2RegionPreference2 { get; set; } = String.Empty;
        public string Zone2RegionPreference3 { get; set; } = String.Empty;
        public string ZonePreference3 { get; set; } = String.Empty;
        public string Zone3RegionPreference1 { get; set; } = String.Empty;
        public string Zone3RegionPreference2 { get; set; } = String.Empty;
        public string Zone3RegionPreference3 { get; set; } = String.Empty;
        public string ZonePreference4 { get; set; } = String.Empty;
        public string Zone4RegionPreference1 { get; set; } = String.Empty;
        public string Zone4RegionPreference2 { get; set; } = String.Empty;
        public string Zone4RegionPreference3 { get; set; } = String.Empty;
        public string ZonePreference5 { get; set; } = String.Empty;
        public string Zone5RegionPreference1 { get; set; } = String.Empty;
        public string Zone5RegionPreference2 { get; set; } = String.Empty;
        public string Zone5RegionPreference3 { get; set; } = String.Empty;
    }
}
