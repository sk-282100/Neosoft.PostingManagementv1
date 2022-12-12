using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Contracts.Persistence
{
    public interface IJobFamilyRepository : IAsyncRepository<JobFamilies>
    {
        public Task<bool> AddAsync(string jobFamilyName);
        public Task<bool> DeleteAsync(JobFamilies jobFamilies);
        public Task<bool> UpdateAsync(JobFamilies jobFamilies); 
        public Task <List<JobFamilies>> GetAllJobFamily();
        public Task<JobFamilies> GetJobFamilyById(int id);
        public Task<JobFamilies> GetJobFamilyByName(string jobFamilyName);
        public Task<bool> IsJobFamilyAlreadyExist(string jobFamilyName);
    }
}
