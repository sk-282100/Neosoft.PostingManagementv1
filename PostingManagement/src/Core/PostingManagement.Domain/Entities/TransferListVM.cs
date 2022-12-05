using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Domain.Entities
{
    [Keyless]
    public class TransferListVM
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string ScaleCode { get; set; }
        public string Scale { get; set; }
        public string UbijobRole { get; set; }
        public string RegionName { get; set; }
        public string ZoneName { get; set; }
        public string MovementType { get; set; }
    }
}
