using AutoMapper;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData.BaseGetExcelDataQueryHandler;
using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData.EmployeeMasterRecords
{
    public class GetEmployeeMasterDataQueryHandler : BaseGetExcelDataQueryHandler<EmployeeMaster>
    {
        public GetEmployeeMasterDataQueryHandler(IExcelUploadRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
