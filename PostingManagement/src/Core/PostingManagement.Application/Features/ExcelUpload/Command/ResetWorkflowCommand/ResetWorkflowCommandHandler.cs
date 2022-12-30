using MediatR;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.ExcelUpload.Command.ResetWorkflowCommand
{
    public class ResetWorkflowCommandHandler : IRequestHandler<ResetWorkflowCommand, Response<bool>>
    {
        private readonly IExcelUploadRepository _excelUploadRepository;
        public ResetWorkflowCommandHandler(IExcelUploadRepository excelUploadRepository)
        {
            _excelUploadRepository = excelUploadRepository;
        }

        public async Task<Response<bool>> Handle(ResetWorkflowCommand request, CancellationToken cancellationToken)
        {
            var result = await _excelUploadRepository.ResetWorkflow();
            return new Response<bool>() { Succeeded = true, Data = result };
        }
    }
}
