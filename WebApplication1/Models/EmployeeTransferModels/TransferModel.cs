using System.ComponentModel;

namespace PostingManagement.UI.Models.EmployeeTransferModels
{
    public class TransferModel
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string ScaleName { get; set; }
        public int Scale { get; set; }
        public string Designation { get; set; }
        public string Region { get; set; }
        public string Zone { get; set; }
        public string MovementType { get; set; }
        [DisplayName("De(Select)")]
        public bool Select { get; set; }

    }



}
