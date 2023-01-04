using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Moq;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Features.Orders.GetOrdersForMonth;
using PostingManagement.Application.Features.Triggers.Queries.GetAllTrigger;
using PostingManagement.Application.Profiles;
using PostingManagement.Application.Responses;
using PostingManagement.Application.UnitTests.Mocks;
using PostingManagement.Domain.Entities;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PostingManagement.Application.UnitTests.Triggers.Queries
{
    public class GetAllTriggerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITriggerRepository> _mockTriggerRepository;
        Mock<IDataProtectionProvider> mockDataProtectionProvider = new Mock<IDataProtectionProvider>();

        public GetAllTriggerTests()
        {
            CreateDataProtector(1);
            _mockTriggerRepository = TriggerRepositoryMocks.GetTriggerRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TriggerVm, GetAllTriggerDto>().ConvertUsing(new GetAllTriggerCustomMapper(mockDataProtectionProvider.Object));
            });
            _mapper = configurationProvider.CreateMapper();
        }

   
        [Fact]
        public async Task Get_AllTrigger_Data()
        {
            var handler = new GetAllTriggerQueryHandler(_mockTriggerRepository.Object, _mapper);

            var result = await handler.Handle(new GetAllTriggerQuery() , CancellationToken.None);

            result.ShouldBeOfType<Response<List<GetAllTriggerDto>>>();
            result.Data.ShouldBeOfType<List<GetAllTriggerDto>>();
            result.Data.ShouldNotBeEmpty();
        }

        private void CreateDataProtector(int triggerId)
        {
            Mock<IDataProtector> mockDataProtector = new Mock<IDataProtector>();
            mockDataProtector.Setup(sut => sut.Protect(It.IsAny<byte[]>())).Returns(Encoding.UTF8.GetBytes("ProtectedData"));
            mockDataProtector.Setup(sut => sut.Unprotect(It.IsAny<byte[]>())).Returns(Encoding.UTF8.GetBytes(triggerId.ToString()));
            mockDataProtectionProvider.Setup(s => s.CreateProtector(It.IsAny<string>())).Returns(mockDataProtector.Object);
        }

    }
}
