using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostingManagement.Application.Features.ExcelUpload.Command;
using PostingManagement.Application.Features.TransferList.Queries.GetEmployeeDetailsById;
using PostingManagement.Application.Features.TransferList.Queries.GetTransferList;

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
        public async Task<IActionResult> GetTransferList()
        {
            var request = new GetTransferListQuery();
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
    }
}
