namespace PostingManagement.UI.Models
{
    public class ExcelUploadRequest<T> where T : class 
    {
        public string FileName { get; set; }
        public List<T> FileData { get; set; }
    }
}
