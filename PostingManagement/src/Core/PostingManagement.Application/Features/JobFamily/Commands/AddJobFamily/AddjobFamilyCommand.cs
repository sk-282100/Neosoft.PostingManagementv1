using MediatR;
using PostingManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.JobFamily.Commands.AddJobFamily
{
    public class AddjobFamilyCommand :IRequest<Response<bool>>
    {
        public string JobFamilyName { get; set; }
    }
}
