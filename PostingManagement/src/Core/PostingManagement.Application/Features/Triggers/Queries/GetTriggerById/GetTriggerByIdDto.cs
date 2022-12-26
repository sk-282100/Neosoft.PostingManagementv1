namespace PostingManagement.Application.Features.Triggers.Queries.GetTriggerById
{
    public class GetTriggerByIdDto
    {
        public string TriggerId { get; set; }
        public string ScaleId { get; set; }
        public int Tenure { get; set; }
        public string Mandatory { get; set; }
    }
}
