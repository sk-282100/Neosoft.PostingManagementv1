using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostingManagement.Application.Features.JobFamily.Commands.AddJobFamily;
using PostingManagement.Application.Features.JobFamily.Commands.DeleteJobFamily;
using PostingManagement.Application.Features.JobFamily.Commands.EditJobFamily;
using PostingManagement.Application.Features.JobFamily.Queries.GetAllJobFamilyquery;
using PostingManagement.Application.Features.JobFamily.Queries.GetJobFamilyById;
using PostingManagement.Application.Features.JobFamily.Queries.IsJobFamilyAlreadyExist;
using PostingManagement.Domain.Entities;

namespace PostingManagement.Api.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class JobFamilyController : ControllerBase
    {
        private readonly IMediator _mediator;
        public JobFamilyController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetAllJobFamily")]
        public async Task<IActionResult> GetAllJobFamily()
        {
            GetAllJobFamilyQuery query = new GetAllJobFamilyQuery();
            var response = await _mediator.Send(query);
            return Ok(response);    
        }
        [HttpPost("AddJobFamily")]
        public async Task<IActionResult> AddJobFamily(AddjobFamilyCommand command)
        {
            var response = await _mediator.Send(command);   
            return Ok(response);    
        }
        [HttpDelete("DeleteJobFamily")]
        public async Task<IActionResult> DeleteJobFamily(string id)
        {
            DeleteJobFamilyCommand command = new DeleteJobFamilyCommand { JobFamilyId = id };
            var response = await _mediator.Send(command);
            return Ok(response);    
        }
        [HttpPut("EditJobFamily")]
        public async Task <IActionResult> EditJobFamily(EditJobFamilyCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("GetJobFamilyById")]
        public async Task<IActionResult> GetJobFamilyById(string id)
        {
            GetJobFamilyByIdQuery query = new GetJobFamilyByIdQuery { JobFamilyId = id };
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        [HttpGet("IsJobFamilyAlreadyExist")]
        public async Task<IActionResult> IsJobFamilyAlreadyExist(string jobFamilyName)
        {
            IsJobFamilyAlreadyExistQuery query = new IsJobFamilyAlreadyExistQuery { JobFamilyName = jobFamilyName };
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        
    }
}
