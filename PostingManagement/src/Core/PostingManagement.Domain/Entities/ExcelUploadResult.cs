using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Domain.Entities
{
    public class ExcelUploadResult
    {
        public int SuccessCount { get; set; }
        public string UploadStatus { get; set; }
        public int BatchId { get; set; }
    }
}
