using AutoMapper;
using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Features.ExcelUpload.Command;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData.BaseGetExcelDataQueryHandler
{
    public abstract class BaseGetExcelDataQueryHandler<T> : IRequestHandler<GetExcelDataQuery<T>, Response<List<T>>> where T : class
    {
        private readonly IExcelUploadRepository _repository;
        private readonly IMapper _mapper;        
        public BaseGetExcelDataQueryHandler(IExcelUploadRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Response<List<T>>> Handle(GetExcelDataQuery<T> request, CancellationToken cancellationToken)
        {
            List<T> dataList = await _repository.GetAllRecords<T>(request.FileTypeCode, request.BatchId);
            //ExcelUploadResult excelUploadResult = await _repository.AddAsync(request.UploadedBy, request.ExcelDataList, request.FileName);
            //var result = _mapper.Map<T>(dataList);
            return new Response<List<T>>() { Succeeded = true, Data = dataList };
        }
    }
}
