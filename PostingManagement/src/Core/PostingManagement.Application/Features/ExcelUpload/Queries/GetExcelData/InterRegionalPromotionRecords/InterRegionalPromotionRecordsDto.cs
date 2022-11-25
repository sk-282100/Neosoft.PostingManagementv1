using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData.InterRegionalPromotionRecords
{
    public class InterRegionalPromotionRecordsDto
    {
        public int BatchId { get; set; }
        [Key]
        public int EmployeeId { get; set; }
        public string CurrentScale { get; set; } = null!;
        public string PromotedScale { get; set; } = null!;
        public string Action { get; set; } = null!;
        public string Effdt { get; set; } = null!;
        public string PromotionType { get; set; } = null!;
    }
}
