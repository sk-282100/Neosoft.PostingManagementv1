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
        public string TransferReason { get; set; }
        public string Zone { get; set; }
        public string Region1 { get; set; }
        public string Region2 { get; set; }
        public string Region3 { get; set; }
    }
}
