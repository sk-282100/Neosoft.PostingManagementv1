using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.ExcelUpload.Queries.GetUploadHistoryList
{
    public class GetUploadHistoryDto
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
