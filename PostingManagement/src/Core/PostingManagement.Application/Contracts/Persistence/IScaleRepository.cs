using PostingManagement.Domain.Entities;

namespace PostingManagement.Application.Contracts.Persistence
{
    public interface IScaleRepository
    {
        /// <summary>
        /// Get all the Scale present in the trigger table
        /// </summary>
        /// <returns>Returns List of Scale Model</returns>
        public Task<List<Scale>> GetAllScaleDetails();
    }
}
