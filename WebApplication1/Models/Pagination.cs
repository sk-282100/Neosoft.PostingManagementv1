using PostingManagement.UI.Models.EmployeeTransferModels;

namespace PostingManagement.UI.Models
{
    public class Pagination
    {
        public DatatablePostData data { get; set; }
    }
    public class DatatablePostData
    {
        public List<Column> columns { get; set; }
        public int draw { get; set; }
        public int length { get; set; }
        public List<Order> order { get; set; }
        public Search search { get; set; }
        public int start { get; set; }
    }
    public class Column
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public Search search { get; set; }
    }
    public class Order
    {
        public int column { get; set; }
        public string dir { get; set; }
    }
    public class Search
    {
        public string value { get; set; }
        public bool regex { get; set; }
    }
    public class DTResponse
    {
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<EmployeeTransferModel> data { get; set; }
    }
}
