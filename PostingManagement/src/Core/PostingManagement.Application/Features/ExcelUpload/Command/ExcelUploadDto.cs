using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.ExcelUpload.Command
{
    public class ExcelUploadDto
    {
        public int SuccessCount { get; set; }
        public string UploadStatus { get; set; }
    }
}
