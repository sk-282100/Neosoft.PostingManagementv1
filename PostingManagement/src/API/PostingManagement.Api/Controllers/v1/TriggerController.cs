using MediatR;
using Microsoft.AspNetCore.Mvc;
using PostingManagement.Application.Features.Triggers.Commands.AddTrigger;
using PostingManagement.Application.Features.Triggers.Commands.DeleteTrigger;
using PostingManagement.Application.Features.Triggers.Commands.UpdateTrigger;
using PostingManagement.Application.Features.Triggers.Queries.GetAllTrigger;
using PostingManagement.Application.Features.Triggers.Queries.GetTriggerById;

namespace PostingManagement.Api.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TriggerController : Controller
    {
        private readonly IMediator _mediator;

        public TriggerController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("AddNewTrigger")]
        public async Task<IActionResult> AddTrigger(AddTriggerCommand request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("UpdateTrigger")]
        public async Task<IActionResult> UpdateTrigger(UpdateTriggerCommand request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("DeleteTrigger")]
        public async Task<IActionResult> DeleteTrigger(string triggerId)
        {
            DeleteTriggerCommand request = new DeleteTriggerCommand() { TriggerId = triggerId };
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("GetAllTriggers")]
        public async Task<IActionResult> GetAllTrigger()
        {
            GetAllTriggerQuery request = new GetAllTriggerQuery();
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("GetTriggerById")]
        public async Task<IActionResult> GetTriggerById(string Id)
        {
            GetTriggerByIdQuery request = new GetTriggerByIdQuery() { TriggerId = Id };
            var response = await _mediator.Send(request);
           
            return Ok(response);
        }
        
    }
}
