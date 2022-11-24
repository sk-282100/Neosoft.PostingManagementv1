using System.ComponentModel.DataAnnotations;

namespace PostingManagement.UI.Models
{
    public class UploadHistoryDetails
    {
 
        public int HistoryId { get; set; }
        [Display(Name = "Batch ID")]
        public int BatchId { get; set; }        
        [Display(Name ="File Name")]
        public string? FileName { get; set; }
        [Display(Name = "Uploaded Date")]
        public DateTime Date { get; set; }
        [Display(Name = "Number Of Rows")]
        public int NumberOfRows { get; set; }
        [Display(Name = "Inserted Rows")]
        public int InsertedRows { get; set; }
        [Display(Name = "Status")]
        public string UploadStatus { get; set; }
    }
}
