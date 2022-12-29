using PostingManagement.UI.Models.EmployeeTransferModels;
using PostingManagement.UI.Models.ExcelFileTypes;

namespace PostingManagement.UI.Models.Responses
{
    public class ViewRecordsResponseMVC<T>
    {
        public List<T> Data { get; set; }
        public int TotalRecords { get; set; }
    }
}
