using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Domain.Entities
{
    public class DepartmentMaster
    {
        public int DepartmentCode { get; set; }
        public string DepartmentName { get; set; } = null!;
    }
}
