using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Domain.Entities
{
    public class InterRegionalPromotion
    {
        public int EmployeeId { get; set; }
        public string CurrentScale { get; set; } = null!;
        public string PromotedScale { get; set; } = null!;
        public string Action { get; set; } = null!;
        public string Effdt { get; set; } = null!;
        public string PromotionType { get; set; } = null!;
    }
}
