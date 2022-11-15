using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostingManagement.Application.Features.ExcelUpload.Command;
using PostingManagement.Domain.Entities;

namespace PostingManagement.Api.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("EmployeeMasterExcelUpload")]
        public async Task<IActionResult> EmployeeMasterExcelUpload(string username, ExcelUploadRequest<EmployeeMaster> excelData)
        {
            var request = new ExcelUploadCommand<EmployeeMaster>() { UploadedBy = username, ExcelDataList = excelData.FileData, FileName = excelData.FileName };
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
