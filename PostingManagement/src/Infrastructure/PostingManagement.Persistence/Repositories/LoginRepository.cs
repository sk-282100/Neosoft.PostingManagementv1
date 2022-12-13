using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Domain.Entities;

namespace PostingManagement.Persistence.Repositories
{
    public class LoginRepository : BaseRepository<LoginModel>, ILoginRepository
    {
        private readonly ApplicationDbContext _context;
        public LoginRepository(ApplicationDbContext dbContext, ILogger<LoginModel> logger) : base(dbContext, logger)
        {
            _context = dbContext;
        }
        public async Task<UserDetails> GetDetailsByUsername(string username)
        {
            var userDetails = await _context.UserDetailsTbl.FirstAsync(x => x.UserName == username && x.DeletedBy == null);
            return userDetails;
        }

        public async Task<string> GetRoleByid(int RoleId)
        {
            var result = await _context.RoleTable.FirstAsync(x => x.RoleId == RoleId);
            return result.RoleName;
        }

        public async Task<bool> SaveOTP(OTPModel otpModel)
        {
            var existingOTP = await _context.OTPTbl.FirstOrDefaultAsync(x => x.UId == otpModel.UId);
            if (existingOTP != null)
            {
                _context.OTPTbl.Remove(existingOTP);
                _context.SaveChanges();
            }
            await _context.OTPTbl.AddAsync(otpModel);
            return await _context.SaveChangesAsync() == 1 ? true : false;
        }

        public async Task<OTPModel> GetOTPDetailsByUId(int uId)
        {
            var otpDetails = await _context.OTPTbl.FirstOrDefaultAsync(x => x.UId == uId);
            return otpDetails;
        }

        public async Task<int> generateOTP(int otpLength = 4)
        {
            char[] charArr = "0123456789".ToCharArray();
            string strrandom = string.Empty;

            Random objran = new Random();
            for (int i = 0; i < otpLength; i++)
            {
                //It will not allow Repetation of Characters
                int pos = objran.Next(1, charArr.Length);
                if (!strrandom.Contains(charArr.GetValue(pos).ToString()))
                {
                    strrandom += charArr.GetValue(pos);
                }
                else
                { 
                    i--;
                }
            }
            return Convert.ToInt32(strrandom);
        }
    }
}