using System.ComponentModel.DataAnnotations;

namespace PostingManagement.UI.Models.ExcelFileTypes
{
    public class InterRegionalPromotion
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
