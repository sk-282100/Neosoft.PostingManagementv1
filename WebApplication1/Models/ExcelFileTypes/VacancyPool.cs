using Microsoft.EntityFrameworkCore;

namespace PostingManagement.UI.Models.ExcelFileTypes
{
    [Keyless]
    public class VacancyPool
    {
        public int BatchId { get; set; }
        public string ZoneCode { get; set; }
        public string ZoneName { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public int NoOfVacancyZone { get; set; }
        public int NoOfVacancyRegion { get; set; }

    }
}
