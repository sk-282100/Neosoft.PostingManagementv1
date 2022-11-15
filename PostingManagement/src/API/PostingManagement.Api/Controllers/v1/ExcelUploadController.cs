using MediatR;
using Microsoft.AspNetCore.Mvc;
using PostingManagement.Application.Features.ExcelUpload.Command;
using PostingManagement.Application.Features.ExcelUpload.Queries.GetUploadHistoryList;
using PostingManagement.Domain.Entities;

namespace PostingManagement.Api.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ExcelUploadController : Controller
    {
        private readonly IMediator _mediator;

        public ExcelUploadController(IMediator mediator)
        {
            _mediator = mediator;
        }
         
        [HttpPost]
        public async Task<IActionResult> ExcelUpload(string username,ExcelUploadRequest<BranchMaster> excelData)
        {
            var request=new ExcelUploadCommand<BranchMaster>() { UploadedBy=username, ExcelDataList = excelData.FileData , FileName=excelData.FileName };
            var response= await _mediator.Send(request);
            return Ok(response);
        }

        
        [HttpPost("BranchMasterExcelUpload")]
        public async Task<IActionResult> BranchMasterExcelUpload(string username, ExcelUploadRequest<BranchMaster> excelData)
        {
            var request = new ExcelUploadCommand<BranchMaster>() { UploadedBy = username, ExcelDataList = excelData.FileData, FileName = excelData.FileName };
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("EmployeeMasterExcelUpload")]
        public async Task<IActionResult> EmployeeMasterExcelUpload(string username, ExcelUploadRequest<EmployeeMaster> excelData)
        {
            var request = new ExcelUploadCommand<EmployeeMaster>() { UploadedBy = username, ExcelDataList = excelData.FileData, FileName = excelData.FileName };
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("InterRegionalPromotionExcelUpload")]
        public async Task<IActionResult> InterRegionalPromotionExcelUpload(string username, ExcelUploadRequest<InterRegionalPromotion> excelData)
        {
            var request = new ExcelUploadCommand<InterRegionalPromotion>() { UploadedBy = username, ExcelDataList = excelData.FileData, FileName = excelData.FileName };
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("InterRegionRequestTransferExcelUpload")]
        public async Task<IActionResult> InterRegionRequestTransferExcelUpload(string username, ExcelUploadRequest<InterRegionRequestTransfer> excelData)
        {
            var request = new ExcelUploadCommand<InterRegionRequestTransfer>() { UploadedBy = username, ExcelDataList = excelData.FileData, FileName = excelData.FileName };
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("InterZonalPromotionExcelUpload")]
        public async Task<IActionResult> InterZonalPromotionExcelUpload(string username, ExcelUploadRequest<InterZonalPromotion> excelData)
        {
            var request = new ExcelUploadCommand<InterZonalPromotion>() { UploadedBy = username, ExcelDataList = excelData.FileData, FileName = excelData.FileName };
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("InterZonalRequestTransferExcelUpload")]
        public async Task<IActionResult> InterZonalRequestTransferExcelUpload(string username, ExcelUploadRequest<InterZonalRequestTransfer> excelData)
        {
            var request = new ExcelUploadCommand<InterZonalRequestTransfer>() { UploadedBy = username, ExcelDataList = excelData.FileData, FileName = excelData.FileName };
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("RegionMasterExcelUpload")]
        public async Task<IActionResult> RegionMasterExcelUpload(string username, ExcelUploadRequest<RegionMaster> excelData)
        {
            var request = new ExcelUploadCommand<RegionMaster>() { UploadedBy = username, ExcelDataList = excelData.FileData, FileName = excelData.FileName };
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("ZoneMasterExcelUpload")]
        public async Task<IActionResult> ZoneMasterExcelUpload(string username, ExcelUploadRequest<ZoneMaster> excelData)
        {
            var request = new ExcelUploadCommand<ZoneMaster>() { UploadedBy = username, ExcelDataList = excelData.FileData, FileName = excelData.FileName };
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("DepartmentMasterExcelUpload")]
        public async Task<IActionResult> DepartmentMasterExcelUpload(string username, ExcelUploadRequest<DepartmentMaster> excelData)
        {
            var request = new ExcelUploadCommand<DepartmentMaster>() { UploadedBy = username, ExcelDataList = excelData.FileData, FileName = excelData.FileName };
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet ("GetUploadHistoryList")]
        public async Task<IActionResult> GetUploadHistoryList (int fileTypeCode)
        {
            var request = new GetUploadHistoryQuery() { FileTypeCode = fileTypeCode };
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
