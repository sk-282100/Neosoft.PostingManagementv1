using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Moq;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Features.Roles.Queries.GetAllRoles;
using PostingManagement.Application.Features.Roles.Queries.GetRoleById;
using PostingManagement.Application.Profiles;
using PostingManagement.Application.Responses;
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
    public class GetRoleByIdQueryHandlerTests
    {
        private Mock<IRoleRepository> _roleRepository;
        private readonly IMapper _mapper;
        Mock<IDataProtectionProvider> mockDataProtectionProvider = new Mock<IDataProtectionProvider>();
        public GetRoleByIdQueryHandlerTests()
        {
            CreateDataProtector(1);
            _roleRepository = RoleRepositoryMocks.GetRoleRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.Entities.Role, GetRoleByIdDto>().ConvertUsing(new GetRolesByIdCustomMapper(mockDataProtectionProvider.Object));
            });
            _mapper = configurationProvider.CreateMapper();
        }
        [Fact]
        public async Task Handle_RoleIdDoesNotExists()
        {
            int roleId = 4;
            CreateDataProtector(roleId);
            var handler = new GetRoleByIdQueryHandler(_roleRepository.Object, _mapper, mockDataProtectionProvider.Object);

            var result = await handler.Handle(new GetRoleByIdQuery() { RoleId = "ProtectedRoleId" }, CancellationToken.None);
            
            result.Succeeded.ShouldBeFalse();
            result.Message.ShouldBe("Role Does not Exists");
        }
        [Fact]
        public async Task Handle_GetRoleById_FromRoleRepository()
        {
            int roleId = 1;
            CreateDataProtector(roleId);
            var handler = new GetRoleByIdQueryHandler(_roleRepository.Object, _mapper, mockDataProtectionProvider.Object);

            var result = await handler.Handle(new GetRoleByIdQuery() { RoleId = "ProtectedRoleId"}, CancellationToken.None);

            result.ShouldBeOfType<Response<GetRoleByIdDto>>();
            result.Succeeded.ShouldBeTrue();
            result.ShouldNotBeNull();
        }
        private void CreateDataProtector(int roleId)
        {
            Mock<IDataProtector> mockDataProtector = new Mock<IDataProtector>();
            mockDataProtector.Setup(sut => sut.Protect(It.IsAny<byte[]>())).Returns(Encoding.UTF8.GetBytes("ProtectedData"));
            mockDataProtector.Setup(sut => sut.Unprotect(It.IsAny<byte[]>())).Returns(Encoding.UTF8.GetBytes(roleId.ToString()));
            mockDataProtectionProvider.Setup(s => s.CreateProtector(It.IsAny<string>())).Returns(mockDataProtector.Object);
        }
    }
}
