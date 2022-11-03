using MediatR;
using Microsoft.AspNetCore.Mvc;
using PostingManagement.Application.Features.Categories.Queries.GetCategoriesList;

namespace PostingManagement.Api.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
          private readonly IMediator _mediator;
    private readonly ILogger _logger;

    public HomeController(IMediator mediator, ILogger<CategoryController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    //[Authorize]
    [HttpGet("DemoService", Name = "DemoService")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> DemoService()
    {
        _logger.LogInformation("DemoService Initiated");
        var dtos = "OK";
        _logger.LogInformation("DemoService Completed");
        return Ok(dtos);
    }
}
}
