using Moq;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Features.Roles.Commands.AddRole;
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
    public class AddRoleTests
    {
        private Mock<IRoleRepository> _roleRepository;
        public AddRoleTests()
        {
            _roleRepository = RoleRepositoryMocks.GetRoleRepository();
        }
        [Fact]
        public async Task Handle_RoleNameDoesNotExists()
        {
            var handler = new AddRoleCommandHandler(_roleRepository.Object);

            var result = await handler.Handle(new AddRoleCommand()
            {
                RoleName = "test"   
            }, CancellationToken.None);

            var allRoles = await _roleRepository.Object.GetAllRoles();                        
            result.Message.ShouldBe("Role Added Successfully");
            allRoles.Count.ShouldBe(4);
        }
        [Fact]
        public async Task Handle_RoleNameAlreadyExists()
        {
            var handler = new AddRoleCommandHandler(_roleRepository.Object);

            var result = await handler.Handle(new AddRoleCommand()
            {
                RoleName = "Admin"
            }, CancellationToken.None);

            var allRoles = await _roleRepository.Object.GetAllRoles();
            result.Message.ShouldBe("Role Already Exists");
            allRoles.Count.ShouldBe(3);
        }
        [Fact]
        public async Task Handle_SuccessfullyAddedRole()
        {
            var handler = new AddRoleCommandHandler(_roleRepository.Object);

            var result = await handler.Handle(new AddRoleCommand()
            {
                RoleName = "testRole"
            }, CancellationToken.None);

            var allRoles = await _roleRepository.Object.GetAllRoles();
            result.Message.ShouldBe("Role Added Successfully");
            allRoles.Count.ShouldBe(4);
        }
    }
}