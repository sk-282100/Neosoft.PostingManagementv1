using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Domain.Entities
{
    [Keyless]
    public class MatchingRequestTransferVacancy
    {
        public string ZoneCode { get; set; }
        public string ZoneName { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public int Vacancy { get; set; }
        public int SelectedEmployeesCount { get; set; }
        public bool MatchedUnMatchedVacancy { get; set; }
    }
}
