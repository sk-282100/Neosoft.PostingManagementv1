using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostingManagement.Application.Features.ExcelUpload.Command;
using PostingManagement.Application.Features.TransferList.Commands.InsertIntoTransferListForZO;
using PostingManagement.Application.Features.TransferList.Queries.GetEmployeeDetailsById;
using PostingManagement.Application.Features.TransferList.Queries.GetMatchRequestTransferList;
using PostingManagement.Application.Features.TransferList.Queries.GetSelectedTransferListByCo;
using PostingManagement.Application.Features.TransferList.Queries.GetTransferList;
using PostingManagement.Domain.Entities;

namespace PostingManagement.Api.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TransferListController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransferListController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetTransferList")]
        public async Task<IActionResult> GetTransferList(int pageNumber, int numberOfRecords)
        {
            var request = new GetTransferListQuery() { PageNumber = pageNumber, NumberOfRecords = numberOfRecords };
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("GetAdditionalEmployeeDetails")]
        public async Task<IActionResult> GetAdditionalEmployeeDetails(int employeeId, string movementType)
        {
            var request = new GetEmployeeDetailsByIdQuery() { EmployeeId = employeeId, MovementType = movementType};
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("GetTransferListByEmployeeId")]
        public async Task<IActionResult> GetTransferListByEmployeeId(List<int> employeeIdList)
        {
            var request = new GetSelectedTransferListByCoQuery() {EmployeeIdList = employeeIdList };
            var response = await _mediator.Send(request);
            return Ok(response);
        }


        [HttpPost("TransferListForZO")]
        public async Task<IActionResult> TransferListForZO(List<TransferListVM> transferList)
        {
            var request = new TransferListForZOCommand() { TransferList = transferList };
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("MatchRequestTransferVacancy")]
        public async Task<IActionResult> MatchRequestTransferVacancy(List<int> employeeIdList)
        {
            var request = new GetMatchingRequestTransferListQuery() { EmployeeIdList = employeeIdList };
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
