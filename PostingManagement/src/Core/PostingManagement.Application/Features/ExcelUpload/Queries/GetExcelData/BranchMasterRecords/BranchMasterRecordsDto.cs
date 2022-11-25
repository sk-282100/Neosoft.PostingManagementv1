using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData.BranchMasterRecords
{
    public class BranchMasterRecordsDto
    {
        public int BatchId { get; set; }
        public string OldBranchCode { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string State { get; set; }
        public int StateId { get; set; }
        public string District { get; set; }
        public int DistrictId { get; set; }
        public string City { get; set; }
        public int CityId { get; set; }
        public string Area { get; set; }
        public DateTime DateOfOpening { get; set; }
        public string ZoneName { get; set; }
        public string ZoneCode { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public string BranchType { get; set; }
        public string BankName { get; set; }

        [Column(TypeName = "Varchar(50)")]
        public string AdministrativeFlag { get; set; }
    }
}
