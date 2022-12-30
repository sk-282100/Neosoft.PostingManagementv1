using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Domain.Entities
{
    public class UploadedRecordsRequestAPI
    {
        public int FileTypeCode { get; set; }
        public int BatchId { get; set; }
        public int PageNumber { get; set; }
        public int NumberOfRecords { get; set; }
        public string SortDirection { get; set; }
        public string SortColumn { get; set; }
    }
}
