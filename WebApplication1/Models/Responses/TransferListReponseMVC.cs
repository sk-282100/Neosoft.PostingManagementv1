using PostingManagement.UI.Models.EmployeeTransferModels;

namespace PostingManagement.UI.Models.Responses
{
    public class TransferListReponseMVC
    {
        public List<EmployeeTransferModel> Data { get; set; }
        public int TotalRecords { get; set; }
    }
}
