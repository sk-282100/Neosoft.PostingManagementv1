using PostingManagement.UI.Models.Responses;
using PostingManagement.UI.Models.TriggerModels;

namespace PostingManagement.UI.Services.TriggerServices.Contracts
{
    public interface ITriggerService
    {
        /// <summary>
        ///  Creating the trigger 
        /// </summary>
        /// <param name="request">object contains ScaleId , Tenure and Mandatory Status</param>
        /// <returns> true if trigger created successfully else return false</returns>
        public Task<Response<bool>> SaveTrigger(CreateTriggerRequestModel request);

        /// <summary>
        /// remove the trigger from Trigger table
        /// </summary>
        /// <param name="triggerId">Id of trigger</param>
        /// <returns> true if trigger Remove successfully else return false</returns>
        public Task<Response<bool>> DeleteTrigger(string triggerId);

        /// <summary>
        /// Update the Trigger
        /// </summary>
        /// <param name="request">object contains triggerId , ScaleId , Tenure and Mandatory </param>
        /// <returns> true if Trigger Update successfully else return false</returns>
        public Task<Response<bool>> UpdateTrigger(TriggerViewModel request);

        /// <summary>
        /// Get all values of Trigger
        /// </summary>
        /// <returns>List of objects contains TriggerId , ScaleName , Tenure and Mandatory </returns>
        public Task<Response<List<GetAllTriggerVm>>> GetAllTrigger();

        /// <summary>
        /// Get Trigger Details by UserId
        /// </summary>
        /// <param name="triggerId">triggerId</param>
        /// <returns>object of TriggerViewModel</returns>
        public Task<Response<TriggerViewModel>> GetTriggerById(string triggerId);
        public Task<Response<List<ScaleViewModel>>> GetAllScale();



    }
}
