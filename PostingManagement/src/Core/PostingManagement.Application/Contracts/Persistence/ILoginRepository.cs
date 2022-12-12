using PostingManagement.Domain.Entities;

namespace PostingManagement.Application.Contracts.Persistence
{
    public interface ILoginRepository
    {
        /// <summary>
        /// Get User Details using Username
        /// </summary>
        /// <param name="username">username to get the details</param>
        /// <returns>Returns UserDetails Modek which contains all the details of the user</returns>
        public Task<UserDetails> GetDetailsByUsername(string username);
        /// <summary>
        /// Get Role using Role Id
        /// </summary>
        /// <param name="RoleId">Role ID to get the Role</param>
        /// <returns>Returns Role Name if found</returns>
        public Task<string> GetRoleByid(int RoleId);

        /// <summary>
        /// Save the Otp Details to the OTP Table 
        /// </summary>
        /// <param name="otpModel">object contains UId ,OTP,Email and Expirytime</param>
        /// <returns>return true if OTP Details save Successfully else returns false</returns>
        public  Task<bool> SaveOTP(OTPModel otpModel);

        /// <summary>
        /// Get the OTP Details by Uid (UserId)
        /// </summary>
        /// <param name="uId">User Id of int type</param>
        /// <returns>Object containing OTP Details</returns>
        public  Task<OTPModel> GetOTPDetailsByUId(int uId);

        /// <summary>
        /// Generate Digit OTP 
        /// </summary>
        /// <param name="otpLength">Maximum Length of the required OTP</param>
        /// <returns>OTP of Int Type</returns>
        public Task<int> generateOTP(int otpLength = 4);

    }
}
