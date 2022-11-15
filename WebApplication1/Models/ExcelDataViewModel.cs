namespace PostingManagement.UI.Models
{
    public class ExcelDataViewModel<T> where T : class
    {
        public ExcelUploadResponseModel Response { get; set; }
        public List<T> RecordList { get; set; }
    }
}
