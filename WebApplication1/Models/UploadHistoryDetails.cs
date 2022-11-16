namespace PostingManagement.UI.Models
{
    public class UploadHistoryDetails
    {
        public int HistoryId { get; set; }
        public string FileName { get; set; }
        public DateTime Date { get; set; }
        public int NumberOfRows { get; set; }
        public int InsertedRows { get; set; }
        public string UploadStatus { get; set; }
    }
}
