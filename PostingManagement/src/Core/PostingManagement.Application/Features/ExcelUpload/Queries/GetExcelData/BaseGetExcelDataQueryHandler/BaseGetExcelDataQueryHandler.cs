using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Features.ExcelUpload.Command;
using PostingManagement.Application.Responses;
using PostingManagement.Domain;
using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData.BaseGetExcelDataQueryHandler
{
    public abstract class BaseGetExcelDataQueryHandler<T> : IRequestHandler<GetExcelDataQuery<T>, string> where T : class
    {
        private readonly IExcelUploadRepository _repository;
        private readonly IMapper _mapper;        
        public BaseGetExcelDataQueryHandler(IExcelUploadRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<string> Handle(GetExcelDataQuery<T> request, CancellationToken cancellationToken)
        {            
            var dataList = await _repository.GetAllRecords<T>(request);
            return dataList;
        }        
    }
}
