﻿using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;

namespace PostingManagement.Application.Contracts.Persistence
{
    public interface IEmployeeTransferRepository
    {
        public Task<List<TransferListVM>> GetAllTransferListEmployees();
        public Task<EmployeeDetailsForTransferList> GetEmployeeByIdAndMovementType(int employeeId, string movementType);
        public Task<List<TransferListVM>> GetTransferListEmployeesByEmployeeId(List<int> employeeIdList);
        public ZOTransferListReponse InsertIntoTransferListForZo(List<TransferListVM> list);
    }
}
