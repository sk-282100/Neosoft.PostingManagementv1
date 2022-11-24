using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Domain.Entities
{
    public class InterZonalRequestTransfer
    {
        public int BatchId { get; set; }
        [Key]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Designation { get; set; } = null!;
        public string Scale { get; set; } = null!;
        public string LocationName { get; set; } = null!;
        public string CurrentRo { get; set; } = null!;
        public string ZoneBeforeAmalgamation { get; set; } = null!;
        public DateTime ZoneDateBeforeAmalgamation { get; set; }
        public string TimeAwayFromZone { get; set; } = null!;
        public string ZoneAfterAmalgamation { get; set; } = null!;
        public DateTime ZoneDateAfterAmalgamation { get; set; }
        public string DirectPromotee { get; set; } = null!;
        public string TransferCategory { get; set; } = null!;
        public string TemporaryTransferMonth { get; set; } = null!;
        public string TransferSequenceNumber { get; set; } = null!;
        public string AppliedState { get; set; } = null!;
        public string AppliedZone { get; set; } = null!;
        public string AppliedRegion1 { get; set; } = null!;
        public string? AppliedRegion2 { get; set; }
        public string? AppliedRegion3 { get; set; }
        public string TransferReason { get; set; } = null!;
        public DateTime ApplicationDate { get; set; }
        public DateTime DiarisedDate { get; set; }
        public string Status { get; set; } = null!;
        public DateTime DateOfPromotion { get; set; }
        public DateTime DateOfReversion { get; set; }
        public string Disabled { get; set; } = null!;
        public DateTime? DateOfMarriage { get; set; }
        public string SpecialistCategory { get; set; } = null!;
        public string TransferType { get; set; } = null!;
        public string TemporaryTransferDetails { get; set; } = null!;
        public string AssetsAndLiabilitiesDetail { get; set; } = null!;
        public string StatusOfSubmissionofApproval { get; set; } = null!;
        public string? Comments { get; set; }
        public string RequestType { get; set; } = null!;
    }
}
