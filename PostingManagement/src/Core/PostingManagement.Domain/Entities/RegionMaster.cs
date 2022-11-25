using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Domain.Entities
{
    public class RegionMaster
    {        
        public int BatchId { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RegionCode { get; set; }
        public string RegionName { get; set; } = null!;
        public int ZoneCode { get; set; }
        public string ZoneName { get; set; } = null!;
        public string State { get; set; } = null!;
        public int StateId { get; set; }
        public string District { get; set; } = null!;
        public int DistrictId { get; set; }
        public string City { get; set; } = null!;
        public int CityId { get; set; }
    }
}
