using PostingManagement.Domain.Entities;

namespace PostingManagement.Application.Contracts.Persistence
{
    public interface IAccountRepository
    {
        /// <summary>
        /// Add New User in the User Details Table
        /// </summary>
        /// <param name="userName">username to be added</param>
        /// <param name="RoleId">Role Id to be assigned to the user</param>
        /// <param name="createdBy">Name of the Creator of the user</param>
        /// <returns>returns true if user is added</returns>
        public Task<bool> AddUser(string userName, int RoleId, string createdBy);
        /// <summary>
        /// Delete user from the Table
        /// </summary>
        /// <param name="UserId">Id of the user which is to be deleted</param>
        /// <param name="deletedBy">Name of person who is deleting the user</param>
        /// <returns></returns>
        public Task<bool> DeleteUser(int UserId, string deletedBy);
        /// <summary>
        /// Update User using 4 parameters
        /// </summary>
        /// <param name="uId">user id to be updated</param>
        /// <param name="userName">Updated User Name</param>
        /// <param name="roleId">updated Role Id</param>
        /// <param name="updatedBy">user Updated By</param>
        /// <returns>returns true if updated successfully</returns>
        public Task<bool> UpdateUser(int uId, string userName, int roleId, string updatedBy);
        /// <summary>
        /// Get all the users present in the User details table
        /// </summary>
        /// <returns>Returns List of UserDetailsVm Model</returns>
        public Task<List<UserDetailsVm>> GetAllUserDetails();
        /// <summary>
        /// Get details of an user using userId
        /// </summary>
        /// <param name="UserId">user id to get the user details</param>
        /// <returns>Returns model of UserDetails if found</returns>
        public Task<UserDetails> GetUserDetailsById(int UserId);
        /// <summary>
        /// To check if username is already present
        /// </summary>
        /// <param name="userName">Username to check if it is present</param>
        /// <returns>returns true if user is present</returns>
        public Task<bool> IsUserNamePresent(string userName);

        /// <summary>
        /// Reset the Password by the UserName
        /// </summary>
        /// <param name="userName"> User Name</param>
        /// <param name="newPassword">user id to be updated</param>
        /// <returns>returns true if updated successfully</returns>
        public Task<bool> ResetPassword( string userName, string newPassword);
    }
}
