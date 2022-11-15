using MediatR;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.ExcelUpload.Command
{
    public  class ExcelUploadCommand<T>:IRequest<Response<ExcelUploadDto>>
    {
        public string UploadedBy { get; set; }
        public string FileName { get;set; }
        public List<T> ExcelDataList { get; set; }        
    }
}
