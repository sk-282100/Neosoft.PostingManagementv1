using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Domain
{
    public class UploadedRecordsResponse<T>
    {
        public List<T> Data { get; set; }
        public int TotalRecords { get; set; }
    }
}
