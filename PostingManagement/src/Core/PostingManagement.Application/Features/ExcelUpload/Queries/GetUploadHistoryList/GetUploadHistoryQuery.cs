using MediatR;
using PostingManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.ExcelUpload.Queries.GetUploadHistoryList
{
    public class GetUploadHistoryQuery : IRequest<Response<List<GetUploadHistoryDto>>>
    {
      
        public int FileTypeCode { get; set; }
    }
}
