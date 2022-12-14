using MediatR;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.TransferList.Commands.InsertIntoTransferListForZO
{
    public class TransferListForZOCommand:IRequest<Response<ZOTransferListReponse>>
    {
        public List<TransferListVM> TransferList { get; set; }
    }
}
