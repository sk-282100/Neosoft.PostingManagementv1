using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PostingManagement.Api.Controllers.v1;
using PostingManagement.API.UnitTests.Mocks;
using PostingManagement.Application.Features.Categories.Queries.GetCategoriesList;
using PostingManagement.Application.Features.ExcelUpload.Command;
using PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelData.BranchMasterRecords;
using PostingManagement.Application.Features.ExcelUpload.Queries.GetExcelWorkFlowSTatus;
using PostingManagement.Application.Features.ExcelUpload.Queries.GetUploadHistoryList;
using PostingManagement.Application.Responses;
using PostingManagement.Domain;
using PostingManagement.Domain.Entities;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PostingManagement.API.UnitTests.Controllers.v1
{
    public class ExcelUploadControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;        
        public ExcelUploadControllerTests()
        {            
            _mockMediator = MediatorMocks.GetMediator();
        }
        [Fact]
        public async Task BranchMaster_ExcelUpload()
        {
            var controller = new ExcelUploadController(_mockMediator.Object);

            var result = await controller.BranchMasterExcelUpload("abc", new ExcelUploadRequest<BranchMaster>() );

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType <Response<ExcelUploadDto>>();
        }
        [Fact]
        public async Task EmployeeMaster_ExcelUpload()
        {
            var controller = new ExcelUploadController(_mockMediator.Object);

            var result = await controller.EmployeeMasterExcelUpload("abc", new ExcelUploadRequest<EmployeeMaster>());

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<ExcelUploadDto>>();
        }
        [Fact]
        public async Task DepartmentMaster_ExcelUpload()
        {
            var controller = new ExcelUploadController(_mockMediator.Object);

            var result = await controller.DepartmentMasterExcelUpload("abc", new ExcelUploadRequest<DepartmentMaster>());

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<ExcelUploadDto>>();
        }
        [Fact]
        public async Task RegionMaster_ExcelUpload()
        {
            var controller = new ExcelUploadController(_mockMediator.Object);

            var result = await controller.RegionMasterExcelUpload("abc", new ExcelUploadRequest<RegionMaster>());

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<ExcelUploadDto>>();
        }
        [Fact]
        public async Task ZoneMaster_ExcelUpload()
        {
            var controller = new ExcelUploadController(_mockMediator.Object);

            var result = await controller.ZoneMasterExcelUpload("abc", new ExcelUploadRequest<ZoneMaster>());

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<ExcelUploadDto>>();
        }
        [Fact]
        public async Task InterRegionalPromotion_ExcelUpload()
        {
            var controller = new ExcelUploadController(_mockMediator.Object);

            var result = await controller.InterRegionalPromotionExcelUpload("abc", new ExcelUploadRequest<InterRegionalPromotion>());

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<ExcelUploadDto>>();
        }
        [Fact]
        public async Task InterRegionRequestTransfer_ExcelUpload()
        {
            var controller = new ExcelUploadController(_mockMediator.Object);

            var result = await controller.InterRegionRequestTransferExcelUpload("abc", new ExcelUploadRequest<InterRegionRequestTransfer>());

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<ExcelUploadDto>>();
        }
        [Fact]
        public async Task InterZonalPromotion_ExcelUpload()
        {
            var controller = new ExcelUploadController(_mockMediator.Object);

            var result = await controller.InterZonalPromotionExcelUpload("abc", new ExcelUploadRequest<InterZonalPromotion>());

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<ExcelUploadDto>>();
        }
        [Fact]
        public async Task InterZonalRequestTransfer_ExcelUpload()
        {
            var controller = new ExcelUploadController(_mockMediator.Object);

            var result = await controller.InterZonalRequestTransferExcelUpload("abc", new ExcelUploadRequest<InterZonalRequestTransfer>());;

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<ExcelUploadDto>>();
        }
        [Fact]
        public async Task VacancyPool_ExcelUpload()
        {
            var controller = new ExcelUploadController(_mockMediator.Object);

            var result = await controller.VacancyPoolUpload("abc", new ExcelUploadRequest<VacancyPool>()); ;

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<ExcelUploadDto>>();
        }
        [Fact]
        public async Task Get_UploadHistoryList()
        {
            var controller = new ExcelUploadController(_mockMediator.Object);

            var result = await controller.GetUploadHistoryList(1);

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<List<GetUploadHistoryDto>>>();
        }        
        [Fact]
        public async Task Get_WorkFlowSatus()
        {
            var controller = new ExcelUploadController(_mockMediator.Object);

            var result = await controller.GetWorkFlowSatus();

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<GetWorkFlowStatusDto>>();
        }
        [Fact]
        public async Task Reset_Workflow()
        {
            var controller = new ExcelUploadController(_mockMediator.Object);

            var result = await controller.ResetWorkflow();

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<bool>>();
        }
    }
}
