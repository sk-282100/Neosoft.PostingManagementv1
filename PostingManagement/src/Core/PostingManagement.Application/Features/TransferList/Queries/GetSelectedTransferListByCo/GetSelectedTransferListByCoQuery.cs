using MediatR;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.TransferList.Queries.GetSelectedTransferListByCo
{
    public class GetSelectedTransferListByCoQuery: IRequest<Response<List<TransferListVM>>>
    {
        public List<int> EmployeeIdList { get; set; }
    }
}
