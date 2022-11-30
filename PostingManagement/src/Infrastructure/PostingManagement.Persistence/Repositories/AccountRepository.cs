using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Domain.Entities;
using PostingManagement.Infrastructure.EncryptDecrypt;

namespace PostingManagement.Persistence.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;
        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddUser(string userName, int RoleId, string createdBy)
        {
            UserDetails userModel = new UserDetails();
            
            userModel.RoleId = RoleId;
            userModel.UserName = userName;
            userModel.Password = EncryptionDecryption.EncryptString("Pass@123");
            userModel.CreatedBy = createdBy;
            userModel.CreatedOn = DateTime.Now;
            await _context.UserDetailsTbl.AddAsync(userModel);

            return _context.SaveChanges()==1?true:false;
        }

        public async Task<bool> DeleteUser(int UserId, string deletedBy)
        {
            var userDetails = await _context.UserDetailsTbl.FirstOrDefaultAsync(x => x.UId == UserId && x.DeletedBy == null);
            userDetails.DeletedBy = deletedBy;
            userDetails.DeletedOn = DateTime.Now;
            _context.Entry(userDetails).State = EntityState.Modified;
            return _context.SaveChanges() == 1 ? true : false;

        }

        public async Task<List<UserDetailsVm>> GetAllUserDetails()
        {
            var userDetails = await (from user in _context.UserDetailsTbl
                                    join role in _context.RoleTable on user.RoleId equals role.RoleId 
                                    where user.DeletedBy == null
                                    select new UserDetailsVm
                                    {
                                        UId = user.UId,
                                        UserName = user.UserName,
                                        Role = role.RoleName
                                    }).OrderByDescending(x=>x.UId).ToListAsync();
                                                           ;
            return userDetails;
        }

        public async Task<UserDetails> GetUserDetailsById(int UserRoleId)
        {
            var userDetails = await _context.UserDetailsTbl.FirstOrDefaultAsync(x => x.UId == UserRoleId && x.DeletedBy == null);
            return userDetails;
        }

        public async Task<bool> UpdateUser(int uId, string userName, int roleId, string updatedBy)
        {
            var userDetails = await _context.UserDetailsTbl.FirstOrDefaultAsync(x => x.UId == uId && x.DeletedBy == null);
            userDetails.UserName=userName;
            userDetails.RoleId = roleId;
            userDetails.UpdatedBy = updatedBy;
            userDetails.UpdatedOn = DateTime.Now;
            _context.Entry(userDetails).State = EntityState.Modified;
            return _context.SaveChanges() == 1 ? true : false;

        }

        public async Task<bool> IsUserNamePresent(string userName)
        {
            return _context.UserDetailsTbl.Any(x => x.UserName == userName && x.DeletedBy == null);
        }

      
    }
}

