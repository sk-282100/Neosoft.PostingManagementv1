using Microsoft.AspNetCore.DataProtection;
using Moq;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Features.Triggers.Commands.DeleteTrigger;
using PostingManagement.Application.UnitTests.Mocks;
using Shouldly;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PostingManagement.Application.UnitTests.Triggers.Commands
{
    public class DeleteTriggerTests
    {

        private readonly Mock<ITriggerRepository> _mockTriggerRepository;
        Mock<IDataProtectionProvider> mockDataProtectionProvider = new Mock<IDataProtectionProvider>();
        public DeleteTriggerTests()
        {
            _mockTriggerRepository = TriggerRepositoryMocks.GetTriggerRepository();
        }

        [Fact]
        public async Task Successfully_Delete_Trigger()
        {
            int triggerId = 1;
            CreateDataProtector(triggerId);
            var handler = new DeleteTriggerCommandHandler(_mockTriggerRepository.Object, mockDataProtectionProvider.Object);

            var result = await handler.Handle(new DeleteTriggerCommand()
            {
                TriggerId = "ProtectedTriggerId"
            }, CancellationToken.None);

            result.Data.ShouldBe(true);
            result.Message.ShouldBe("Requested Trigger Deleted successfully ");
            var allTriggers = await _mockTriggerRepository.Object.GetAllTriggerDetails();
            allTriggers.Count.ShouldBe(2);
        }

        [Fact]
        public async Task DeleteTrigger_with_notExisting_TriggerId()
        {
            int triggerId = -1;
            CreateDataProtector(triggerId);
            var handler = new DeleteTriggerCommandHandler(_mockTriggerRepository.Object, mockDataProtectionProvider.Object);

            var result = await handler.Handle(new DeleteTriggerCommand()
            {
                TriggerId = "ProtectedTriggerId"
            }, CancellationToken.None);

            result.Data.ShouldBe(false);
            result.Message.ShouldBe("Deletion Failed !!!");
            var allTriggers = await _mockTriggerRepository.Object.GetAllTriggerDetails();
            allTriggers.Count.ShouldBe(3);
        }

        private void CreateDataProtector(int triggerId)
        {
            Mock<IDataProtector> mockDataProtector = new Mock<IDataProtector>();
            mockDataProtector.Setup(sut => sut.Unprotect(It.IsAny<byte[]>())).Returns(Encoding.UTF8.GetBytes(triggerId.ToString()));
            mockDataProtectionProvider.Setup(s => s.CreateProtector(It.IsAny<string>())).Returns(mockDataProtector.Object);
        }
    }
}
