using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData.InterRegionalRequestRecords
{
    public class InterRegionalRequestRecordsDto
    {
        public int BatchId { get; set; }
        [Key]
        public int EmployeeId { get; set; }
        public string TranferSequenceNo { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string DirectPromotee { get; set; } = null!;
        public string Scale { get; set; } = null!;
        public string TransferType { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Designation { get; set; } = null!;
        public string LocationName { get; set; } = null!;
        public string CurrentRo { get; set; } = null!;
        public string FromZone { get; set; } = null!;
        public string RequiredState { get; set; } = null!;
        public string AppliedZone { get; set; } = null!;
        public DateTime DateOfJoining { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string RegionBeforeAmalgamation { get; set; } = null!;
        public DateTime RegionDateBeforeAmalgamation { get; set; }
        public string TimeSpentAwayFromRegion { get; set; } = null!;
        public string RegionAfterAmalgamation { get; set; } = null!;
        public DateTime RegionDateAfterAmalgamation { get; set; }
        public string AppliedRegion1 { get; set; } = null!;
        public string? AppliedRegion2 { get; set; }
        public string? AppliedRegion3 { get; set; }
        public DateTime DateOfPromotionToPresentDate { get; set; }
        public DateTime DateOfReversion { get; set; }
        public DateTime DateOfMarriage { get; set; }
        public string TemporaryTransferDetails { get; set; } = null!;
        public string AssetAndLiablitiesDetails { get; set; } = null!;
        public string StatusOfSubmission { get; set; } = null!;
        public string Comments { get; set; } = null!;
        public string RequestType { get; set; } = null!;
        public string TransferReason { get; set; } = null!;
    }
}
