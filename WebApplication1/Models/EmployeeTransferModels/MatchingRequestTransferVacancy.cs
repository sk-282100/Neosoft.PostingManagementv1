using Microsoft.EntityFrameworkCore;

namespace PostingManagement.UI.Models.EmployeeTransferModels
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
