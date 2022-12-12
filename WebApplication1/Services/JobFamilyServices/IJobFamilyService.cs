using PostingManagement.Application.Responses;
using PostingManagement.UI.Models;

namespace PostingManagement.UI.Services.JobFamilyServices
{
    public interface IJobFamilyService
    {
        public Task<List<JobFamilyModel>> GetAllJobFamily();
        public Task<Response<bool>> AddJobFamily(JobFamilyModel jobFamilyModel);
        public Task<Response<bool>> RemoveJobFamily(string id);
        public Task<Response<bool>> EditJobFamily(JobFamilyModel jobFamilyModel);   
        public Task <Response<JobFamilyModel>> GetJobFamilyById(string id);
        public Task <Response<bool>> IsJobFamilyAlreadyExist(string jobFamilyName);
    }
}
