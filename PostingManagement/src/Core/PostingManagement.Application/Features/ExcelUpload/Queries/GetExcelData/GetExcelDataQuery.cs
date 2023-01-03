using MediatR;
using PostingManagement.Application.Responses;
using PostingManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData
{
    public class GetExcelDataQuery<T> : IRequest<string> where T : class
    {
        public int FileTypeCode { get; set; }
        public int BatchId { get; set; }
        public int PageNumber { get; set; }
        public int NumberOfRecords { get; set; }
        public string SortDirection { get; set; }
        public string SortColumn { get; set; }
        public string Search { get; set; }
    }
}
