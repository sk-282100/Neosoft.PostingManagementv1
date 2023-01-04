using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Moq;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Features.Triggers.Queries.GetTriggerById;
using PostingManagement.Application.Profiles;
using PostingManagement.Application.Responses;
using PostingManagement.Application.UnitTests.Mocks;
using PostingManagement.Domain.Entities;
using Shouldly;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PostingManagement.Application.UnitTests.Triggers.Queries
{
    public class GetTriggerByIdTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITriggerRepository> _mockTriggerRepository;
        Mock<IDataProtectionProvider> mockDataProtectionProvider = new Mock<IDataProtectionProvider>();

        public GetTriggerByIdTests()
        {
            CreateDataProtector(1);
            _mockTriggerRepository = TriggerRepositoryMocks.GetTriggerRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Trigger, GetTriggerByIdDto>().ConvertUsing(new GetTriggerByIdCustomMapper(mockDataProtectionProvider.Object));
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Get_TriggerById_With_ExistingTriggerId()
        {
            int triggerId = 2;
            CreateDataProtector(triggerId);
            var handler = new GetTriggerByIdQueryHandler(_mockTriggerRepository.Object, _mapper, mockDataProtectionProvider.Object);

            var result = await handler.Handle(new GetTriggerByIdQuery()
            {
                TriggerId = "ProtectedTriggerId"
            },CancellationToken.None);

            result.Succeeded.ShouldBe(true);
            result.ShouldBeOfType<Response<GetTriggerByIdDto>>();
            result.Data.ShouldBeOfType<GetTriggerByIdDto>();
        }

        [Fact]
        public async Task Get_TriggerById_With_NonExistingTriggerId()
        {
            int triggerId = 52;
            CreateDataProtector(triggerId);
            var handler = new GetTriggerByIdQueryHandler(_mockTriggerRepository.Object, _mapper, mockDataProtectionProvider.Object);

            var result = await handler.Handle(new GetTriggerByIdQuery()
            {
                TriggerId = "ProtectedTriggerId"
            }, CancellationToken.None);

            result.Succeeded.ShouldBe(false);
            result.Message.ShouldBe("Trigger Not Found ");
            //result.Data.ShouldBeNull();
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
