using Microsoft.AspNetCore.DataProtection;
using Moq;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Features.Triggers.Commands.AddTrigger;
using PostingManagement.Application.UnitTests.Mocks;
using Shouldly;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PostingManagement.Application.UnitTests.Triggers.Commands
{
    public class AddTriggerTests
    {
        private readonly Mock<ITriggerRepository> _mockTriggerRepository;
        Mock<IDataProtectionProvider> mockDataProtectionProvider = new Mock<IDataProtectionProvider>();

        public AddTriggerTests()
        {
            _mockTriggerRepository = TriggerRepositoryMocks.GetTriggerRepository();
        }

        [Fact]
        public async Task Successfully_Added()
        {
            int scaleId = 1;
            CreateDataProtector(scaleId);
            var handler = new AddTriggerCommandHandler(_mockTriggerRepository.Object, mockDataProtectionProvider.Object);

            await handler.Handle(new AddTriggerCommand()
            {
                ScaleId = "ProtectedScaleId",
                Tenure = 23,
                Mandatory = "No"
            }, CancellationToken.None);

            var allTrigger = await _mockTriggerRepository.Object.GetAllTriggerDetails();
            allTrigger.Count.ShouldBe(4);
        }

        [Fact]
        public async Task Add_Tigger_with_NotExisting_ScaleId()
        {
            int scaleId = -12;
            CreateDataProtector(scaleId);
            var handler = new AddTriggerCommandHandler(_mockTriggerRepository.Object, mockDataProtectionProvider.Object);

            await handler.Handle(new AddTriggerCommand()
            {
                ScaleId = "ProtectedScaleId",
                Tenure = 3,
                Mandatory = "Yes"
            }, CancellationToken.None);

            var allTrigger = await _mockTriggerRepository.Object.GetAllTriggerDetails();
            allTrigger.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Add_Tigger_With_Existing_ScaleId()
        {
            int scaleId = 2;
            CreateDataProtector(scaleId);
            var handler = new AddTriggerCommandHandler(_mockTriggerRepository.Object, mockDataProtectionProvider.Object);

            await handler.Handle(new AddTriggerCommand()
            {
                ScaleId = "ProtectedScaleId",
                Tenure = 23,
                Mandatory = "No"
            }, CancellationToken.None);

            var allTrigger = await _mockTriggerRepository.Object.GetAllTriggerDetails();
            allTrigger.Count.ShouldBe(4);
        }

        private void CreateDataProtector(int triggerId)
        {
            Mock<IDataProtector> mockDataProtector = new Mock<IDataProtector>();
            mockDataProtector.Setup(sut => sut.Unprotect(It.IsAny<byte[]>())).Returns(Encoding.UTF8.GetBytes(triggerId.ToString()));
            mockDataProtectionProvider.Setup(s => s.CreateProtector(It.IsAny<string>())).Returns(mockDataProtector.Object);
        }
    }
}
