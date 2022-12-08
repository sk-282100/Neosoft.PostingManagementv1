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
        /// Converts List of Employee into Datatable
        /// </summary>
        /// <param name="employeeList">List of employees in transfer list</param>
        /// <returns>DataTable of EmployeeList</returns>
        public DataTable ListToDataTable(List<EmployeeTransferModel> employeeList);


    }
}
