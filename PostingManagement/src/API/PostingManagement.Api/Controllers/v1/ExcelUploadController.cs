using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PostingManagement.Application.Features.ExcelUpload.Command;
using PostingManagement.Application.Features.ExcelUpload.Command.ResetWorkflowCommand;
using PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData;
using PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData.BranchMasterRecords;
using PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData.DepartmentMasterRecords;
using PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData.EmployeeMasterRecords;
using PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData.InterRegionalPromotionRecords;
using PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData.InterRegionalRequestRecords;
using PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData.InterZonalPromotionRecords;
using PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData.InterZonalRequestRecords;
using PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData.RegionMasterRecords;
using PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData.ZoneMasterRecords;
using PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelWorkFlowSTatus;
using PostingManagement.Application.Features.ExcelUpload.Queries.GetUploadHistoryList;
using PostingManagement.Application.Helper.Constants;
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

        [HttpPost("VacancyPoolUpload")]
        public async Task<IActionResult> VacancyPoolUpload(string username, ExcelUploadRequest<VacancyPool> excelData)
        {
            var request = new ExcelUploadCommand<VacancyPool>() { UploadedBy = username, ExcelDataList = excelData.FileData, FileName = excelData.FileName };
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

        [HttpGet("GetAllRecords")]
        public async Task<IActionResult> GetExcelData(int fileTypeCode, int batchId)
        {
            var request = new object();
            var result = new object();
            var response = new object();
            switch (fileTypeCode)
            {
                case (int)ExcelFileType.BranchMaster:
                    request = new GetExcelDataQuery<BranchMaster>() { FileTypeCode = fileTypeCode, BatchId = batchId };
                    result = await _mediator.Send(request);
                    if (result.GetType() == (typeof(object)))
                        response = JsonConvert.DeserializeObject<List<BranchMasterRecordsDto>>((string)result);
                    else
                        response = result;
                    return Ok(response);

                case (int)ExcelFileType.DepartmentMaster:
                    request = new GetExcelDataQuery<DepartmentMaster>() { FileTypeCode = fileTypeCode, BatchId = batchId };
                    result = await _mediator.Send(request);
                    if (result.GetType() == (typeof(object)))
                        response = JsonConvert.DeserializeObject<List<DepartmentMasterRecordsDto>>((string)result);
                    else
                        response = result;
                    return Ok(response);

                case (int)ExcelFileType.EmployeeMaster:
                    request = new GetExcelDataQuery<EmployeeMaster>() { FileTypeCode = fileTypeCode, BatchId = batchId };
                    result = await _mediator.Send(request);
                    if (result.GetType() == (typeof(object)))
                        response = JsonConvert.DeserializeObject<List<EmployeeMasterRecordsDto>>((string)result);
                    else
                        response = result;
                    return Ok(response);

                case (int)ExcelFileType.InterRegionPromotion:
                    request = new GetExcelDataQuery<InterRegionalPromotion>() { FileTypeCode = fileTypeCode, BatchId = batchId };
                    result = await _mediator.Send(request);
                    if (result.GetType() == (typeof(object)))
                        response = JsonConvert.DeserializeObject<List<InterRegionalPromotionRecordsDto>>((string)result);
                    else
                        response = result;
                    return Ok(response);

                case (int)ExcelFileType.InterRegionRequestTransfer:
                    request = new GetExcelDataQuery<InterRegionRequestTransfer>() { FileTypeCode = fileTypeCode, BatchId = batchId };
                    result = await _mediator.Send(request);
                    if (result.GetType() == (typeof(object)))
                        response = JsonConvert.DeserializeObject<List<InterRegionalRequestRecordsDto>>((string)result);
                    else
                        response = result;
                    return Ok(response);

                case (int)ExcelFileType.InterZonalPromotion:
                    request = new GetExcelDataQuery<InterZonalPromotion>() { FileTypeCode = fileTypeCode, BatchId = batchId };
                    result = await _mediator.Send(request);
                    if (result.GetType() == (typeof(object)))
                        response = JsonConvert.DeserializeObject<List<InterZonalPromotionRecordsDto>>((string)result);
                    else
                        response = result;
                    return Ok(response);

                case (int)ExcelFileType.InterZonalRequestTranfer:
                    request = new GetExcelDataQuery<InterZonalRequestTransfer>() { FileTypeCode = fileTypeCode, BatchId = batchId };
                    result = await _mediator.Send(request);
                    if (result.GetType() == (typeof(object)))
                        response = JsonConvert.DeserializeObject<List<InterZonalRequestRecordsDto>>((string)result);
                    else
                        response = result;
                    return Ok(response);

                case (int)ExcelFileType.RegionMaster:
                    request = new GetExcelDataQuery<RegionMaster>() { FileTypeCode = fileTypeCode, BatchId = batchId };
                    result = await _mediator.Send(request);
                    if (result.GetType() == (typeof(object)))
                    {
                        response = JsonConvert.DeserializeObject<List<RegionMasterRecordsDto>>(Convert.ToString(result));
                    }
                    else
                        response = result;
                    return Ok(response);
                case (int)ExcelFileType.ZoneMaster:
                    request = new GetExcelDataQuery<ZoneMaster>() { FileTypeCode = fileTypeCode, BatchId = batchId };
                    result = await _mediator.Send(request);
                    if (result.GetType() == (typeof(object)))
                    {
                        response = JsonConvert.DeserializeObject<List<ZoneMasterRecordsDto>>(Convert.ToString(result));
                    }
                    else
                        response = result;
                    return Ok(response);
            }
            return Ok();
        }

        [HttpGet("GetWorkflowStatus")]
        public async Task<IActionResult> GetWorkFlowSatus()
        {
            GetWorkFlowStatusQuery request = new GetWorkFlowStatusQuery();
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("ResetWorkflow")]
        public async Task<IActionResult> ResetWorkflow()
        {
            ResetWorkflowCommand request = new ResetWorkflowCommand();
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
