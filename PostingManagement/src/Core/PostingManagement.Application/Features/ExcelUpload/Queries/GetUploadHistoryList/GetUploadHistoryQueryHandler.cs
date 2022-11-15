using AutoMapper;
using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.ExcelUpload.Queries.GetUploadHistoryList
{
    public class GetUploadHistoryQueryHandler : IRequestHandler< GetUploadHistoryQuery, Response<List<GetUploadHistoryDto>>>
    {
        private readonly IExcelUploadRepository _repository;
        private readonly IMapper _mapper;

        public GetUploadHistoryQueryHandler(IExcelUploadRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<List<GetUploadHistoryDto>>> Handle(GetUploadHistoryQuery request, CancellationToken cancellationToken)
        {
            var historyList = await _repository.GetUploadHistoryList( request.FileTypeCode);
            var responseData = _mapper.Map<List<GetUploadHistoryDto>>(historyList);
            
            return new Response<List<GetUploadHistoryDto>>() { Data=responseData ,Succeeded=true};
        }
    }
}
