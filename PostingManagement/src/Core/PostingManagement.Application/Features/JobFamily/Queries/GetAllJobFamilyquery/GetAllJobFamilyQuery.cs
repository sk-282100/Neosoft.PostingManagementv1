using MediatR;
using PostingManagement.Application.Features.Roles.Queries.GetAllRoles;
using PostingManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.JobFamily.Queries.GetAllJobFamilyquery
{
    public class GetAllJobFamilyQuery : IRequest<Response<List<GetAllJobFamilyDto>>>
    {
    }
}
