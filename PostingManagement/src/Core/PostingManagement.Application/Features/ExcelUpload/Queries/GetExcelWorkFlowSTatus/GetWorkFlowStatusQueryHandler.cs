using AutoMapper;
using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelWorkFlowSTatus
{
    public class GetWorkFlowStatusQueryHandler : IRequestHandler<GetWorkFlowStatusQuery, Response<GetWorkFlowStatusDto>>
    {
        private readonly IExcelUploadRepository _excelUploadRepository;
        private readonly IMapper _mapper;
        public GetWorkFlowStatusQueryHandler(IExcelUploadRepository excelUploadRepository,IMapper mapper)
        {
            _excelUploadRepository = excelUploadRepository;
            _mapper = mapper;
        }

        public async Task<Response<GetWorkFlowStatusDto>> Handle(GetWorkFlowStatusQuery request, CancellationToken cancellationToken)
        {
            var status = await _excelUploadRepository.GetWorkFlowStatus();
            GetWorkFlowStatusDto response = _mapper.Map<GetWorkFlowStatusDto>(status);
            return new Response<GetWorkFlowStatusDto>() { Succeeded = true ,Data =response};
        }
    }
}
