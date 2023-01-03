namespace PostingManagement.UI.Models
{
    public class UploadedRecordsRequest
    {
        public int FileTypeCode { get; set; }
        public int BatchId { get; set; }
        public int PageNumber { get; set; }
        public int NumberOfRecords { get; set; }
        public string SortDirection { get; set; }
        public string SortColumn { get; set; }
        public string Search { get; set; }
    }
}
