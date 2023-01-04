using Microsoft.AspNetCore.DataProtection;
using Moq;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Features.Roles.Commands.DeleteRole;
using PostingManagement.Application.Features.Roles.Commands.EditRole;
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
    public class EditRoleTests
    {
        private Mock<IRoleRepository> _roleRepository;
        Mock<IDataProtectionProvider> mockDataProtectionProvider = new Mock<IDataProtectionProvider>();

        public EditRoleTests()
        {
            _roleRepository = RoleRepositoryMocks.GetRoleRepository();
        }
        [Fact]
        public async Task Handle_RoleIdDoesNotExistsForEdit()
        {
            int roleId = 4;
            CreateDataProtector(roleId);
            var handler = new EditRoleCommandHandler(_roleRepository.Object, mockDataProtectionProvider.Object);

            var result = await handler.Handle(new EditRoleCommand()
            {
                RoleId = "ProtectedRoleId",
                RoleName = "New Role"
            }, CancellationToken.None);
            
            result.Message.ShouldBe("Role Does Not Exist.");            
        }
        [Fact]
        public async Task Handle_RoleSuccessfullyEdited()
        {
            int roleId = 1;
            CreateDataProtector(roleId);
            var handler = new EditRoleCommandHandler(_roleRepository.Object, mockDataProtectionProvider.Object);
            var newRole = new Domain.Entities.Role
            {
                RoleId = roleId,
                RoleName = "NewRole"
            };
            
            var result = await handler.Handle(new EditRoleCommand()
            {
                RoleId = "ProtectedRoleId",
                RoleName = newRole.RoleName
            }, CancellationToken.None);

            var allRoles = await _roleRepository.Object.GetAllRoles();              
            result.Message.ShouldBe("Role Updated Successfully");
        }
        private void CreateDataProtector(int roleId)
        {
            Mock<IDataProtector> mockDataProtector = new Mock<IDataProtector>();
            mockDataProtector.Setup(sut => sut.Unprotect(It.IsAny<byte[]>())).Returns(Encoding.UTF8.GetBytes(roleId.ToString()));
            mockDataProtectionProvider.Setup(s => s.CreateProtector(It.IsAny<string>())).Returns(mockDataProtector.Object);
        }
    }
}
