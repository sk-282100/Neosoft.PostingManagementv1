using MediatR;
using PostingManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData
{
    public class GetExcelDataQuery<T> : IRequest<Response<List<T>>> where T : class
    {
        public int FileTypeCode { get; set; }
        public int BatchId { get; set; }
    }
}
