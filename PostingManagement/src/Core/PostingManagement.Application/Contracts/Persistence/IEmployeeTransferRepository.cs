using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;

namespace PostingManagement.Application.Contracts.Persistence
{
    public interface IEmployeeTransferRepository
    {
        public Task<List<TransferListVM>> GetAllTransferListEmployees(int pageNumber, int numberOfRecords);
        public Task<EmployeeDetailsForTransferList> GetEmployeeByIdAndMovementType(int employeeId, string movementType);
        public ZOTransferListReponse InsertIntoTransferListForZo(List<TransferListVM> list);
    }
}
