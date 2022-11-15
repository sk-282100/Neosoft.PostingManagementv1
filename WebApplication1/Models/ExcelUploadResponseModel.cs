namespace PostingManagement.UI.Models
{
    public class ExcelUploadResponseModel
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public ExcelUploadResult Data { get; set; }
    }
}
