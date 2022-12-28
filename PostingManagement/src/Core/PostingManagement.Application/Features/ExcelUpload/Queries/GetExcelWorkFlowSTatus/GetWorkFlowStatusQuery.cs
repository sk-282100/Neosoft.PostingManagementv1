using MediatR;
using PostingManagement.Application.Responses;

namespace PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelWorkFlowSTatus
{
    public class GetWorkFlowStatusQuery :IRequest<Response<GetWorkFlowStatusDto>>
    {

    }
}
