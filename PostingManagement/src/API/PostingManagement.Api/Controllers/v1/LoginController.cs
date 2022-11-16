using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostingManagement.Application.Features.LogIn.Queries;
using PostingManagement.Domain.Entities;

namespace PostingManagement.Api.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IMediator _mediator;
        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Authentication(LoginQuery query)
        {
            var loginResponse = await _mediator.Send(query);
            return Ok(loginResponse);
        }
    }
}
