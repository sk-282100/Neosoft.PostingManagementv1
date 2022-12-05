using MediatR;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.TransferList.Queries.GetEmployeeDetailsById
{
    public class GetEmployeeDetailsByIdQuery : IRequest<Response<EmployeeDetailsForTransferList>>
    {
        public int EmployeeId { get; set; }
        public string MovementType { get; set; }
    }
}
