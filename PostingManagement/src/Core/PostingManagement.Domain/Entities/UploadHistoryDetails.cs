using System.ComponentModel.DataAnnotations;

namespace PostingManagement.Domain.Entities
{
    public class UploadHistoryDetails
    {
        [Key]
        public int HistoryId { get; set; }
        public int BatchId { get; set; }
        public string FileName { get; set; }
        public DateTime Date { get; set; }
        public int NumberOfRows { get; set; }
        public int InsertedRows { get; set; }
        public string UploadStatus { get; set; }
        public string? ReasonOfFailure { get; set; }
    }
}
