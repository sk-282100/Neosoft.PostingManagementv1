using Microsoft.AspNetCore.DataProtection;
using Moq;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Features.Roles.Commands.AddRole;
using PostingManagement.Application.Features.Roles.Commands.DeleteRole;
using PostingManagement.Application.UnitTests.Mocks;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PostingManagement.Application.UnitTests.Role.Commands
{
    public class DeleteRoleTests
    {
        private Mock<IRoleRepository> _roleRepository;
        Mock<IDataProtectionProvider> mockDataProtectionProvider = new Mock<IDataProtectionProvider>();

        public DeleteRoleTests()
        {
            _roleRepository = RoleRepositoryMocks.GetRoleRepository();     
        }
        [Fact]
        public async Task Handle_RoleIdDoesNotExistsForDelete()
        {
            int roleId = 4;
            CreateDataProtector(roleId);
            var handler = new DeleteRoleCommandHandler(_roleRepository.Object, mockDataProtectionProvider.Object);

            var result = await handler.Handle(new DeleteRoleCommand()
            {
                RoleId = "ProtectedRoleId"
            }, CancellationToken.None);

            var allRoles = await _roleRepository.Object.GetAllRoles();
            result.Message.ShouldBe("Role Does Not Exists");
            allRoles.Count.ShouldBe(3);
        }
        [Fact]
        public async Task Handle_RoleIdExistsAndDeletedSuccessfully()
        {
            int roleId = 1;
            CreateDataProtector(roleId);
            var handler = new DeleteRoleCommandHandler(_roleRepository.Object, mockDataProtectionProvider.Object);

            var result = await handler.Handle(new DeleteRoleCommand()
            {
                RoleId = "ProtectedRoleId"
            }, CancellationToken.None);

            var allRoles = await _roleRepository.Object.GetAllRoles();
            result.Message.ShouldBe("Role Deleted Successfully");
            allRoles.Count.ShouldBe(2);
        }

        private void CreateDataProtector(int roleId)
        {
            Mock<IDataProtector> mockDataProtector = new Mock<IDataProtector>();
            mockDataProtector.Setup(sut => sut.Unprotect(It.IsAny<byte[]>())).Returns(Encoding.UTF8.GetBytes(roleId.ToString()));
            mockDataProtectionProvider.Setup(s => s.CreateProtector(It.IsAny<string>())).Returns(mockDataProtector.Object);
        }
    }
}
