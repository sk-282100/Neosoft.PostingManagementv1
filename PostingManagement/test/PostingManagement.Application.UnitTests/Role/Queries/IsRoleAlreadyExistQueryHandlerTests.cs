using Moq;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Features.Roles.Queries.GetRoleById;
using PostingManagement.Application.Features.Roles.Queries.IsRoleAlreadyExist;
using PostingManagement.Application.UnitTests.Mocks;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PostingManagement.Application.UnitTests.Role.Queries
{
    public class IsRoleAlreadyExistQueryHandlerTests
    {
        private Mock<IRoleRepository> _roleRepository;
        public IsRoleAlreadyExistQueryHandlerTests()
        {
            _roleRepository = RoleRepositoryMocks.GetRoleRepository();
        }
        [Fact]
        public async Task Handle_RoleDoesNotExists()
        {            
            var handler = new IsRoleAlreadyExistQueryHandler(_roleRepository.Object);

            var result = await handler.Handle(new IsRoleAlreadyExistQuery() { RoleName = "AnyRole" }, CancellationToken.None);
                        
            result.Data.ShouldBeFalse();
        }
        [Fact]
        public async Task Handle_RoleAlreadyExists()
        {
            var handler = new IsRoleAlreadyExistQueryHandler(_roleRepository.Object);

            var result = await handler.Handle(new IsRoleAlreadyExistQuery() { RoleName = "Admin" }, CancellationToken.None);

            result.Data.ShouldBeTrue();
        }
    }
}
