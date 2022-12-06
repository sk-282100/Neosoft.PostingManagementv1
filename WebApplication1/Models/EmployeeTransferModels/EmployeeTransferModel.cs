using System.ComponentModel;

namespace PostingManagement.UI.Models.EmployeeTransferModels
{
    public class EmployeeTransferModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string ScaleCode { get; set; }
        public string Scale { get; set; }
        public string UbijobRole { get; set; }
        public string RegionName { get; set; }
        public string ZoneName { get; set; }
        public string MovementType { get; set; }
        [DisplayName("De(Select)")]
        public bool Select { get; set; }

    }



}
