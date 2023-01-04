using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Moq;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Features.Events.Queries.GetEventsList;
using PostingManagement.Application.Features.Roles.Queries.GetAllRoles;
using PostingManagement.Application.Profiles;
using PostingManagement.Application.Responses;
using PostingManagement.Application.UnitTests.Mocks;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PostingManagement.Application.UnitTests.Role.Queries
{
    public class GetAllRolesQueryHandlerTests
    {
        private Mock<IRoleRepository> _roleRepository;
        private readonly IMapper _mapper;
        Mock<IDataProtectionProvider> mockDataProtectionProvider = new Mock<IDataProtectionProvider>();
        public GetAllRolesQueryHandlerTests()
        {
            CreateDataProtector(1);
            _roleRepository = RoleRepositoryMocks.GetRoleRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.Entities.Role, GetAllRolesDto>().ConvertUsing(new GetAllRolesDtoCustomMapper(mockDataProtectionProvider.Object));
            });
            _mapper = configurationProvider.CreateMapper();            
        }
        [Fact]
        public async Task Handle_GetAllRoles_FromRoleRepository()
        {
            var handler = new GetAllRolesQueryHandler(_roleRepository.Object, _mapper);

            var result = await handler.Handle(new GetAllRolesQuery(), CancellationToken.None);           

            result.ShouldBeOfType<Response<List<GetAllRolesDto>>>();
            result.Data.Count.ShouldBe(3);
            result.Data.ShouldNotBeEmpty();
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