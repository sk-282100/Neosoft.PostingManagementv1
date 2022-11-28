using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Domain.Entities;

namespace PostingManagement.Persistence.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IDataProtector _protector;
        public AccountRepository(ApplicationDbContext context, IDataProtectionProvider provider)
        {
            _context = context;
            _protector = provider.CreateProtector("");
        }

        public async Task<bool> AddUser(string userName, int RoleId, string createdBy)
        {
            UserDetails userModel = new UserDetails();
            
            userModel.RoleId = RoleId;
            userModel.UserName = userName;
            userModel.Password = _protector.Protect( GeneratePassword(8));
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
                                    }).ToListAsync();
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

        private string GeneratePassword(int length = 12)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }
    }
}

