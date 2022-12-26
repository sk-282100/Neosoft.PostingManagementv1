using PostingManagement.Domain.Entities;

namespace PostingManagement.Application.Contracts.Persistence
{
    public interface ITriggerRepository
    {
        /// <summary>
        /// Add New trigger in the trigger Table
        /// </summary>
        /// <param name="scaleId">scale Id to be assigned to the trigger</param>
        /// <param name="Tenure">tenure is a time period of employee (in months)</param>
        /// <returns>returns true if trigger is added</returns>
        public Task<bool> AddTrigger( int scaleId, int Tenure, string mandatoryStatus);
        /// <summary>
        /// Delete trigger from the Table
        /// </summary>
        /// <param name="triggerId">Id of the trigger which is to be deleted</param>
        public Task<bool> DeleteTrigger(int triggerId);
        /// <summary>
        /// Update trigger using 4 parameters
        /// </summary>
        /// <param name="triggerId">trigger id for updation</param>
        /// <param name="scaleId">Updated Scale Id</param>
        /// <param name="Tenure">updated Tenure</param>
        /// <param name="mandatoryStatus">Updated Mandatory Status (Yes/No)</param>
        /// <returns>returns true if trigger updated successfully</returns>
        public Task<bool> UpdateTrigger(int triggerId, int scaleId, int Tenure, string mandatoryStatus);
        /// <summary>
        /// Get all the triggers present in the trigger table
        /// </summary>
        /// <returns>Returns List of triggerVm Model</returns>
        public Task<List<TriggerVm>> GetAllTriggerDetails();
        /// <summary>
        /// Get details of an trigger using triggerId
        /// </summary>
        /// <param name="triggerId">trigger id to get the trigger details</param>
        /// <returns>Returns model of trigger if found</returns>
        public Task<Trigger?> GetTriggerDetailsById(int triggerId);
    }
}
