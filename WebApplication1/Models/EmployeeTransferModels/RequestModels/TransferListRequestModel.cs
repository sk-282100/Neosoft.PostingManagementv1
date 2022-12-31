namespace PostingManagement.UI.Models.EmployeeTransferModels.RequestModels
{
    public class TransferListRequestModel
    {
        public int PageNumber { get; set; }
        public int NumberOfRecords { get; set; }
        public string SortDirection { get; set; }
        public string SortColumn { get; set; }
        public string Search { get; set; }
    }
}
