namespace PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelWorkFlowSTatus
{
    public class GetWorkFlowStatusDto
    {
        public string EmployeeTransferListStatus { get; set; }
        public string VacancyListStatus { get; set; }
        public string InterZonalRequestStatus { get; set; }
        public string InterZonalPromotionStatus { get; set; }
    }
}
