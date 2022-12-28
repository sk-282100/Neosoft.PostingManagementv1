using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Domain.Entities
{
    public class TriggerVm
    {
        public int TriggerId { get; set; }
        public string ScaleName { get; set; }
        public int Tenure { get; set; }
        public string Mandatory { get; set; }
    }
}
