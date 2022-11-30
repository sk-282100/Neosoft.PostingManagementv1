using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Helper.Constants
{
    public enum ExcelFileType : int
    {
        BranchMaster = 1,
        DepartmentMaster = 2,
        EmployeeMaster = 3,
        InterRegionPromotion = 4,
        InterRegionRequestTransfer = 5,
        InterZonalPromotion = 6,
        InterZonalRequestTranfer = 7,
        RegionMaster = 8,
        ZoneMaster = 9

    }
}
