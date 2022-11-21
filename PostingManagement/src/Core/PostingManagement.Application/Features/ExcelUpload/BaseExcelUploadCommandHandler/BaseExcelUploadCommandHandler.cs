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

namespace PostingManagement.Application.Features.ExcelUpload.BaseExcelUploadCommandHandler
{
    public abstract class BaseExcelUploadCommandHandler<T> : IRequestHandler<ExcelUploadCommand<T>, Response<ExcelUploadDto>>
    {
        private readonly IExcelUploadRepository _repository;
        private readonly IMapper _mapper;
        public BaseExcelUploadCommandHandler(IExcelUploadRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<ExcelUploadDto>> Handle(ExcelUploadCommand<T> request, CancellationToken cancellationToken)
        {
            ExcelUploadResult excelUploadResult = await _repository.AddAsync(request.UploadedBy, request.ExcelDataList, request.FileName);
            var result = _mapper.Map<ExcelUploadDto>(excelUploadResult);
            return new Response<ExcelUploadDto>() { Succeeded=true,Data=result};
        }

    }
}
