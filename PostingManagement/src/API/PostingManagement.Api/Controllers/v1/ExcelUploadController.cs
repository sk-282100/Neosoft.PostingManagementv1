using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PostingManagement.Application.Features.ExcelUpload.Command;
using PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData;
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

        [HttpPost ("GetAllRecords")]
        public async Task<IActionResult> GetExcelData(int fileTypeCode, int batchId)
        {
            var request = new object();
            var result = new object();
            var response = new object();
            switch (fileTypeCode)
            {                
                case 1:
                    request = new GetExcelDataQuery<BranchMaster>() { FileTypeCode = fileTypeCode,BatchId = batchId};
                    result = await _mediator.Send(request);
                    if (result.GetType() == (typeof(object)))
                        response = JsonConvert.DeserializeObject<List<BranchMaster>>((string)result);
                    else
                        response = result;
                    return Ok(response);
                case 2:
                    request = new GetExcelDataQuery<DepartmentMaster>() { FileTypeCode = fileTypeCode, BatchId = batchId };
                    result = await _mediator.Send(request);
                    if (result.GetType() == (typeof(object)))
                        response = JsonConvert.DeserializeObject<List<DepartmentMaster>>((string)result);
                    else
                        response = result;
                    return Ok(response);
                case 3:

                case 4:

                case 5:

                case 6:

                case 7:

                case 8:
                    request = new GetExcelDataQuery<RegionMaster>() { FileTypeCode = fileTypeCode, BatchId = batchId };
                    result = await _mediator.Send(request);
                    if (result.GetType() == (typeof(object)))
                    {
                        response = JsonConvert.DeserializeObject<List<RegionMaster>>(Convert.ToString(result));
                    }
                    else
                        response = result;
                    return Ok(response);
                case 9:
                    request = new GetExcelDataQuery<ZoneMaster>() { FileTypeCode = fileTypeCode, BatchId = batchId };
                    result = await _mediator.Send(request);
                    if (result.GetType() == (typeof(object)))
                    {
                        response = JsonConvert.DeserializeObject<List<ZoneMaster>>(Convert.ToString(result));
                    }
                    else
                        response = result;
                    return Ok(response);
            }
            return Ok();            
        }
    }
}
