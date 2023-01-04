using Moq;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Features.Events.Queries.GetEventsList;
using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.UnitTests.Mocks
{
    public class RoleRepositoryMocks
    {
        public static Mock<IRoleRepository> GetRoleRepository()
        {
            var roleList = new List<Domain.Entities.Role>
            {
                new Domain.Entities.Role
                {
                    RoleId = 1,
                    RoleName = "Admin" 
                },
                new Domain.Entities.Role
                {
                    RoleId = 2,
                    RoleName = "CO"
                },
                new Domain.Entities.Role
                {
                    RoleId = 3,
                    RoleName = "ZO"
                }
            };
            var mockRoleRepository = new Mock<IRoleRepository>();
            mockRoleRepository.Setup(repo => repo.GetAllRoles()).ReturnsAsync(
                () =>
                {
                    return roleList.OrderByDescending(x => x.RoleId).ToList();
                });
            mockRoleRepository.Setup(repo => repo.GetRoleById(It.IsAny<int>())).ReturnsAsync(
                (int roleId) =>
                {
                    return roleList.Where(x => x.RoleId == roleId).FirstOrDefault();
                });
            mockRoleRepository.Setup(repo => repo.GetRoleByName(It.IsAny<string>())).ReturnsAsync(
                (string roleName) =>
                {
                    return roleList.Where(x => x.RoleName == roleName).FirstOrDefault();
                });
            mockRoleRepository.Setup(repo => repo.IsRoleAlreadyExist(It.IsAny<string>())).ReturnsAsync(
                (string roleName) =>
                {
                    return roleList.Any(x => x.RoleName == roleName);
                });
            mockRoleRepository.Setup(repo => repo.UpdateRole(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(
                (int roleId, string roleName) =>
                {
                    var oldRole = roleList.First(x => x.RoleId == roleId);
                    var index = roleList.IndexOf(oldRole);
                    if (index != -1)
                    {
                        oldRole.RoleId = roleId;
                        oldRole.RoleName = roleName;                        
                        roleList[index] = oldRole;
                        return true;
                    }
                    return false;
                });
            mockRoleRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Domain.Entities.Role>())).ReturnsAsync(
                (Domain.Entities.Role role) =>
                {
                    roleList.Remove(role);
                    return true;
                });

            mockRoleRepository.Setup(repo => repo.AddAsync(It.IsAny<string>())).ReturnsAsync(
                (string roleName) =>
                {
                    Domain.Entities.Role role = new Domain.Entities.Role { RoleName = roleName };
                    roleList.Add(role);
                    return true;
                });

            return mockRoleRepository;
        }
    }
}
