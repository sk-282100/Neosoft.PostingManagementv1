using MediatR;
using PostingManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.JobFamily.Commands.DeleteJobFamily
{
    public class DeleteJobFamilyCommand : IRequest<Response<bool>>
    {
        public string JobFamilyId { get; set; } 
    }
}
