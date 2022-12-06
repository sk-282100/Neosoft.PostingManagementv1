
using PostingManagement.UI.Models;
using PostingManagement.UI.Models.AccountModels;
using PostingManagement.UI.Models.Responses;

namespace PostingManagement.UI.Services.AccountServices.Contracts
{
    public interface IAccountService
    {
        /// <summary>
        ///  Creating the username and defult Password for login
        /// </summary>
        /// <param name="request">object contains username and roleId</param>
        /// <returns> true if created successfully else return false</returns>
        public Task<Response<bool>> SaveUserDetails(CreateUserRequestModel request );

        /// <summary>
        /// remove the user from UserDetails table
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <param name="DeletedBy">Deleted by</param>
        /// <returns> true if user details Remove successfully else return false</returns>
        public Task<Response<bool>> DeleteUserDetails(string userId ,string DeletedBy);

        /// <summary>
        /// Update the user Role
        /// </summary>
        /// <param name="request">object contains username,RoleId,id and updatedby </param>
        /// <returns> true if user details Update successfully else return false</returns>
        public Task<Response<bool>> UpdateUserDetails(UpdateUserRoleRequestModel request );

        /// <summary>
        /// Get all values of user details
        /// </summary>
        /// <returns>List of objects contains UserId,Username and Role </returns>
        public Task<Response<List<GetAllUserVm>>> GetAllUserDetails();

        /// <summary>
        /// Get user details by UserId
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <returns>object of UserViewModel</returns>
        public Task<Response<UserViewModel>> GetUserById(string userId);

        /// <summary>
        /// Check the Username is present in the Records or not
        /// </summary>
        /// <param name="userName">user name</param>
        /// <returns> object of Response type with true if username is present else false</returns>
        public Task<Response<bool>> IsUserNamePresent(string userName);

        /// <summary>
        /// Reset Password for Particular Username
        /// </summary>
        /// <param name="request">object contains Username and New Password </param>
        /// <returns> true if Password Update successfully else return false</returns>
        public Task<Response<bool>> ResetPassword(ResetPasswordRequestModel request);

    }
}
