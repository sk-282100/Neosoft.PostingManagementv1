using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Domain.Entities
{
    public class UploadHistoryDetails
    {
        [Key]
        public int HistoryId { get; set; }
        public string FileName { get; set; }
        public DateTime Date { get; set; }
        public int NumberOfRows { get; set; }
        public int InsertedRows { get; set; }
        public string UploadStatus { get; set; }
    }
}
