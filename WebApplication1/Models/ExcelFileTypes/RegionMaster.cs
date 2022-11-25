using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.UI.Models.ExcelFileTypes
{
    public class RegionMaster
    {
        public int BatchId { get; set; }
        [Key]
        public string RegionCode { get; set; }
        public string RegionName { get; set; } = null!;
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
