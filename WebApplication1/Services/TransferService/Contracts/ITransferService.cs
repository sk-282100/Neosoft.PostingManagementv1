using PostingManagement.UI.Models.EmployeeTransferModels;
using PostingManagement.UI.Models.Responses;
using System.Data;

namespace PostingManagement.UI.Services.TransferService.Contracts
{
    public interface ITransferService
    {
        /// <summary>
        /// Gets the List of all the Employees for transfer
        /// </summary>
        /// <returns>List of employees avaialbale for transfer</returns>
        public Task<List<EmployeeTransferModel>> GetEmployeesForTransfer();

        /// <summary>
        /// Gets the Additional information about the Employee based on EmployeeId
        /// </summary>
        /// <param name="employeeId">Employee Id of the Employee</param>
        /// <param name="movementType">string</param>
        /// <returns>returns an object of type EmployeeDetailsForTransfer </returns>
        public Task<EmployeeDetailsForTransferList> GetEmployeeAddidtionalDetails(int employeeId, string movementType);

        /// <summary>
        /// Get the EmployeesList By employeeId selected by CoAdmin
        /// </summary>
        /// <param name="emoloyeeIdList"></param>
        /// <returns>List of EmployeeTransferModel</returns>
        public Task<List<EmployeeTransferModel>> GetEmployeesDataForTransferByEmployeeId(List<int> emoloyeeIdList);

        /// <summary>
        /// Insert the Data into TRansferListForZo
        /// </summary>
        /// <param name="EmployeesSelectedByCo"></param>
        /// <returns>Returns a Serialized Object which includes the no of rows inserted and Status  </returns>
        public Task<ZOTransferListReponse> GenerateEmployeeTransferListCo(List<EmployeeTransferModel> EmployeesSelectedByCo);
    }
}
