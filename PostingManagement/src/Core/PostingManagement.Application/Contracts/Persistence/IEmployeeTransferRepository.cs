using PostingManagement.Application.Features.TransferList.Queries.GetTransferList;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;

namespace PostingManagement.Application.Contracts.Persistence
{
    public interface IEmployeeTransferRepository
    {
        public Task<TransferListReponse> GetAllTransferListEmployees(GetTransferListQuery request);
        public Task<EmployeeDetailsForTransferList> GetEmployeeByIdAndMovementType(int employeeId, string movementType);
        public Task<List<MatchingRequestTransferVacancy>> GetMatchingRequestTransferList(List<int> employeeIdList);
        public Task<List<TransferListVM>> GetTransferListEmployeesByEmployeeId(List<int> employeeIdList);
        public Task<ZOTransferListReponse> InsertIntoTransferListForZo(List<TransferListVM> list);
    }
}
