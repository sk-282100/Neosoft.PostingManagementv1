using MediatR;
using PostingManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.JobFamily.Queries.GetJobFamilyById
{
    public class GetJobFamilyByIdQuery :IRequest<Response<GetjobFamilyByIdDto>>
    {
        public string JobFamilyId { get; set; }
    }
}
