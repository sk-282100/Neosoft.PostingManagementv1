using Microsoft.AspNetCore.DataProtection;
using Moq;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Features.Triggers.Commands.UpdateTrigger;
using PostingManagement.Application.UnitTests.Mocks;
using PostingManagement.Domain.Entities;
using Shouldly;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace PostingManagement.Application.UnitTests.Triggers.Commands
{
    public class UpdateTriggerTests
    {
        private readonly ITestOutputHelper output;
        private readonly Mock<ITriggerRepository> _mockTriggerRepository;
        Mock<IDataProtectionProvider> mockDataProtectionProvider = new Mock<IDataProtectionProvider>();

        public UpdateTriggerTests(ITestOutputHelper outputHelper)
        {
            output = outputHelper;
            _mockTriggerRepository = TriggerRepositoryMocks.GetTriggerRepository();
        }

        [Fact]
        public async Task Successful_Update_Trigger()
        {
            int scaleId = 1;
            int triggerId = 1;
            CreateDataProtector(triggerId, scaleId);
            var handler = new UpdateTriggerCommandHandler(_mockTriggerRepository.Object,mockDataProtectionProvider.Object);
            var newTrigger = new Trigger()
            {
                TriggerId = triggerId,
                ScaleId = scaleId,
                Tenure = 20,
                Mandatory = "Yes"
            };

            var result = await handler.Handle(new UpdateTriggerCommand()
            {
                TriggerId = "ProtectedTriggerId",
                ScaleId = "ProtectedScaleId",
                Tenure = newTrigger.Tenure,
                Mandatory = newTrigger.Mandatory
            }, CancellationToken.None);

            result.Message.ShouldBe("Requested Trigger Update successfully ");
        }

        [Fact]
        public async Task Update_Trigger_with_NonExisting_ScaleId()
        {
            int scaleId = -1;
            int triggerId = 1;
            CreateDataProtector(triggerId, scaleId);
            var handler = new UpdateTriggerCommandHandler(_mockTriggerRepository.Object, mockDataProtectionProvider.Object);
            var newTrigger = new Trigger()
            {
                TriggerId = triggerId,
                ScaleId = scaleId,
                Tenure = 20,
                Mandatory = "Yes"
            };

            var result = await handler.Handle(new UpdateTriggerCommand()
            {
                TriggerId = "ProtectedTriggerId",
                ScaleId = "ProtectedScaleId",
                Tenure = newTrigger.Tenure,
                Mandatory = newTrigger.Mandatory
            }, CancellationToken.None);

            result.Message.ShouldBe("Updation Failed !!!");
        }

        [Fact]
        public async Task Update_Trigger_with_NonExisting_Trigger()
        {
            int scaleId = 1;
            int triggerId = -1;
            CreateDataProtector(triggerId, scaleId);
            var handler = new UpdateTriggerCommandHandler(_mockTriggerRepository.Object, mockDataProtectionProvider.Object);
            var newTrigger = new Trigger()
            {
                TriggerId = triggerId,
                ScaleId = scaleId,
                Tenure = 20,
                Mandatory = "Yes"
            };

            var result = await handler.Handle(new UpdateTriggerCommand()
            {
                TriggerId = "ProtectedTriggerId",
                ScaleId = "ProtectedScaleId",
                Tenure = newTrigger.Tenure,
                Mandatory = newTrigger.Mandatory
            }, CancellationToken.None);

            result.Message.ShouldBe("This Trigger is not Present");
        }


        private void CreateDataProtector(int triggerId,int scaleId)
        {
            Mock<IDataProtector> mockDataProtector = new Mock<IDataProtector>();
            mockDataProtector.Setup(sut => sut.Unprotect(It.IsAny<byte[]>())).Returns(
                (byte[] protectedId) =>
                {
                    if(protectedId.Length == 13)
                        return Encoding.UTF8.GetBytes(triggerId.ToString());
                    if(protectedId.Length == 12)
                        return Encoding.UTF8.GetBytes(scaleId.ToString());
                    else
                        return Encoding.UTF8.GetBytes("-1");
                });
            mockDataProtectionProvider.Setup(s => s.CreateProtector(It.IsAny<string>())).Returns(mockDataProtector.Object);
        }

    }
}

