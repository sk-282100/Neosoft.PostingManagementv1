using MediatR;
using Microsoft.AspNetCore.Mvc;
using PostingManagement.Application.Features.Scales.Queries.GetAllScales;

namespace PostingManagement.Api.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ScaleController : Controller
    {
        private readonly IMediator _mediator;
        public ScaleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllScale")]
        public async Task<IActionResult> GetAllScaleDetails()
        {
            GetAllScalesQuery request = new GetAllScalesQuery();
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        
    }
}
