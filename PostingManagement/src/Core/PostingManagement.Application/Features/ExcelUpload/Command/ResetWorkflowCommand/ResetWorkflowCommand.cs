using MediatR;
using PostingManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.ExcelUpload.Command.ResetWorkflowCommand
{
    public class ResetWorkflowCommand:IRequest<Response<bool>>
    {
    }
}
