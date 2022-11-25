﻿using PostingManagement.UI.Models.AccountModels;
using PostingManagement.UI.Models.Responses;

namespace PostingManagement.UI.Services.AccountServices.Contracts
{
    public interface IAccountService
    {
        public Task<Response<bool>> SaveUserDetails(CreateUserRequestModel request );
        public Task<Response<bool>> DeleteUserDetails(string userId ,string DeletedBy);
        public Task<Response<bool>> UpdateUserDetails(UpdateUserRoleRequestModel request );
        public Task<Response<List<UserViewModel>>> GetAllUserDetails();
        public Task<Response<UserViewModel>> GetUserById(string userId);
        public Task<Response<bool>> IsUserNamePresent(string userName);


    }
}